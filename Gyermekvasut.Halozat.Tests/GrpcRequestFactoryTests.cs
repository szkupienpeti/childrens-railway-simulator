using Gyermekvasut.Halozat.Factory;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public class GrpcRequestFactoryTests
{
    [TestMethod]
    public void CreateEngedelyKeresRequest_WhenHasUtolsoVonatAndTipusIsAzonosIranyu_ShouldThrow()
    {
        // Arrange
        var allomasNev = AllomasNev.Szechenyihegy;
        var utolsoVonat = VonatTestsUtil.PAROS_VONATSZAM;
        var vonatszam = VonatTestsUtil.PARATLAN_VONATSZAM;
        var ido = TelefonTestsUtil.TEST_IDO;
        var nev = TelefonTestsUtil.TEST_NEV;
        // Act and assert
        var exception = Assert.ThrowsException<ArgumentException>(() => GrpcRequestFactory.CreateEngedelyKeresRequest(allomasNev,
            EngedelyKeresTipus.AzonosIranyuVolt, utolsoVonat, vonatszam, ido, nev));
        Assert.AreEqual("Az utolsó vonatnak pontosan akkor kell hiányoznia, ha azonos irányú az engedélykérés", exception.Message);
    }

    [DataTestMethod]
    [DataRow(EngedelyKeresTipus.EllenkezoIranyuVolt)]
    [DataRow(EngedelyKeresTipus.EllenkezoIranyuVan)]
    public void CreateEngedelyKeresRequest_WhenNoUtolsoVonatAndTipusIsEllenkezoIranyu_ShouldThrow(EngedelyKeresTipus ellenkezoIranyuEnedelyKeresTipus)
    {
        // Arrange
        var allomasNev = AllomasNev.Szechenyihegy;
        var vonatszam = VonatTestsUtil.PARATLAN_VONATSZAM;
        var ido = TelefonTestsUtil.TEST_IDO;
        var nev = TelefonTestsUtil.TEST_NEV;
        // Act and assert
        var exception = Assert.ThrowsException<ArgumentException>(() => GrpcRequestFactory.CreateEngedelyKeresRequest(allomasNev,
            ellenkezoIranyuEnedelyKeresTipus, null, vonatszam, ido, nev));
        Assert.AreEqual("Az utolsó vonatnak pontosan akkor kell hiányoznia, ha azonos irányú az engedélykérés", exception.Message);
    }

    [TestMethod]
    public void CreateEngedelyAdasRequest_WhenHasUtolsoVonatAndTipusIsAzonosIranyu_ShouldThrow()
    {
        // Arrange
        var allomasNev = AllomasNev.Szechenyihegy;
        var utolsoVonat = VonatTestsUtil.PAROS_VONATSZAM;
        var vonatszam = VonatTestsUtil.PARATLAN_VONATSZAM;
        var nev = TelefonTestsUtil.TEST_NEV;
        // Act and assert
        var exception = Assert.ThrowsException<ArgumentException>(
            () => GrpcRequestFactory.CreateEngedelyAdasRequest(allomasNev, EngedelyAdasTipus.AzonosIranyu, utolsoVonat, vonatszam, nev));
        Assert.AreEqual("Az utolsó vonatnak pontosan akkor kell hiányoznia, ha azonos irányú az engedélyadás", exception.Message);
    }

    [TestMethod]
    public void CreateEngedelyAdasRequest_WhenNoUtolsoVonatAndTipusIsEllenkezoIranyu_ShouldThrow()
    {
        // Arrange
        var allomasNev = AllomasNev.Szechenyihegy;
        string vonatszam = VonatTestsUtil.PARATLAN_VONATSZAM;
        string nev = TelefonTestsUtil.TEST_NEV;
        // Act and assert
        var exception = Assert.ThrowsException<ArgumentException>(
            () => GrpcRequestFactory.CreateEngedelyAdasRequest(allomasNev, EngedelyAdasTipus.EllenkezoIranyu, null, vonatszam, nev));
        Assert.AreEqual("Az utolsó vonatnak pontosan akkor kell hiányoznia, ha azonos irányú az engedélyadás", exception.Message);
    }
}
