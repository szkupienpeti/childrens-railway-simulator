using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek;
using Moq;
using Gyermekvasut.Grpc;
using Grpc.Core;
using Gyermekvasut.Tests.Util;
using Gyermekvasut.Modellek.Telefon;

namespace Gyermekvasut.Halozat.Tests.EngedelyAdas;

[TestClass]
public class HalozatiAllomasEngedelyAdasTests : MockHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a HalozatiAllomas objektumon EngedelytAd() függvényt hívunk (azonos irányú vonat volt útban),
    /// akkor az meghívja a megfelelő irányú kliens EngedelyAdas() függvényét, megfelelő tartalmú EngedelyAdasRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void AllomasEngedelytAd_WhenHasSzomszedAzonosIranyu_ShouldCallClient(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        EngedelyAdasRequest? grpcRequest = null;
        GetMockClient(irany)
            .Setup(client => client.EngedelyAdas(It.IsAny<EngedelyAdasRequest>(), null, null, default))
            .Callback<EngedelyAdasRequest, Metadata, DateTime?, CancellationToken>((req, _, _, _) => grpcRequest = req);
        // Act
        var engedelyAdasTipus = EngedelyAdasTipus.AzonosIranyu;
        var vonatInfo = VonatTestsUtil.GetErkezoVonatInfo(irany);
        var vonatszam = vonatInfo.Vonatszam;
        var nev = TelefonTestsUtil.TEST_NEV;
        Allomas.EngedelytAd(irany, engedelyAdasTipus, null, vonatszam, nev);
        // Assert
        AssertClientCalledWithExpectedRequest(grpcRequest, allomasNev, irany, engedelyAdasTipus, null, vonatszam, nev);
    }

    /// <summary>
    /// Ha a HalozatiAllomas objektumon EngedelytAd() függvényt hívunk (ellenkező irányú vonat volt/van útban),
    /// akkor az meghívja a megfelelő irányú kliens EngedelyAdas() függvényét, megfelelő tartalmú EngedelyAdasRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void AllomasEngedelytAd_WhenHasSzomszedEllenkezoIranyu_ShouldCallClient(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        EngedelyAdasRequest? grpcRequest = null;
        GetMockClient(irany)
            .Setup(client => client.EngedelyAdas(It.IsAny<EngedelyAdasRequest>(), null, null, default))
            .Callback<EngedelyAdasRequest, Metadata, DateTime?, CancellationToken>((req, _, _, _) => grpcRequest = req);
        // Act
        var engedelyAdasTipus = EngedelyAdasTipus.EllenkezoIranyu;
        var ellenvonatInfo = VonatTestsUtil.GetInduloVonatInfo(irany);
        var utolsoVonat = ellenvonatInfo.Vonatszam;
        var vonatInfo = VonatTestsUtil.GetErkezoVonatInfo(irany);
        var vonatszam = vonatInfo.Vonatszam;
        var nev = TelefonTestsUtil.TEST_NEV;
        Allomas.EngedelytAd(irany, engedelyAdasTipus, utolsoVonat, vonatszam, nev);
        // Assert
        AssertClientCalledWithExpectedRequest(grpcRequest, allomasNev, irany, engedelyAdasTipus, utolsoVonat, vonatszam, nev);
    }

    private void AssertClientCalledWithExpectedRequest(EngedelyAdasRequest? grpcRequest, AllomasNev allomasNev, Irany irany,
        EngedelyAdasTipus engedelyAdasTipus, string? utolsoVonat, string vonatszam, string nev)
    {
        GetMockClient(irany).Verify(a => a.EngedelyAdas(It.IsAny<EngedelyAdasRequest>(), null, null, default), Times.Once());
        Assert.IsNotNull(grpcRequest);
        Assert.AreEqual(allomasNev, GrpcToModelMapper.MapAllomasNev(grpcRequest.Kuldo));
        Assert.AreEqual(engedelyAdasTipus, GrpcToModelMapper.MapEngedelyAdasTipus(grpcRequest.Tipus));
        if (utolsoVonat == null)
        {
            Assert.IsFalse(grpcRequest.HasUtolsoVonat);
        }
        else
        {
            Assert.AreEqual(utolsoVonat, grpcRequest.UtolsoVonat);
        }
        Assert.AreEqual(vonatszam, grpcRequest.Vonatszam);
        Assert.AreEqual(nev, grpcRequest.Nev);
    }
}
