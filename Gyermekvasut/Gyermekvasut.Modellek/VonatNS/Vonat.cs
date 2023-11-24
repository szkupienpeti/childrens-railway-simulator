using Gyermekvasut.Modellek.Palya;

namespace Gyermekvasut.Modellek.VonatNS;

public class Vonat : Szerelveny
{
    public bool Koruljar { get; }
    public List<Menetrend> Menetrendek { get; } = new();
    private Menetrend? kovetkezoMenetrend = null;

    public Menetrend? AktualisMenetrend { get; set; }
    public bool VeglegFeloszlott
        => AktualisMenetrend == null && kovetkezoMenetrend == null;
    public override string Nev
        => AktualisMenetrend != null
        ? AktualisMenetrend.Vonatszam
        : base.Nev;

    public Vonat(string nev, Irany irany, Szakasz szakasz, Jarmu[] jarmuvek, Menetrend[] menetrendek)
        : base(nev, irany, szakasz, jarmuvek)
    {
        foreach (var menetrend in menetrendek)
        {
            Menetrendek.Add(menetrend);
        }
        AktualisMenetrend = Menetrendek[0];
        IranyKonzisztenciaCheck();
    }

    public Jarmu? Feloszlik()
    {
        MaxSebesseg = TOLATAS_SEBESSEG;

        int aktualisMenetrendIndex = Menetrendek.IndexOf(AktualisMenetrend!);
        AktualisMenetrend = null;
        if (aktualisMenetrendIndex == Menetrendek.Count - 1)
        {
            // Utolsó menetrend vége
            return null;
        }

        kovetkezoMenetrend = Menetrendek[aktualisMenetrendIndex + 1];
        if (Koruljar)
        {
            // Gép leakad
            Jarmu gep = Jarmuvek[0];
            Jarmuvek.Remove(gep);
            return gep;
        }
        else
        {
            // Körüljárás nélkül fordul
            Fordul();
            return null;
        }
    }

    public void Visszacsatol(Jarmu gep)
    {
        Fordul();
        Jarmuvek.Insert(0, gep);
    }

    private void Fordul()
    {
        AktualisMenetrend = kovetkezoMenetrend;
        kovetkezoMenetrend = null;
        Irany = Irany.Fordit();
        Jarmuvek.Reverse();
        IranyKonzisztenciaCheck();
    }

    private void IranyKonzisztenciaCheck()
    {
        if (AktualisMenetrend!.Irany != Irany)
        {
            throw new ArgumentException("A szerelvény irány és a menetrendi irány nem egyeznek meg.");
        }
    }
}
