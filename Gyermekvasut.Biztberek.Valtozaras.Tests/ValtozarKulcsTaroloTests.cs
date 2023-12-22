using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.Palya;

namespace Gyermekvasut.Biztberek.Valtozaras.Tests;

[TestClass]
public class ValtozarKulcsTaroloTests
{
    private static Valto CreateValto(ValtoAllas valtoAllas, ValtoLezaras valtoLezaras)
        => new("", Irany.KezdopontFele, ValtoTajolas.Balos, 1, valtoAllas, valtoLezaras, new(1,0));

    [DataTestMethod]
    [DataRow(ValtoAllas.Egyenes)]
    [DataRow(ValtoAllas.Kitero)]
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

    [DataTestMethod]
    [DataRow(ValtoAllas.Egyenes)]
    [DataRow(ValtoAllas.Kitero)]
    public void KulcsBetesz_WhenTaroloTele_ShouldThrow(ValtoAllas valtoAllas)
    {
        // Arrange
        var valto = CreateValto(valtoAllas, ValtoLezaras.Lezarva);
        var tarolo = new ValtozarKulcsTarolo(valto, valtoAllas);
        // Act and assert
        Assert.ThrowsException<ArgumentException>(() => tarolo.ValtozarKulcsBetesz(valtoAllas),
            $"A(z) {valtoAllas} nem tehet� be, mert m�r bent van a(z) {valtoAllas}");
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
        Assert.ThrowsException<InvalidOperationException>(() => tarolo.ValtozarKulcsBetesz(valtozarKulcs),
            $"A v�lt� {valtoAllas} v�g�ll�sban van, de {valtozarKulcs} �ll�sba pr�b�lt�k lez�rni");
    }

    [DataTestMethod]
    [DataRow(ValtoAllas.Egyenes)]
    [DataRow(ValtoAllas.Kitero)]
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

    [DataTestMethod]
    [DataRow(ValtoAllas.Egyenes)]
    [DataRow(ValtoAllas.Kitero)]
    public void KulcsKivesz_WhenTaroloUres_ShouldThrow(ValtoAllas valtoAllas)
    {
        // Arrange
        var valto = CreateValto(valtoAllas, ValtoLezaras.Feloldva);
        var tarolo = new ValtozarKulcsTarolo(valto, null);
        // Act and assert
        Assert.ThrowsException<InvalidOperationException>(() => tarolo.ValtozarKulcsKivesz(),
            "Nincs bent v�lt�z�rkulcs");
    }
}