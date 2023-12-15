using Gyermekvasut.Modellek.VonatNS;

namespace Gyermekvasut.Modellek.Palya;

public class Valto : PalyaElem
{
    private static readonly int MILLISECONDS_IN_SECOND = 1000;
    // Dinamikus
    private ValtoAllas? _vegallas;
    public ValtoAllas? Vegallas
    {
        get => _vegallas;
        private set
        {
            if (value != _vegallas)
            {
                ValtoAllas? elozoVegallas = _vegallas;
                _vegallas = value;
                OnVegallasChanged(elozoVegallas);
            }
        }
    }
    public event EventHandler? VegallasChanged;
    public ValtoAllas Vezerles { get; private set; }

    private ValtoLezaras _lezaras;
    public ValtoLezaras Lezaras
    {
        get => _lezaras;
        private set
        {
            if (value != _lezaras)
            {
                _lezaras = value;
                OnLezarasChanged();
            }
        }
    }

    public event EventHandler? LezarasChanged;

    // TODO Csak az elektromos váltóban legyen, a helyszíni váltó azonnal átáll, ott a váltókezelőbe kell időzítés
    private readonly System.Timers.Timer allitasTimer;

    // Statikus
    public int AllitasiIdoSec { get; }
    public ValtoTajolas Tajolas { get; }
    public Irany CsucsIrany { get; }
    // Alaphelyzet
    public ValtoAllas AlapAllas { get; }
    public ValtoLezaras AlapLezaras { get; }
    // Pálya
    private PalyaElem? csucsSzar;
    public PalyaElem CsucsSzar => csucsSzar!;
    private PalyaElem? egyenesSzar;
    public PalyaElem EgyenesSzar => egyenesSzar!;
    private PalyaElem? kiteroSzar;
    public PalyaElem KiteroSzar => kiteroSzar!;

    public Valto(string nev, Irany csucsIrany, ValtoTajolas tajolas, int allitasiIdo)
        : this(nev, csucsIrany, tajolas, allitasiIdo, ValtoAllas.Egyenes, ValtoLezaras.Feloldva) { }

    public Valto(string nev, Irany csucsIrany, ValtoTajolas tajolas, int allitasiIdoSec,
        ValtoAllas alapAllas, ValtoLezaras alapLezaras) : base(nev)
    {
        Vegallas = alapAllas;
        Vezerles = alapAllas;
        Lezaras = alapLezaras;
        allitasTimer = new(allitasiIdoSec * MILLISECONDS_IN_SECOND / Szimulacio.Instance.SebessegSzorzo)
        {
            AutoReset = false
        };
        allitasTimer.Elapsed += AllitasiIdoLejart;
        AllitasiIdoSec = allitasiIdoSec;
        Tajolas = tajolas;
        CsucsIrany = csucsIrany;
        AlapAllas = alapAllas;
        AlapLezaras = alapLezaras;
    }

    public PalyaElem GetGyokSzar(ValtoAllas valtoAllas)
    {
        return valtoAllas switch
        {
            ValtoAllas.Egyenes => EgyenesSzar,
            ValtoAllas.Kitero => KiteroSzar,
            _ => throw new NotImplementedException()
        };
    }

    public Vagany GetGyokFeloliVagany(ValtoAllas valtoAllas)
    {
        Irany gyokIrany = CsucsIrany.Fordit();
        PalyaElem valtoGyokSzar = GetGyokSzar(valtoAllas);
        return valtoGyokSzar.GetKovetkezoFeltetelesPalyaElem<Vagany>(gyokIrany)!;
    }

    private void OnVegallasChanged(ValtoAllas? elozoVegallas)
    {
        VegallasChanged?.Invoke(this, new ValtoVegallasEventArgs(elozoVegallas, Vegallas));
    }

    private void OnLezarasChanged()
    {
        LezarasChanged?.Invoke(this, EventArgs.Empty);
    }

    private void AllitasiIdoLejart(object? sender, System.Timers.ElapsedEventArgs e)
    {
        Vegallas = Vezerles;
    }

    public bool Allit(ValtoAllas cel)
    {
        if (Lezaras == ValtoLezaras.Lezarva || Vegallas == cel)
        {
            return false;
        }
        Vezerles = cel;
        Vegallas = null;
        allitasTimer.Start();
        return true;
    }

    public void Lezar(ValtoAllas allas)
    {
        if (Vegallas != allas)
        {
            throw new InvalidOperationException($"A váltó {Vegallas} végállásban van, de {allas} állásba próbálták lezárni");
        }
        Lezaras = ValtoLezaras.Lezarva;
    }

    public void Felold(ValtoAllas allas)
    {
        if (Vegallas != allas)
        {
            throw new InvalidOperationException($"A váltó {Vegallas} végállásban van, de {allas} állásból próbálták feloldani");
        }
        Lezaras = ValtoLezaras.Feloldva;
    }

    public override PalyaElem? Kovetkezo(Irany irany)
    {
        if (irany == CsucsIrany)
        {
            if (Vegallas == null)
            {
                throw new InvalidOperationException($"Váltfelvágás: a gyök felől érintett {Nev} váltónak nincs végállása");
            }
            return csucsSzar;
        }
        else
        {
            return Vegallas switch
            {
                ValtoAllas.Egyenes => egyenesSzar,
                ValtoAllas.Kitero => kiteroSzar,
                _ => throw new InvalidOperationException($"A csúcs felől érintett {Nev} váltónak nincs végállása")
            };
        }
    }

    public override void Szomszedolas(Irany irany, PalyaElem szomszed)
    {
        if (irany == CsucsIrany)
        {
            csucsSzar = szomszed;
        }
        else
        {
            egyenesSzar = szomszed;
        }
    }

    public void KiteroSzomszedolas(PalyaElem kiteroSzomszed)
    {
        kiteroSzar = kiteroSzomszed;
    }
}

public enum ValtoTajolas
{
    Balos = 1,
    Jobbos = 2
}

public enum ValtoAllas
{
    Egyenes = 1,
    Kitero = 2
}

public enum ValtoLezaras
{
    Feloldva = 0,
    Lezarva = 1
}

public class ValtoVegallasEventArgs : EventArgs
{
    public ValtoAllas? ElozoVegallas { get; set; }
    public ValtoAllas? UjVegallas { get; set; }

    public ValtoVegallasEventArgs(ValtoAllas? elozoVegallas, ValtoAllas? ujVegallas)
    {
        ElozoVegallas = elozoVegallas;
        UjVegallas = ujVegallas;
    }
}