using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Palya.Jelzok;
using System.Diagnostics;

namespace Gyermekvasut.Modellek.VonatNS;

public class Szerelveny
{
    public static readonly int PALYASEBESSEG = 20;
    public static readonly int CSOKKENTETT_SEBESSEG = 15;
    protected static readonly int TOLATAS_SEBESSEG = 10;

    private System.Timers.Timer ElejeTimer { get; } = new() { AutoReset = false };
    private System.Timers.Timer VegeTimer { get; } = new() { AutoReset = false };
    public int MaxSebesseg { get; protected set; } = 20;
    public string SzerelvenyNev { get; }
    public Irany Irany { get; protected set; }
    public VonatIrany VonatIrany => Irany.ToVonatIrany();
    public virtual string Nev => SzerelvenyNev;
    public List<Jarmu> Jarmuvek { get; } = new();
    public int Hossz
    {
        get
        {
            return Jarmuvek.Sum(j => j.Tipus.Hossz());
        }
    }
    public List<Szakasz> Szakaszok { get; } = new();
    private int UtolsoSzakaszElfoglaltHossz { get; set; }
    public bool Megszuntetve { get; private set; }

    public Szerelveny(string szerelvenyNev, Irany irany, params Jarmu[] jarmuvek)
    {
        SzerelvenyNev = szerelvenyNev;
        Irany = irany;
        foreach (var jarmu in jarmuvek)
        {
            Jarmuvek.Add(jarmu);
        }
        ElejeTimer.Elapsed += ElejeTimer_Elapsed;
        VegeTimer.Elapsed += VegeTimer_Elapsed;
    }

    public void Lehelyez(Szakasz szakasz)
    {
        if (szakasz.Hossz < Hossz)
        {
            throw new ArgumentException("A szerelvény lehelyezési szakasza rövidebb, mint a szerelvény");
        }
        Szakaszok.Add(szakasz);
        UtolsoSzakaszElfoglaltHossz = Hossz;
        szakasz.Elfoglal(this);
    }

    public void Tovabblep()
    {
        // Feltételezés: szakasz szélén áll
        PalyaElem elem = Szakaszok[0];
        // Ha vágányon áll: menesztés szükséges
        if (elem is Vagany vagany && !vagany.Menesztes)
        {
            // Menesztésre (feloszlásra) várakozás
            Trace.WriteLine(Nev + " menesztésre (feloszlásra) vár " + vagany.AllomasNev.Nev() + " " + vagany.Nev + " vágányon");
            vagany.MenesztesChanged += Vagany_MenesztesChanged;
            return;
        }
        PalyaElem? kovetkezo = elem;
        while ((kovetkezo = kovetkezo.Kovetkezo(Irany)) != null)
        {
            if (kovetkezo is Fojelzo fojelzo)
            {
                if (fojelzo.Irany == Irany)
                {
                    // Szemből érintett főjelző
                    MaxSebesseg = fojelzo.Jelzes.Sebesseg();
                    if (fojelzo.Jelzes == Sebessegjelzes.Megallj)
                    {
                        // Szabadra várakozás
                        Trace.WriteLine(Nev + " szabadra vár " + fojelzo.Nev + " előtt");
                        fojelzo.JelzesChanged += MegalljFojelzo_JelzesChanged;
                        return;
                    }
                }
                else if (fojelzo.Rendeltetes == FojelzoRendeltetes.Bejarati)
                {
                    // Háttal érintett bejárati jelző
                    MaxSebesseg = PALYASEBESSEG;
                }
            }
            else if (kovetkezo is Szakasz szakasz)
            {
                szakasz.Elfoglal(this);
                Szakaszok.Insert(0, szakasz);
                ElejeTimer.Interval = TavolsagInterval(szakasz.Hossz);
                ElejeTimer.Start();
                if (!VegeTimer.Enabled)
                {
                    VegeTimerLeptet();
                }
            }
        }
    }

    public void Megszuntet()
    {
        ElejeTimer.Stop();
        VegeTimer.Stop();
        Szakaszok.ForEach(sz => sz.Felszabadit(this));
        Megszuntetve = true;
    }

    private void VegeTimerLeptet()
    {
        int foglaltSzakaszokOsszHossz = Szakaszok.Sum(sz => sz.Hossz);
        int utolsoFoglaltSzakaszHossz = GetUtolsoSzakasz().Hossz;
        int osszHosszUtolsoNelkul = foglaltSzakaszokOsszHossz - utolsoFoglaltSzakaszHossz;
        int utolsoSzakaszraJutoFoglaltsag = Hossz - osszHosszUtolsoNelkul;
        if (utolsoSzakaszraJutoFoglaltsag <= 0)
        {
            // Utolsó foglalt szakasz elhagyása
            VegeTimer.Interval = TavolsagInterval(UtolsoSzakaszElfoglaltHossz);
            VegeTimer.Start();
        }
        else
        {
            // Utolsó foglalt szakasz foglalt marad
            UtolsoSzakaszElfoglaltHossz = utolsoSzakaszraJutoFoglaltsag;
        } 
    }

    private void VegeTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        // Régi utolsó felszabadítása
        Szakasz felszabadulo = GetUtolsoSzakasz();
        felszabadulo.Felszabadit(this);
        Szakaszok.Remove(felszabadulo);
        // Új utolsó ekkor teljesen foglalt
        UtolsoSzakaszElfoglaltHossz = GetUtolsoSzakasz().Hossz;
        // Továbblépés
        VegeTimerLeptet();
    }

    private void ElejeTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        Tovabblep();
    }

    private double TavolsagInterval(int hossz)
    {
        double sebessegMpS = MaxSebesseg / 3.6;
        double idoS = hossz / sebessegMpS;
        return idoS * 1000;
    }

    private void Vagany_MenesztesChanged(object? sender, EventArgs e)
    {
        Vagany vagany = (sender as Vagany)!;
        if (vagany.Menesztes)
        {
            vagany.MenesztesChanged -= Vagany_MenesztesChanged;
            Tovabblep();
        }
    }

    private void MegalljFojelzo_JelzesChanged(object? sender, EventArgs e)
    {
        Fojelzo fojelzo = (sender as Fojelzo)!;
        MaxSebesseg = fojelzo.Jelzes.Sebesseg();
        if (fojelzo.Jelzes != Sebessegjelzes.Megallj)
        {
            fojelzo.JelzesChanged -= MegalljFojelzo_JelzesChanged;
            Tovabblep();
        }
    }

    private Szakasz GetUtolsoSzakasz()
        => Szakaszok[^1];

    public override string ToString() => Nev;
}
