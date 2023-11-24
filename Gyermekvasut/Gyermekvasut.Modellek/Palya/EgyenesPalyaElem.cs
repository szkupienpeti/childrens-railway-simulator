using Gyermekvasut.Modellek.VonatNS;

namespace Gyermekvasut.Modellek.Palya;

public class EgyenesPalyaElem : PalyaElem
{
    private PalyaElem? kpSzomszed = null;
    private PalyaElem? vpSzomszed = null;

    public EgyenesPalyaElem(string nev) : base(nev) { }

    public override void KpSzomszedolas(PalyaElem kpSzomszed)
    {
        this.kpSzomszed = kpSzomszed;
    }
    public override void VpSzomszedolas(PalyaElem vpSzomszed)
    {
        this.vpSzomszed = vpSzomszed;
    }

    public override PalyaElem? Kovetkezo(Irany irany)
    {
        return irany switch
        {
            Irany.Paros => vpSzomszed,
            Irany.Paratlan => kpSzomszed,
            _ => throw new NotImplementedException()
        };
    }
}
