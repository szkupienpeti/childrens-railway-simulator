using Gyermekvasut.Grpc;
using Gyermekvasut.Halozat.Factory;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Tests.Util;
using Gyermekvasut.Modellek.VonatNS;

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
        var vonatInfo = VonatTestsUtil.GetErkezoVonatInfo(irany);
        return GrpcRequestFactory.CreateIndulasiIdoKozlesRequest(kuldo, vonatInfo.Vonatszam, VonatTestsUtil.TEST_IDO, VonatTestsUtil.TEST_NEV);
    }

    public static VonatAllomaskozbolKilepRequest CreateBejovoVonatAllomaskozbolKilepRequest(AllomasNev allomasNev, Irany irany)
    {
        var vonatInfo = VonatTestsUtil.GetInduloVonatInfo(irany);
        return CreateBejovoVonatAllomaskozbolKilepRequest(allomasNev, irany, vonatInfo.Vonatszam);
    }

    public static VonatAllomaskozbolKilepRequest CreateElteroBejovoVonatAllomaskozbolKilepRequest(AllomasNev allomasNev, Irany irany)
        => CreateBejovoVonatAllomaskozbolKilepRequest(allomasNev, irany, VonatTestsUtil.MASIK_VONATSZAM);

    private static VonatAllomaskozbolKilepRequest CreateBejovoVonatAllomaskozbolKilepRequest(AllomasNev allomasNev, Irany irany, string vonatszam)
    {
        var kuldo = allomasNev.Szomszed(irany)!.Value;
        return GrpcRequestFactory.CreateVonatAllomaskozbolKilepRequest(kuldo, vonatszam);
    }

    public static VonatAllomaskozbeBelepRequest CreateBejovoVonatAllomaskozbeBelepRequest(AllomasNev allomasNev, Irany irany)
    {
        var vonat = VonatTestsUtil.CreateErkezoTestVonat(irany);
        return CreateBejovoVonatAllomaskozbeBelepRequest(allomasNev, irany, vonat);
    }

    public static VonatAllomaskozbeBelepRequest CreateBejovoVonatAllomaskozbeBelepRequest(AllomasNev allomasNev, Irany irany, Vonat vonat)
    {
        var kuldo = allomasNev.Szomszed(irany)!.Value;
        return GrpcRequestFactory.CreateVonatAllomaskozbeBelepRequest(kuldo, vonat);
    }
}
