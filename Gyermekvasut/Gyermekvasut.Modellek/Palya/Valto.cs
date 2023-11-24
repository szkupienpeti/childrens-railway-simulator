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
    public Irany CsucsFelolIrany { get; }
    // Alaphelyzet
    public ValtoAllas AlapAllas { get; }
    public ValtoLezaras AlapLezaras { get; }
    // Pálya
    private PalyaElem? csucsSzar;
    private PalyaElem? egyenesSzar;
    private PalyaElem? kiteroSzar;

    public Valto(string nev, Irany csucsFelolIrany, ValtoTajolas tajolas, int allitasiIdo)
        : this(nev, csucsFelolIrany, tajolas, allitasiIdo, ValtoAllas.Egyenes, ValtoLezaras.Feloldva) { }

    public Valto(string nev, Irany csucsFelolIrany, ValtoTajolas tajolas, int allitasiIdoSec,
        ValtoAllas alapAllas, ValtoLezaras alapLezaras) : base(nev)
    {
        Vegallas = alapAllas;
        Vezerles = alapAllas;
        Lezaras = alapLezaras;
        allitasTimer = new(allitasiIdoSec * MILLISECONDS_IN_SECOND / Szimulacio.Instance.SebessegSzorzo);
        allitasTimer.AutoReset = false;
        allitasTimer.Elapsed += AllitasiIdoLejart;
        AllitasiIdoSec = allitasiIdoSec;
        Tajolas = tajolas;
        CsucsFelolIrany = csucsFelolIrany;
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

    public void Allit(ValtoAllas cel)
    {
        if (Lezaras == ValtoLezaras.Lezarva || Vegallas == cel)
        {
            return;
        }
        Vezerles = cel;
        Vegallas = null;
        allitasTimer.Start();
    }

    public void Lezar()
    {
        if (Vegallas == null)
        {
            return;
        }
        Lezaras = ValtoLezaras.Lezarva;
    }

    public void Felold()
    {
        Lezaras = ValtoLezaras.Feloldva;
    }

    public void Szomszedolas(PalyaElem csucsFelol, PalyaElem egyenes, PalyaElem kitero)
    {
        this.csucsSzar = csucsFelol;
        this.egyenesSzar = egyenes;
        this.kiteroSzar = kitero;
    }
    public override PalyaElem? Kovetkezo(Irany irany)
    {
        if (irany == CsucsFelolIrany)
        {
            return Vegallas switch
            {
                ValtoAllas.Egyenes => egyenesSzar,
                ValtoAllas.Kitero => kiteroSzar,
                _ => throw new InvalidOperationException($"A csúcs felől érintett {Nev} váltónak nincs végállása")
            };
        }
        else
        {
            if (Vegallas == null)
            {
                throw new InvalidOperationException($"Váltfelvágás: a gyök felől érintett {Nev} váltónak nincs végállása");
            }
            return csucsSzar;
        }
    }

    public override void KpSzomszedolas(PalyaElem kpSzomszed)
    {
        switch (CsucsFelolIrany)
        {
            case Irany.Paros:
                csucsSzar = kpSzomszed;
                break;
            case Irany.Paratlan:
                egyenesSzar = kpSzomszed;
                break;
        }
    }
    public override void VpSzomszedolas(PalyaElem vpSzomszed)
    {
        switch (CsucsFelolIrany)
        {
            case Irany.Paros:
                egyenesSzar = vpSzomszed;
                break;
            case Irany.Paratlan:
                csucsSzar = vpSzomszed;
                break;
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