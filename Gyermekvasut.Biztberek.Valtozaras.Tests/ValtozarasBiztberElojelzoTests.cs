using Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Palya;

namespace Gyermekvasut.Biztberek.Valtozaras.Tests;

[TestClass]
public class ValtozarasBiztberElojelzoTests : ValtozarasBiztberTestBase
{
    [TestMethod]
    [DynamicData(nameof(ValtozarasBiztberAllomasOldalakData), typeof(ValtozarasBiztberTestBase))]
    public void Elojelzo_WhenAlaphelyzet_ShouldNemAllithato(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        CreateBiztber(allomasNev);
        // Act
        var eredmenyek = ElojelzoAllitasKiserlet(irany);
        // Assert
        AssertSikertelenAllitasEredmeny(EmeltyuAllitasEredmeny.NemAllithatoSzerkezetileg, eredmenyek);
    }

    [TestMethod]
    [DynamicData(nameof(ValtozarasBiztberAllomasOldalakData), typeof(ValtozarasBiztberTestBase))]
    public void Elojelzo_WhenKiteroBejarat_ShouldNemAllithato(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        CreateBiztber(allomasNev);
        BejaratiVaganyutBeallit(irany, ValtoAllas.Kitero);
        BejaratiJelzoAllit(irany, ValtoAllas.Kitero);
        // Act
        var eredmenyek = ElojelzoAllitasKiserlet(irany);
        // Assert
        AssertSikertelenAllitasEredmeny(EmeltyuAllitasEredmeny.NemAllithatoSzerkezetileg, eredmenyek);
    }

    [TestMethod]
    [DynamicData(nameof(ValtozarasBiztberAllomasOldalakData), typeof(ValtozarasBiztberTestBase))]
    public void Elojelzo_WhenEgyenesBejarat_ShouldAtallitva(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        CreateBiztber(allomasNev);
        BejaratiVaganyutBeallit(irany, ValtoAllas.Egyenes);
        BejaratiJelzoAllit(irany, ValtoAllas.Egyenes);
        // Act
        var eredmenyek = ElojelzoAllitasKiserlet(irany);
        // Assert
        AssertSikeresAllitasEredmeny(eredmenyek);
    }

    [TestMethod]
    [DynamicData(nameof(ValtozarasBiztberAllomasOldalakData), typeof(ValtozarasBiztberTestBase))]
    public void ElojelzoVissza_WhenFelNemHasznaltEgyenesBejarat_ShouldAllitasMegtagadva(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        CreateBiztber(allomasNev);
        BejaratiVaganyutBeallit(irany, ValtoAllas.Egyenes);
        BejaratiJelzoAllit(irany, ValtoAllas.Egyenes);
        ElojelzoAllit(irany);
        // Act
        var eredmenyek = ElojelzoAllitasKiserlet(irany);
        // Assert
        AssertSikertelenAllitasEredmeny(EmeltyuAllitasEredmeny.AllitasMegtagadvaKoraiVisszavetel, eredmenyek);
    }

    [TestMethod]
    [DynamicData(nameof(ValtozarasBiztberAllomasOldalakData), typeof(ValtozarasBiztberTestBase))]
    public void Elojelzo_WhenFelhasznaltVaganyut_ShouldAllitasMegtagadva(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        CreateBiztber(allomasNev);
        BejaratiVaganyutBeallit(irany, ValtoAllas.Egyenes);
        BejaratiJelzoAllit(irany, ValtoAllas.Egyenes);
        ElojelzoAllit(irany);
        VonatBehalad(irany);
        ElojelzoAllit(irany);
        // Act
        var eredmenyek = ElojelzoAllitasKiserlet(irany);
        // Assert
        AssertSikertelenAllitasEredmeny(EmeltyuAllitasEredmeny.AllitasMegtagadvaFelhasznaltVaganyut, eredmenyek);
    }

    [TestMethod]
    [DynamicData(nameof(ValtozarasBiztberAllomasOldalakData), typeof(ValtozarasBiztberTestBase))]
    public void ElojelzoVissza_WhenFelhasznaltEgyenesBejarat_ShouldAtallitva(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        CreateBiztber(allomasNev);
        BejaratiVaganyutBeallit(irany, ValtoAllas.Egyenes);
        BejaratiJelzoAllit(irany, ValtoAllas.Egyenes);
        ElojelzoAllit(irany);
        VonatBehalad(irany);
        // Act
        var eredmenyek = ElojelzoAllitasKiserlet(irany);
        // Assert
        AssertSikeresAllitasEredmeny(eredmenyek);
    }

    private EmeltyuAllitasEredmenyek ElojelzoAllitasKiserlet(Irany irany)
    {
        var elojelzoEmeltyu = GetElojelzoEmeltyu(irany);
        var biztberEredmeny = Biztber.ElojelzoEmeltyuAllitasKiserlet(elojelzoEmeltyu);
        var allitasEredmeny = elojelzoEmeltyu.AllitasiKiserlet(Biztber);
        return new(biztberEredmeny, allitasEredmeny);
    }
}
