using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Halozat.EventArgsNS;
public abstract class KozlemenyEventArgs : HalozatiAllomasEventArgs
{
    public string Vonatszam { get; }
    public string Nev { get; }

    protected KozlemenyEventArgs(AllomasNev kuldo, string vonatszam, string nev) : base(kuldo)
    {
        Vonatszam = vonatszam;
        Nev = nev;
    }
}
