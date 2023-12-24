using Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Biztberek.Valtozaras.Tests;

[TestClass]
public class ValtozarasBiztberTests : ValtozarasBiztberTestBase
{
    [TestMethod]
    [DynamicData(nameof(ValtozarasBiztberAllomasOldalakData), typeof(ValtozarasBiztberTestBase))]
    public void EmeltyuAllasok_WhenAlaphelyzet(AllomasNev allomasNev, Irany irany)
    {
        // Arrange and act
        CreateBiztber(allomasNev);
        // Assert
        Assert.AreEqual(EmeltyuAllas.Also, GetElojelzoEmeltyu(irany).Allas);
        Assert.AreEqual(EmeltyuAllas.Also, GetBejaratiJelzoEmeltyu1(irany).Allas);
        Assert.AreEqual(EmeltyuAllas.Felso, GetBejaratiJelzoEmeltyu2(irany).Allas);
    }
}
