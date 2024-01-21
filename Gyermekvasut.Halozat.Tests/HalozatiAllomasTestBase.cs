using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Modellek.VonatNS;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public abstract class HalozatiAllomasTestBase
{
    private static readonly string PARATLAN_VONATSZAM = "TEST_1";
    private static readonly string PAROS_VONATSZAM = "TEST_2";
    private static readonly string GEP_NEV = "TEST_Mk45";
    private static readonly Jarmu[] SZERELVENY = new Jarmu[] { new(GEP_NEV, JarmuTipus.Mk45) };
    protected static readonly Dictionary<Irany, TestVonatInfo> VONAT_INFOS = new()
    {
        { Irany.KezdopontFele, new(PARATLAN_VONATSZAM, VonatIrany.Paratlan, true) },
        { Irany.VegpontFele, new(PAROS_VONATSZAM, VonatIrany.Paros, true) },
    };

    protected static readonly TimeOnly TEST_IDO = new(9, 10);
    protected static readonly string TEST_NEV = "TEST_NEV";

    protected HalozatiAllomas? _allomas;
    protected HalozatiAllomas Allomas => _allomas!;

    [TestCleanup]
    public virtual void TestCleanup()
    {
        StopIfNotNull(_allomas);
    }

    protected static void StopIfNotNull(HalozatiAllomas? allomas)
    {
        allomas?.Stop();
    }

    protected Vonat CreateInduloTestVonatAllomaskozben(Irany allomasOldal)
        => CreateTestVonatAllomaskozben(allomasOldal, allomasOldal);

    protected Vonat CreateErkezoTestVonatAllomaskozben(Irany allomasOldal)
        => CreateTestVonatAllomaskozben(allomasOldal, allomasOldal.Fordit());

    private Vonat CreateTestVonatAllomaskozben(Irany allomasOldal, Irany vonatIrany)
    {
        var allomaskoz = Allomas.Topologia.Allomaskozok[allomasOldal]!;
        return CreateTestVonat(vonatIrany, allomaskoz);
    }

    protected static Vonat CreateTestVonat(Irany vonatIrany, Szakasz allomaskoz)
    {
        var vonat = CreateTestVonat(vonatIrany);
        vonat.Lehelyez(allomaskoz);
        return vonat;
    }

    protected static Vonat CreateTestVonat(Irany vonatIrany)
    {
        var vonatInfo = VONAT_INFOS[vonatIrany];
        var menetrendek = new[] { vonatInfo.Menetrend };
        var vonat = new Vonat(vonatInfo.Vonatszam, vonatIrany, SZERELVENY, menetrendek);
        return vonat;
    }

    protected AllomasNev GetSzomszedAllomasNev(Irany irany)
        => Allomas.AllomasNev.Szomszed(irany)!.Value;
}

public record TestVonatInfo(string Vonatszam, Menetrend Menetrend)
{
    public TestVonatInfo(string vonatszam, VonatIrany vonatIrany, bool koruljar)
        : this(vonatszam, new Menetrend(vonatszam, vonatIrany, koruljar))
    { }
}
