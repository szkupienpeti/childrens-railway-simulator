using Gyermekvasut.Modellek.VonatNS;
using System.Diagnostics;

namespace Gyermekvasut.Modellek.Palya;

public class Szakasz : EgyenesPalyaElem
{
    private static readonly int HIANYZO_HOSSZ = -1;

    public int Hossz { get; private set; }
    public bool HosszHianyzik { get => Hossz == HIANYZO_HOSSZ; }
    private Szerelveny? _szerelveny = null;
    public Szerelveny? Szerelveny
    {
        get => _szerelveny;
        private set
        {
            if (value != _szerelveny)
            {
                Szerelveny? elozoSzerelveny = _szerelveny;
                _szerelveny = value;
                OnSzerelvenyChanged(elozoSzerelveny);
            }
        }
    }
    public event EventHandler<SzakaszSzerelvenyChangedEventArgs>? SzerelvenyChanged;

    public Szakasz(string nev) : base(nev)
    {
        Hossz = HIANYZO_HOSSZ;
    }

    public Szakasz(string nev, int hossz) : base(nev)
    {
        ValidateHossz(hossz);
        Hossz = hossz;
    }

    public void SetHossz(int hossz)
    {
        if (Hossz != HIANYZO_HOSSZ)
        {
            throw new InvalidOperationException("Már be van állítva a szakasz hossza");
        }
        ValidateHossz(hossz);
        Hossz = hossz;
    }

    private static void ValidateHossz(int hossz)
    {
        if (hossz <= 0)
        {
            throw new ArgumentException("A szakasz hosszának pozitívnak kell lennie");
        }
    }

    public void Elfoglal(Szerelveny szerelveny)
    {
        if (Szerelveny != null)
        {
            throw new ArgumentException("Foglalt szakaszt próbál elfoglalni egy szerelvény");
        }
        Szerelveny = szerelveny;
        Trace.WriteLine($"{szerelveny} elfoglalta {this} szakaszt");
    }

    public virtual void Felszabadit(Szerelveny szerelveny)
    {
        if (Szerelveny != szerelveny)
        {
            throw new ArgumentException("Nem a szakasz szerelvénye próbálja felszabadítani a szakaszt");
        }
        Szerelveny = null;
        Trace.WriteLine($"{szerelveny} elhagyta {this} szakaszt");
    }

    protected virtual void OnSzerelvenyChanged(Szerelveny? elozoSzerelveny)
    {
        SzerelvenyChanged?.Invoke(this, new(elozoSzerelveny, Szerelveny));
    }
}

public class SzakaszSzerelvenyChangedEventArgs : EventArgs
{
    public Szerelveny? ElozoSzerelveny { get; }
    public Szerelveny? UjSzerelveny { get; }
    public SzakaszSzerelvenyChangedEventArgs(Szerelveny? elozoSzerelveny, Szerelveny? ujSzerelveny)
    {
        ElozoSzerelveny = elozoSzerelveny;
        UjSzerelveny = ujSzerelveny;
    }
}
