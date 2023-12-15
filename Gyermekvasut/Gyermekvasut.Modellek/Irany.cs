using Gyermekvasut.Modellek.VonatNS;

namespace Gyermekvasut.Modellek;

public enum Irany
{
    KezdopontFele = 1,
    VegpontFele = 2
}

public static class IranyExtensions
{
    public static Irany Fordit(this Irany irany)
    {
        return irany switch
        {
            Irany.KezdopontFele => Irany.VegpontFele,
            Irany.VegpontFele => Irany.KezdopontFele,
            _ => throw new NotImplementedException()
        };
    }

    public static VonatIrany ToVonatIrany(this Irany irany)
    {
        return irany switch
        {
            Irany.KezdopontFele => VonatIrany.Paratlan,
            Irany.VegpontFele => VonatIrany.Paros,
            _ => throw new NotImplementedException()
        };
    }
}