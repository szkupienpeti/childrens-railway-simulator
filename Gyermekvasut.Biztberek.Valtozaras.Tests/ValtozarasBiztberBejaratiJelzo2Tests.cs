using Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Palya;

namespace Gyermekvasut.Biztberek.Valtozaras.Tests;

[TestClass]
public class ValtozarasBiztberBejaratiJelzo2Tests : ValtozarasBiztberTestBase
{
    [TestMethod]
    [DynamicData(nameof(ValtozarasBiztberAllomasOldalakData), typeof(ValtozarasBiztberTestBase))]
    public void BejaratiJelzo2_WhenAlaphelyzet_ShouldNemAllithato(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        CreateBiztber(allomasNev);
        // Act
        var eredmenyek = BejaratiJelzo2AllitasKiserlet(irany);
        // Assert
        AssertSikertelenAllitasEredmeny(EmeltyuAllitasEredmeny.NemAllithatoSzerkezetileg, eredmenyek);
    }

    [TestMethod]
    [DynamicData(nameof(ValtozarasBiztberAllomasOldalakData), typeof(ValtozarasBiztberTestBase))]
    public void BejaratiJelzo2_WhenEgyenesBejarat_ShouldNemAllithato(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        CreateBiztber(allomasNev);
        BejaratiVaganyutBeallit(irany, ValtoAllas.Egyenes);
        BejaratiJelzoAllit(irany, ValtoAllas.Egyenes);
        // Act
        var eredmenyek = BejaratiJelzo2AllitasKiserlet(irany);
        // Assert
        AssertSikertelenAllitasEredmeny(EmeltyuAllitasEredmeny.NemAllithatoSzerkezetileg, eredmenyek);
    }

    [TestMethod]
    [DynamicData(nameof(ValtozarasBiztberAllomasOldalakData), typeof(ValtozarasBiztberTestBase))]
    public void BejaratiJelzo2_WhenKiteroBejarat_ShouldAtallitva(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        CreateBiztber(allomasNev);
        BejaratiVaganyutBeallit(irany, ValtoAllas.Kitero);
        // Act
        var eredmenyek = BejaratiJelzo2AllitasKiserlet(irany);
        // Assert
        AssertSikeresAllitasEredmeny(eredmenyek);
    }

    [TestMethod]
    [DynamicData(nameof(ValtozarasBiztberAllomasOldalakData), typeof(ValtozarasBiztberTestBase))]
    public void BejaratiJelzo2Vissza_WhenFelNemHasznaltKiteroBejarat_ShouldAllitasMegtagadva(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        CreateBiztber(allomasNev);
        BejaratiVaganyutBeallit(irany, ValtoAllas.Kitero);
        BejaratiJelzoAllit(irany, ValtoAllas.Kitero);
        // Act
        var eredmenyek = BejaratiJelzo2AllitasKiserlet(irany);
        // Assert
        AssertSikertelenAllitasEredmeny(EmeltyuAllitasEredmeny.AllitasMegtagadvaKoraiVisszavetel, eredmenyek);
    }

    [TestMethod]
    [DynamicData(nameof(ValtozarasBiztberAllomasOldalakData), typeof(ValtozarasBiztberTestBase))]
    public void BejaratiJelzo2Vissza_WhenFelhasznaltKiteroBejarat_ShouldAtallitva(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        CreateBiztber(allomasNev);
        BejaratiVaganyutBeallit(irany, ValtoAllas.Kitero);
        BejaratiJelzoAllit(irany, ValtoAllas.Kitero);
        VonatBehalad(irany);
        // Act
        var eredmenyek = BejaratiJelzo2AllitasKiserlet(irany);
        // Assert
        AssertSikeresAllitasEredmeny(eredmenyek);
    }

    private EmeltyuAllitasEredmenyek BejaratiJelzo2AllitasKiserlet(Irany irany)
    {
        var bejaratiJelzoEmeltyu2 = GetBejaratiJelzoEmeltyu2(irany);
        var biztberEredmeny = Biztber.BejaratiJelzoEmeltyu2AllitasKiserlet(bejaratiJelzoEmeltyu2);
        var allitasEredmeny = bejaratiJelzoEmeltyu2.AllitasiKiserlet(Biztber);
        return new(biztberEredmeny, allitasEredmeny);
    }
}
