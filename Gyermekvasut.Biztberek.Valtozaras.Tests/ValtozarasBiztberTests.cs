using Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Biztberek.Valtozaras.Tests;

[TestClass]
public class ValtozarasBiztberTests
{
    private static ValtozarasBiztber CreateBiztber(AllomasNev allomasNev)
    {
        Allomas allomas = new(allomasNev);
        ValtozarasBiztberFactory factory = new(allomas);
        return factory.Create();
    }

    [TestMethod]
    public void ElojelzoAllitasiKiserlet_Alaphelyzet_NemAllithatoSzerkezetileg(Irany irany)
    {
        // Arrange
        var biztber = CreateBiztber(AllomasNev.Janoshegy);
        var emeltyuCsoport = biztber.EmeltyuCsoportok[irany];
        var elojelzoEmeltyu = emeltyuCsoport.ElojelzoEmeltyu;
        // Act
        var allitasEredmeny = biztber.ElojelzoEmeltyuAllitasKiserlet(elojelzoEmeltyu);
        // Assert
        Assert.AreEqual(EmeltyuAllitasEredmeny.NemAllithatoSzerkezetileg, allitasEredmeny);
    }
}
