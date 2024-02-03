using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.VonatNS;
using Gyermekvasut.Modellek;

namespace Gyermekvasut.Tests.Util;

public static class VonatTestsUtil
{
    public static readonly string PARATLAN_VONATSZAM = "TEST_1";
    public static readonly string PAROS_VONATSZAM = "TEST_2";
    public static readonly string MASIK_VONATSZAM = "X_TEST_MASIK";
    public static readonly string GEP_NEV = "TEST_Mk45";
    public static readonly Jarmu[] SZERELVENY = new Jarmu[] { new(GEP_NEV, JarmuTipus.Mk45) };
    public static readonly Dictionary<Irany, TestVonatInfo> VONAT_INFOS = new()
    {
        { Irany.KezdopontFele, new(PARATLAN_VONATSZAM, VonatIrany.Paratlan, true) },
        { Irany.VegpontFele, new(PAROS_VONATSZAM, VonatIrany.Paros, true) },
    };

    public static TestVonatInfo GetErkezoVonatInfo(Irany allomasOldal)
        => VONAT_INFOS[allomasOldal.Fordit()];

    public static TestVonatInfo GetInduloVonatInfo(Irany allomasOldal)
        => VONAT_INFOS[allomasOldal];

    public static readonly TimeOnly TEST_IDO = new(9, 10);
    public static readonly string TEST_NEV = "TEST_NEV";

    public static Vonat CreateAndLehelyezTestVonat(Irany vonatIrany, Szakasz allomaskoz, string? vonatszam = null)
    {
        var vonat = CreateTestVonat(vonatIrany, vonatszam);
        vonat.Lehelyez(allomaskoz);
        return vonat;
    }

    public static Vonat CreateInduloTestVonat(Irany allomasOldal, string? vonatszam = null)
        => CreateTestVonat(allomasOldal, vonatszam);

    public static Vonat CreateErkezoTestVonat(Irany allomasOldal, string? vonatszam = null)
        => CreateTestVonat(allomasOldal.Fordit(), vonatszam);

    public static Vonat CreateTestVonat(Irany vonatIrany, string? vonatszam = null)
    {
        var vonatInfo = VONAT_INFOS[vonatIrany];
        var menetrendek = new[] { vonatInfo.Menetrend };
        var vonatszamToUse = vonatszam ?? vonatInfo.Vonatszam;
        var vonat = new Vonat(vonatszamToUse, vonatIrany, SZERELVENY, menetrendek);
        return vonat;
    }
}

public record TestVonatInfo(string Vonatszam, Menetrend Menetrend)
{
    public TestVonatInfo(string vonatszam, VonatIrany vonatIrany, bool koruljar)
        : this(vonatszam, new Menetrend(vonatszam, vonatIrany, koruljar))
    { }
}
