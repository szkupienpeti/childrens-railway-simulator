using Gyermekvasut.Modellek.VonatNS;

namespace Gyermekvasut.Modellek.Palya;

public abstract class PalyaElem
{
    public string Nev { get; }

    public PalyaElem(string nev)
    {
        Nev = nev;
    }

    public abstract void KpSzomszedolas(PalyaElem kpSzomszed);
    public abstract void VpSzomszedolas(PalyaElem vpSzomszed);
    public abstract PalyaElem? Kovetkezo(Irany irany);

    public override string ToString() => Nev;
}
