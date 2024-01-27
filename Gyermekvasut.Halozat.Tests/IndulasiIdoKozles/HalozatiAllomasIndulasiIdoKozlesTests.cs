using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek;
using Moq;
using Gyermekvasut.Grpc;
using Grpc.Core;
using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Halozat.Tests.IndulasiIdoKozles;

[TestClass]
public class HalozatiAllomasIndulasiIdoKozlesTests : MockHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a HalozatiAllomas objektumon IndulasiIdotKozol() függvényt hívunk, akkor az meghívja
    /// a megfelelő irányú kliens IndulasiIdoKozles() függvényét, megfelelő tartalmú IndulasiIdoKozlesRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void AllomasIndulasiIdotKozol_WhenHasSzomszed_ShouldCallClient(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        IndulasiIdoKozlesRequest? grpcRequest = null;
        GetMockSzomszedClient(irany)
            .Setup(client => client.IndulasiIdoKozles(It.IsAny<IndulasiIdoKozlesRequest>(), null, null, default))
            .Callback<IndulasiIdoKozlesRequest, Metadata, DateTime?, CancellationToken>((req, _, _, _) => grpcRequest = req);
        // Act
        var vonatInfo = VonatTestsUtil.GetInduloVonat(irany);
        var vonatszam = vonatInfo.Vonatszam;
        var ido = VonatTestsUtil.TEST_IDO;
        var nev = VonatTestsUtil.TEST_NEV;
        Allomas.IndulasiIdotKozol(irany, vonatszam, ido, nev);
        // Assert
        GetMockSzomszedClient(irany).Verify(a => a.IndulasiIdoKozles(It.IsAny<IndulasiIdoKozlesRequest>(), null, null, default), Times.Once());
        Assert.IsNotNull(grpcRequest);
        Assert.AreEqual(allomasNev, GrpcToModelMapper.MapAllomasNev(grpcRequest.Kuldo));
        Assert.AreEqual(vonatszam, grpcRequest.Vonatszam);
        Assert.AreEqual(ido, GrpcToModelMapper.MapIdo(grpcRequest.Ido));
        Assert.AreEqual(nev, grpcRequest.Nev);
    }
}
