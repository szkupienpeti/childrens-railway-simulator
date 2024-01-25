using Gyermekvasut.Grpc;
using Gyermekvasut.Halozat.Factory;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.Telefon;

namespace Gyermekvasut.Halozat.Tests.VisszaCsengetesNS;
internal class VisszaCsengetesTestsUtil
{
    private static readonly List<Csengetes> EGY_HOSSZU = new() { Csengetes.Hosszu };
    private static readonly List<Csengetes> KET_HOSSZU = new() { Csengetes.Hosszu, Csengetes.Hosszu };

    public static VisszaCsengetesRequest CreateBejovoVisszaCsengetesRequest(AllomasNev allomasNev, Irany irany)
    {
        var kuldo = allomasNev.Szomszed(irany)!.Value;
        var csengetesek = GetBejovoVisszaCsengetes(irany);
        return GrpcRequestFactory.CreateVisszaCsengetesRequest(kuldo, csengetesek);
    }

    public static List<Csengetes> GetBejovoVisszaCsengetes(Irany irany)
        => GetKimenoVisszaCsengetes(irany.Fordit());

    public static List<Csengetes> GetKimenoVisszaCsengetes(Irany irany)
        => irany switch
        {
            Irany.KezdopontFele => KET_HOSSZU,
            Irany.VegpontFele => EGY_HOSSZU,
            _ => throw new NotImplementedException()
        };
}
