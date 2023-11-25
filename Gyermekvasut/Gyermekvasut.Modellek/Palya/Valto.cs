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
                _vegallas = value;
                OnVegallasChanged();
            }
        }
    }
    public event EventHandler<ValtoVegallasEventArgs>? VegallasChanged;
    public ValtoAllas Vezerles { get; private set; }
    public ValtoLezaras Lezaras { get; private set; }
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
    private PalyaElem? egyenesSzar;
    private PalyaElem? kiteroSzar;

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

    private void OnVegallasChanged()
    {
        VegallasChanged?.Invoke(this, new ValtoVegallasEventArgs(Vegallas));
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

    public bool Lezar()
    {
        if (Vegallas == null)
        {
            return false;
        }
        Lezaras = ValtoLezaras.Lezarva;
        return true;
    }

    public void Felold()
    {
        Lezaras = ValtoLezaras.Feloldva;
    }

    public void Szomszedolas(PalyaElem csucsFelol, PalyaElem egyenes, PalyaElem kitero)
    {
        csucsSzar = csucsFelol;
        egyenesSzar = egyenes;
        kiteroSzar = kitero;
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
    public ValtoAllas? Vegallas { get; set; }

    public ValtoVegallasEventArgs(ValtoAllas? vegallas)
    {
        Vegallas = vegallas;
    }
}