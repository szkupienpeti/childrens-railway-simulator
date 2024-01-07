namespace Gyermekvasut.Modellek.Palya;

public abstract class EgyenesPalyaElem : PalyaElem
{
    private PalyaElem? kpSzomszed;
    private PalyaElem? vpSzomszed;

    protected EgyenesPalyaElem(string nev) : base(nev) { }

    public override void Szomszedolas(Irany irany, PalyaElem szomszed)
    {
        switch (irany)
        {
            case Irany.KezdopontFele:
                kpSzomszed = szomszed;
                break;
            case Irany.VegpontFele:
                vpSzomszed = szomszed;
                break;
            default:
                throw new NotImplementedException();
        }
    }

    public override PalyaElem? Kovetkezo(Irany irany)
    {
        return irany switch
        {
            Irany.KezdopontFele => kpSzomszed,
            Irany.VegpontFele => vpSzomszed,
            _ => throw new NotImplementedException()
        };
    }
}
