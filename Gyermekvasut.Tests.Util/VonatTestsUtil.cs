using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.VonatNS;
using Gyermekvasut.Modellek;

namespace Gyermekvasut.Tests.Util;

public static class VonatTestsUtil
{
    public static readonly string PARATLAN_VONATSZAM = "TEST_1";
    public static readonly string PAROS_VONATSZAM = "TEST_2";
    public static readonly string GEP_NEV = "TEST_Mk45";
    public static readonly Jarmu[] SZERELVENY = new Jarmu[] { new(GEP_NEV, JarmuTipus.Mk45) };
    public static readonly Dictionary<Irany, TestVonatInfo> VONAT_INFOS = new()
    {
        { Irany.KezdopontFele, new(PARATLAN_VONATSZAM, VonatIrany.Paratlan, true) },
        { Irany.VegpontFele, new(PAROS_VONATSZAM, VonatIrany.Paros, true) },
    };

    public static TestVonatInfo GetErkezoVonat(Irany irany)
        => VONAT_INFOS[irany.Fordit()];

    public static TestVonatInfo GetInduloVonat(Irany irany)
        => VONAT_INFOS[irany];

    public static readonly TimeOnly TEST_IDO = new(9, 10);
    public static readonly string TEST_NEV = "TEST_NEV";

    public static Vonat CreateTestVonat(Irany vonatIrany, Szakasz allomaskoz)
    {
        var vonat = CreateTestVonat(vonatIrany);
        vonat.Lehelyez(allomaskoz);
        return vonat;
    }

    public static Vonat CreateTestVonat(Irany vonatIrany)
    {
        var vonatInfo = VONAT_INFOS[vonatIrany];
        var menetrendek = new[] { vonatInfo.Menetrend };
        var vonat = new Vonat(vonatInfo.Vonatszam, vonatIrany, SZERELVENY, menetrendek);
        return vonat;
    }
}

public record TestVonatInfo(string Vonatszam, Menetrend Menetrend)
{
    public TestVonatInfo(string vonatszam, VonatIrany vonatIrany, bool koruljar)
        : this(vonatszam, new Menetrend(vonatszam, vonatIrany, koruljar))
    { }
}
