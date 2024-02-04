using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek;
using Moq;
using Gyermekvasut.Grpc;
using Grpc.Core;
using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Halozat.Tests.IndulasiIdoKozlesVetel;

[TestClass]
public class HalozatiAllomasIndulasiIdoKozlesVetelTests : MockHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a HalozatiAllomas objektumon IndulasiIdoKozlestVesz() függvényt hívunk, akkor az meghívja
    /// a megfelelő irányú kliens IndulasiIdoKozlesVetel() függvényét, megfelelő tartalmú IndulasiIdoKozlesVetelRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void AllomasIndulasiIdoKozlestVesz_WhenHasSzomszed_ShouldCallClient(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        IndulasiIdoKozlesVetelRequest? grpcRequest = null;
        GetMockClient(irany)
            .Setup(client => client.IndulasiIdoKozlesVetel(It.IsAny<IndulasiIdoKozlesVetelRequest>(), null, null, default))
            .Callback<IndulasiIdoKozlesVetelRequest, Metadata, DateTime?, CancellationToken>((req, _, _, _) => grpcRequest = req);
        // Act
        var vonatInfo = VonatTestsUtil.GetErkezoVonatInfo(irany);
        var vonatszam = vonatInfo.Vonatszam;
        var ido = VonatTestsUtil.TEST_IDO;
        var nev = VonatTestsUtil.TEST_NEV;
        Allomas.IndulasiIdoKozlestVesz(irany, vonatszam, ido, nev);
        // Assert
        GetMockClient(irany).Verify(a => a.IndulasiIdoKozlesVetel(It.IsAny<IndulasiIdoKozlesVetelRequest>(), null, null, default), Times.Once());
        Assert.IsNotNull(grpcRequest);
        Assert.AreEqual(allomasNev, GrpcToModelMapper.MapAllomasNev(grpcRequest.Kuldo));
        Assert.AreEqual(vonatszam, grpcRequest.Vonatszam);
        Assert.AreEqual(ido, GrpcToModelMapper.MapIdo(grpcRequest.Ido));
        Assert.AreEqual(nev, grpcRequest.Nev);
    }
}
