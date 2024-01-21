using Gyermekvasut.Grpc;
using Gyermekvasut.Halozat.Factory;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.Telefon;

namespace Gyermekvasut.Halozat.Tests.CsengetesNS;
internal class CsengetesTestsUtil
{
    private static readonly List<Csengetes> EGY_HOSSZU = new() { Csengetes.Hosszu };
    private static readonly List<Csengetes> KET_HOSSZU = new() { Csengetes.Hosszu, Csengetes.Hosszu };

    public static CsengetesRequest CreateBejovoCsengetesRequest(AllomasNev allomasNev, Irany irany)
    {
        var kuldo = allomasNev.Szomszed(irany)!.Value;
        var csengetesek = GetBejovoCsengetes(irany);
        return GrpcRequestFactory.CreateCsengetesRequest(kuldo, csengetesek);
    }

    public static List<Csengetes> GetBejovoCsengetes(Irany irany)
        => GetKimenoCsengetes(irany.Fordit());

    public static List<Csengetes> GetKimenoCsengetes(Irany irany)
        => irany switch
        {
            Irany.KezdopontFele => EGY_HOSSZU,
            Irany.VegpontFele => KET_HOSSZU,
            _ => throw new NotImplementedException()
        };
}
