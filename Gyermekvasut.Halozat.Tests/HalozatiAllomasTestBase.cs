using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.VonatNS;
using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public abstract class HalozatiAllomasTestBase
{
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
        var vonat = VonatTestsUtil.CreateTestVonat(vonatIrany, allomaskoz);
        return vonat;
    }

    protected AllomasNev GetSzomszedAllomasNev(Irany irany)
        => Allomas.AllomasNev.Szomszed(irany)!.Value;
}
