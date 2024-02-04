using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek;
using Moq;
using Gyermekvasut.Grpc;
using Grpc.Core;
using Gyermekvasut.Tests.Util;
using Gyermekvasut.Modellek.Telefon;

namespace Gyermekvasut.Halozat.Tests.EngedelyKeres;

[TestClass]
public class HalozatiAllomasEngedelyKeresTests : MockHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a HalozatiAllomas objektumon EngedelytKer() függvényt hívunk (azonos irányú vonat volt útban),
    /// akkor az meghívja a megfelelő irányú kliens EngedelyKeres() függvényét, megfelelő tartalmú EngedelyKeresRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void AllomasEngedelytKer_WhenHasSzomszedAzonosIranyu_ShouldCallClient(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        EngedelyKeresRequest? grpcRequest = null;
        GetMockClient(irany)
            .Setup(client => client.EngedelyKeres(It.IsAny<EngedelyKeresRequest>(), null, null, default))
            .Callback<EngedelyKeresRequest, Metadata, DateTime?, CancellationToken>((req, _, _, _) => grpcRequest = req);
        // Act
        var engedelyKeresTipus = EngedelyKeresTipus.AzonosIranyuVolt;
        var vonatInfo = VonatTestsUtil.GetInduloVonatInfo(irany);
        var vonatszam = vonatInfo.Vonatszam;
        var ido = TelefonTestsUtil.TEST_IDO;
        var nev = TelefonTestsUtil.TEST_NEV;
        Allomas.EngedelytKer(irany, engedelyKeresTipus, null, vonatszam, ido, nev);
        // Assert
        AssertClientCalledWithExpectedRequest(grpcRequest, allomasNev, irany, engedelyKeresTipus, null, vonatszam, ido, nev);
    }

    /// <summary>
    /// Ha a HalozatiAllomas objektumon EngedelytKer() függvényt hívunk (ellenkező irányú vonat volt útban),
    /// akkor az meghívja a megfelelő irányú kliens EngedelyKeres() függvényét, megfelelő tartalmú EngedelyKeresRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void AllomasEngedelytKer_WhenHasSzomszedEllenkezoIranyuVolt_ShouldCallClient(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        EngedelyKeresRequest? grpcRequest = null;
        GetMockClient(irany)
            .Setup(client => client.EngedelyKeres(It.IsAny<EngedelyKeresRequest>(), null, null, default))
            .Callback<EngedelyKeresRequest, Metadata, DateTime?, CancellationToken>((req, _, _, _) => grpcRequest = req);
        // Act
        var engedelyKeresTipus = EngedelyKeresTipus.EllenkezoIranyuVolt;
        var ellenvonatInfo = VonatTestsUtil.GetErkezoVonatInfo(irany);
        var utolsoVonat = ellenvonatInfo.Vonatszam;
        var vonatInfo = VonatTestsUtil.GetInduloVonatInfo(irany);
        var vonatszam = vonatInfo.Vonatszam;
        var ido = TelefonTestsUtil.TEST_IDO;
        var nev = TelefonTestsUtil.TEST_NEV;
        Allomas.EngedelytKer(irany, engedelyKeresTipus, utolsoVonat, vonatszam, ido, nev);
        // Assert
        AssertClientCalledWithExpectedRequest(grpcRequest, allomasNev, irany, engedelyKeresTipus, utolsoVonat, vonatszam, ido, nev);
    }

    /// <summary>
    /// Ha a HalozatiAllomas objektumon EngedelytKer() függvényt hívunk (ellenkező irányú vonat van útban),
    /// akkor az meghívja a megfelelő irányú kliens EngedelyKeres() függvényét, megfelelő tartalmú EngedelyKeresRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void AllomasEngedelytKer_WhenHasSzomszedEllenkezoIranyuVan_ShouldCallClient(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        EngedelyKeresRequest? grpcRequest = null;
        GetMockClient(irany)
            .Setup(client => client.EngedelyKeres(It.IsAny<EngedelyKeresRequest>(), null, null, default))
            .Callback<EngedelyKeresRequest, Metadata, DateTime?, CancellationToken>((req, _, _, _) => grpcRequest = req);
        // Act
        var engedelyKeresTipus = EngedelyKeresTipus.EllenkezoIranyuVan;
        var ellenvonatInfo = VonatTestsUtil.GetErkezoVonatInfo(irany);
        var utolsoVonat = ellenvonatInfo.Vonatszam;
        var vonatInfo = VonatTestsUtil.GetInduloVonatInfo(irany);
        var vonatszam = vonatInfo.Vonatszam;
        var ido = TelefonTestsUtil.TEST_IDO;
        var nev = TelefonTestsUtil.TEST_NEV;
        Allomas.EngedelytKer(irany, engedelyKeresTipus, utolsoVonat, vonatszam, ido, nev);
        // Assert
        AssertClientCalledWithExpectedRequest(grpcRequest, allomasNev, irany, engedelyKeresTipus, utolsoVonat, vonatszam, ido, nev);
    }

    private void AssertClientCalledWithExpectedRequest(EngedelyKeresRequest? grpcRequest, AllomasNev allomasNev, Irany irany,
        EngedelyKeresTipus engedelyKeresTipus, string? utolsoVonat, string vonatszam, TimeOnly ido, string nev)
    {
        GetMockClient(irany).Verify(a => a.EngedelyKeres(It.IsAny<EngedelyKeresRequest>(), null, null, default), Times.Once());
        Assert.IsNotNull(grpcRequest);
        Assert.AreEqual(allomasNev, GrpcToModelMapper.MapAllomasNev(grpcRequest.Kuldo));
        Assert.AreEqual(engedelyKeresTipus, GrpcToModelMapper.MapEngedelyKeresTipus(grpcRequest.Tipus));
        if (utolsoVonat == null)
        {
            Assert.IsFalse(grpcRequest.HasUtolsoVonat);
        }
        else
        {
            Assert.AreEqual(utolsoVonat, grpcRequest.UtolsoVonat);
        }
        Assert.AreEqual(vonatszam, grpcRequest.Vonatszam);
        Assert.AreEqual(ido, GrpcToModelMapper.MapIdo(grpcRequest.Ido));
        Assert.AreEqual(nev, grpcRequest.Nev);
    }
}
