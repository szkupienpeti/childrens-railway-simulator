using Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Palya;

namespace Gyermekvasut.Biztberek.Valtozaras.Tests;

[TestClass]
public class ValtozarasBiztberBejaratiJelzo1Tests : ValtozarasBiztberTestBase
{
    [TestMethod]
    [DynamicData(nameof(ValtozarasBiztberAllomasOldalakData), typeof(ValtozarasBiztberTestBase))]
    public void BejaratiJelzo1_WhenAlaphelyzet_ShouldNemAllithato(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        CreateBiztber(allomasNev);
        // Act
        var eredmenyek = BejaratiJelzo1AllitasKiserlet(irany);
        // Assert
        AssertSikertelenAllitasEredmeny(EmeltyuAllitasEredmeny.NemAllithatoSzerkezetileg, eredmenyek);
    }

    [TestMethod]
    [DynamicData(nameof(ValtozarasBiztberAllomasOldalakData), typeof(ValtozarasBiztberTestBase))]
    public void BejaratiJelzo1_WhenKiteroBejarat_ShouldNemAllithato(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        CreateBiztber(allomasNev);
        BejaratiVaganyutBeallit(irany, ValtoAllas.Kitero);
        BejaratiJelzoAllit(irany, ValtoAllas.Kitero);
        // Act
        var eredmenyek = BejaratiJelzo1AllitasKiserlet(irany);
        // Assert
        AssertSikertelenAllitasEredmeny(EmeltyuAllitasEredmeny.NemAllithatoSzerkezetileg, eredmenyek);
    }

    [TestMethod]
    [DynamicData(nameof(ValtozarasBiztberAllomasOldalakData), typeof(ValtozarasBiztberTestBase))]
    public void BejaratiJelzo1_WhenEgyenesBejarat_ShouldAtallitva(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        CreateBiztber(allomasNev);
        BejaratiVaganyutBeallit(irany, ValtoAllas.Egyenes);
        // Act
        var eredmenyek = BejaratiJelzo1AllitasKiserlet(irany);
        // Assert
        AssertSikeresAllitasEredmeny(eredmenyek);
    }

    [TestMethod]
    [DynamicData(nameof(ValtozarasBiztberAllomasOldalakData), typeof(ValtozarasBiztberTestBase))]
    public void BejaratiJelzo1Vissza_WhenElojelzoNincsVisszaveve_ShouldNemAllithato(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        CreateBiztber(allomasNev);
        BejaratiVaganyutBeallit(irany, ValtoAllas.Egyenes);
        BejaratiJelzoAllit(irany, ValtoAllas.Egyenes);
        ElojelzoAllit(irany);
        // Act
        var eredmenyek = BejaratiJelzo1AllitasKiserlet(irany);
        // Assert
        AssertSikertelenAllitasEredmeny(EmeltyuAllitasEredmeny.NemAllithatoSzerkezetileg, eredmenyek);
    }

    [TestMethod]
    [DynamicData(nameof(ValtozarasBiztberAllomasOldalakData), typeof(ValtozarasBiztberTestBase))]
    public void BejaratiJelzo1Vissza_WhenFelNemHasznaltEgyenesBejarat_ShouldAllitasMegtagadva(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        CreateBiztber(allomasNev);
        BejaratiVaganyutBeallit(irany, ValtoAllas.Egyenes);
        BejaratiJelzoAllit(irany, ValtoAllas.Egyenes);
        // Act
        var eredmenyek = BejaratiJelzo1AllitasKiserlet(irany);
        // Assert
        AssertSikertelenAllitasEredmeny(EmeltyuAllitasEredmeny.AllitasMegtagadvaKoraiVisszavetel, eredmenyek);
    }

    [TestMethod]
    [DynamicData(nameof(ValtozarasBiztberAllomasOldalakData), typeof(ValtozarasBiztberTestBase))]
    public void BejaratiJelzo1Vissza_WhenFelhasznaltEgyenesBejaratElojelzovel_ShouldAtallitva(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        CreateBiztber(allomasNev);
        BejaratiVaganyutBeallit(irany, ValtoAllas.Egyenes);
        BejaratiJelzoAllit(irany, ValtoAllas.Egyenes);
        ElojelzoAllit(irany);
        VonatBehalad(irany);
        ElojelzoAllit(irany);
        // Act
        var eredmenyek = BejaratiJelzo1AllitasKiserlet(irany);
        // Assert
        AssertSikeresAllitasEredmeny(eredmenyek);
    }

    [TestMethod]
    [DynamicData(nameof(ValtozarasBiztberAllomasOldalakData), typeof(ValtozarasBiztberTestBase))]
    public void BejaratiJelzo1Vissza_WhenFelhasznaltEgyenesBejaratElojelzoNelkul_ShouldAtallitva(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        CreateBiztber(allomasNev);
        BejaratiVaganyutBeallit(irany, ValtoAllas.Egyenes);
        BejaratiJelzoAllit(irany, ValtoAllas.Egyenes);
        VonatBehalad(irany);
        // Act
        var eredmenyek = BejaratiJelzo1AllitasKiserlet(irany);
        // Assert
        AssertSikeresAllitasEredmeny(eredmenyek);
    }

    private EmeltyuAllitasEredmenyek BejaratiJelzo1AllitasKiserlet(Irany irany)
    {
        var bejaratiJelzoEmeltyu1 = GetBejaratiJelzoEmeltyu1(irany);
        var biztberEredmeny = Biztber.BejaratiJelzoEmeltyu1AllitasKiserlet(bejaratiJelzoEmeltyu1);
        var allitasEredmeny = bejaratiJelzoEmeltyu1.AllitasiKiserlet(Biztber);
        return new(biztberEredmeny, allitasEredmeny);
    }
}
