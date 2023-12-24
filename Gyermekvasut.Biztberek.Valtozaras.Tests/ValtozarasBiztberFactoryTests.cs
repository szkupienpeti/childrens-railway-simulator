using Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Biztberek.Valtozaras.Tests;

[TestClass]
public class ValtozarasBiztberFactoryTests : SzimulaciosTestBase
{
    [DataTestMethod]
    [DataRow(AllomasNev.Viragvolgy)]
    [DataRow(AllomasNev.Janoshegy)]
    public void Create_Alaphelyzet_Alaphelyzet(AllomasNev allomasNev)
    {
        // Arrange
        var allomas = new Allomas(allomasNev);
        var factory = new ValtozarasBiztberFactory(allomas);
        // Act
        var biztber = factory.Create();
        // Assert
        foreach (var irany in Enum.GetValues<Irany>())
        {
            AssertValtozarasEmeltyuCsoportAlaphelyzet(biztber.EmeltyuCsoportok[irany]);
        }
    }

    private static void AssertValtozarasEmeltyuCsoportAlaphelyzet(ValtozarasEmeltyuCsoport valtozarasEmeltyuCsoport)
    {
        Assert.IsNull(valtozarasEmeltyuCsoport.ValtozarKulcsTarolo.ValtozarKulcs);
        Assert.AreEqual(EmeltyuAllas.Also, valtozarasEmeltyuCsoport.ElojelzoEmeltyu.Allas);
        Assert.AreEqual(EmeltyuAllas.Also, valtozarasEmeltyuCsoport.BejaratiJelzoEmeltyu1.Allas);
        Assert.AreEqual(EmeltyuAllas.Felso, valtozarasEmeltyuCsoport.BejaratiJelzoEmeltyu2.Allas);
    }
}
