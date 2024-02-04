using Gyermekvasut.Grpc;
using Gyermekvasut.Halozat.Factory;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Tests.Util;
using Gyermekvasut.Modellek.VonatNS;
using Gyermekvasut.Modellek.Telefon;

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

    public static EngedelyKeresRequest CreateBejovoAzonosIranyuEngedelyKeresRequest(AllomasNev allomasNev, Irany irany)
    {
        var kuldo = allomasNev.Szomszed(irany)!.Value;
        var vonatInfo = VonatTestsUtil.GetErkezoVonatInfo(irany);
        return GrpcRequestFactory.CreateEngedelyKeresRequest(kuldo, EngedelyKeresTipus.AzonosIranyuVolt, null, vonatInfo.Vonatszam, VonatTestsUtil.TEST_IDO, VonatTestsUtil.TEST_NEV);
    }

    public static EngedelyKeresRequest CreateBejovoEllenkezoIranyuVoltEngedelyKeresRequest(AllomasNev allomasNev, Irany irany)
        => CreateBejovoEllenkezoIranyuEngedelyKeresRequest(allomasNev, irany, EngedelyKeresTipus.EllenkezoIranyuVolt);

    public static EngedelyKeresRequest CreateBejovoEllenkezoIranyuVanEngedelyKeresRequest(AllomasNev allomasNev, Irany irany)
        => CreateBejovoEllenkezoIranyuEngedelyKeresRequest(allomasNev, irany, EngedelyKeresTipus.EllenkezoIranyuVan);

    private static EngedelyKeresRequest CreateBejovoEllenkezoIranyuEngedelyKeresRequest(AllomasNev allomasNev, Irany irany, EngedelyKeresTipus engedelyKeresTipus)
    {
        if (engedelyKeresTipus == EngedelyKeresTipus.AzonosIranyuVolt)
        {
            throw new ArgumentException("Azonos irányú engedélykérés-típushoz nem lehet ellenkező irányú engedélykérést létrehozni.");
        }
        var kuldo = allomasNev.Szomszed(irany)!.Value;
        var vonatInfo = VonatTestsUtil.GetErkezoVonatInfo(irany);
        var ellenvonatInfo = VonatTestsUtil.GetInduloVonatInfo(irany);
        return GrpcRequestFactory.CreateEngedelyKeresRequest(kuldo, engedelyKeresTipus,
            ellenvonatInfo.Vonatszam, vonatInfo.Vonatszam, VonatTestsUtil.TEST_IDO, VonatTestsUtil.TEST_NEV);
    }

    public static EngedelyAdasRequest CreateBejovoAzonosIranyuEngedelyAdasRequest(AllomasNev allomasNev, Irany irany)
    {
        var kuldo = allomasNev.Szomszed(irany)!.Value;
        var vonatInfo = VonatTestsUtil.GetInduloVonatInfo(irany);
        return GrpcRequestFactory.CreateEngedelyAdasRequest(kuldo, EngedelyAdasTipus.AzonosIranyu, null, vonatInfo.Vonatszam, VonatTestsUtil.TEST_NEV);
    }

    public static EngedelyAdasRequest CreateBejovoEllenkezoIranyuEngedelyAdasRequest(AllomasNev allomasNev, Irany irany)
    {
        var kuldo = allomasNev.Szomszed(irany)!.Value;
        var vonatInfo = VonatTestsUtil.GetInduloVonatInfo(irany);
        var ellenvonatInfo = VonatTestsUtil.GetErkezoVonatInfo(irany);
        return GrpcRequestFactory.CreateEngedelyAdasRequest(kuldo, EngedelyAdasTipus.EllenkezoIranyu,
            ellenvonatInfo.Vonatszam, vonatInfo.Vonatszam, VonatTestsUtil.TEST_NEV);
    }

    public static IndulasiIdoKozlesRequest CreateBejovoIndulasiIdoKozlesRequest(AllomasNev allomasNev, Irany irany)
    {
        var kuldo = allomasNev.Szomszed(irany)!.Value;
        var vonatInfo = VonatTestsUtil.GetErkezoVonatInfo(irany);
        return GrpcRequestFactory.CreateIndulasiIdoKozlesRequest(kuldo, vonatInfo.Vonatszam, VonatTestsUtil.TEST_IDO, VonatTestsUtil.TEST_NEV);
    }

    public static IndulasiIdoKozlesVetelRequest CreateBejovoIndulasiIdoKozlesVetelRequest(AllomasNev allomasNev, Irany irany)
    {
        var kuldo = allomasNev.Szomszed(irany)!.Value;
        var vonatInfo = VonatTestsUtil.GetInduloVonatInfo(irany);
        return GrpcRequestFactory.CreateIndulasiIdoKozlesVetelRequest(kuldo, vonatInfo.Vonatszam, VonatTestsUtil.TEST_IDO, VonatTestsUtil.TEST_NEV);
    }

    public static VisszajelentesRequest CreateBejovoVisszajelentesRequest(AllomasNev allomasNev, Irany irany)
    {
        var kuldo = allomasNev.Szomszed(irany)!.Value;
        var vonatInfo = VonatTestsUtil.GetInduloVonatInfo(irany);
        return GrpcRequestFactory.CreateVisszajelentesRequest(kuldo, vonatInfo.Vonatszam, VonatTestsUtil.TEST_NEV);
    }

    public static VisszajelentesVetelRequest CreateBejovoVisszajelentesVetelRequest(AllomasNev allomasNev, Irany irany)
    {
        var kuldo = allomasNev.Szomszed(irany)!.Value;
        var vonatInfo = VonatTestsUtil.GetErkezoVonatInfo(irany);
        return GrpcRequestFactory.CreateVisszajelentesVetelRequest(kuldo, vonatInfo.Vonatszam, VonatTestsUtil.TEST_NEV);
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
