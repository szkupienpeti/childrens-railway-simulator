using Gyermekvasut.Grpc;
using Gyermekvasut.Halozat.Factory;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Halozat.Tests;

internal static class HalozatTestsUtil
{
    public static CsengetesRequest CreateBejovoCsengetesRequest(AllomasNev allomasNev, Irany irany)
    {
        var kuldo = allomasNev.Szomszed(irany)!.Value;
        var csengetesek = TelefonTestsUtil.GetBejovoCsengetes(irany);
        return GrpcRequestFactory.CreateCsengetesRequest(kuldo, csengetesek);
    }

    public static VisszaCsengetesRequest CreateBejovoVisszaCsengetesRequest(AllomasNev allomasNev, Irany irany)
    {
        var kuldo = allomasNev.Szomszed(irany)!.Value;
        var csengetesek = TelefonTestsUtil.GetBejovoCsengetes(irany);
        return GrpcRequestFactory.CreateVisszaCsengetesRequest(kuldo, csengetesek);
    }

    public static IndulasiIdoKozlesRequest CreateBejovoIndulasiIdoKozlesRequest(AllomasNev allomasNev, Irany irany)
    {
        var kuldo = allomasNev.Szomszed(irany)!.Value;
        var vonatInfo = VonatTestsUtil.GetErkezoVonat(irany);
        return GrpcRequestFactory.CreateIndulasiIdoKozlesRequest(kuldo, vonatInfo.Vonatszam, VonatTestsUtil.TEST_IDO, VonatTestsUtil.TEST_NEV);
    }
}
