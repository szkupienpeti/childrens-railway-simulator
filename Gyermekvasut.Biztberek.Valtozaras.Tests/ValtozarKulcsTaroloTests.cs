using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Biztberek.Valtozaras.Tests;

[TestClass]
public class ValtozarKulcsTaroloTests : SzimulaciosTestBase
{
    private static Valto CreateValto(ValtoAllas valtoAllas, ValtoLezaras valtoLezaras)
        => new("", Irany.KezdopontFele, ValtoTajolas.Balos, valtoAllas, valtoLezaras, new(1,0));

    [TestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.ValtoAllasValues), typeof(DynamicTestDataUtil))]
    public void KulcsBetesz_WhenTaroloUres_ShouldLezar(ValtoAllas valtoAllas)
    {
        // Arrange
        var valto = CreateValto(valtoAllas, ValtoLezaras.Feloldva);
        var tarolo = new ValtozarKulcsTarolo(valto, null);
        // Act
        tarolo.ValtozarKulcsBetesz(valtoAllas);
        // Assert
        Assert.AreEqual(ValtoLezaras.Lezarva, valto.Lezaras);
        Assert.AreEqual(valtoAllas, tarolo.ValtozarKulcs!.Value);
    }

    [TestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.ValtoAllasValues), typeof(DynamicTestDataUtil))]
    public void KulcsBetesz_WhenTaroloTele_ShouldThrow(ValtoAllas valtoAllas)
    {
        // Arrange
        var valto = CreateValto(valtoAllas, ValtoLezaras.Lezarva);
        var tarolo = new ValtozarKulcsTarolo(valto, valtoAllas);
        // Act and assert
        var exception = Assert.ThrowsException<ArgumentException>(() => tarolo.ValtozarKulcsBetesz(valtoAllas));
        Assert.AreEqual($"A(z) {valtoAllas} nem tehetõ be, mert már bent van a(z) {valtoAllas}", exception.Message);
    }

    [DataTestMethod]
    [DataRow(ValtoAllas.Egyenes, ValtoAllas.Kitero)]
    [DataRow(ValtoAllas.Kitero, ValtoAllas.Egyenes)]
    public void KulcsBetesz_WhenAllasElter_ShouldThrow(ValtoAllas valtoAllas, ValtoAllas valtozarKulcs)
    {
        // Arrange
        var valto = CreateValto(valtoAllas, ValtoLezaras.Feloldva);
        var tarolo = new ValtozarKulcsTarolo(valto, null);
        // Act and assert
        var exception = Assert.ThrowsException<InvalidOperationException>(() => tarolo.ValtozarKulcsBetesz(valtozarKulcs));
        Assert.AreEqual($"A váltó {valtoAllas} végállásban van, de {valtozarKulcs} állásba próbálták lezárni", exception.Message);
    }

    [TestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.ValtoAllasValues), typeof(DynamicTestDataUtil))]
    public void KulcsKivesz_WhenTaroloTele_ShouldFelold(ValtoAllas valtoAllas)
    {
        // Arrange
        var valto = CreateValto(valtoAllas, ValtoLezaras.Lezarva);
        var tarolo = new ValtozarKulcsTarolo(valto, valtoAllas);
        // Act
        var valtozarKulcs = tarolo.ValtozarKulcsKivesz();
        // Assert
        Assert.AreEqual(ValtoLezaras.Feloldva, valto.Lezaras);
        Assert.AreEqual(valtoAllas, valtozarKulcs);
    }

    [TestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.ValtoAllasValues), typeof(DynamicTestDataUtil))]
    public void KulcsKivesz_WhenTaroloUres_ShouldThrow(ValtoAllas valtoAllas)
    {
        // Arrange
        var valto = CreateValto(valtoAllas, ValtoLezaras.Feloldva);
        var tarolo = new ValtozarKulcsTarolo(valto, null);
        // Act and assert
        var exception = Assert.ThrowsException<InvalidOperationException>(() => tarolo.ValtozarKulcsKivesz());
        Assert.AreEqual("Nincs bent váltózárkulcs", exception.Message);
    }
}