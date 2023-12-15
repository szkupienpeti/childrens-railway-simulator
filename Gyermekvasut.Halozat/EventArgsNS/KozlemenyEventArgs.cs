using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Halozat.EventArgsNS;
public abstract class KozlemenyEventArgs : TelefonEventArgs
{
    public string Vonatszam { get; }
    public string Nev { get; }

    public KozlemenyEventArgs(AllomasNev kuldo, string vonatszam, string nev) : base(kuldo)
    {
        Vonatszam = vonatszam;
        Nev = nev;
    }
}
