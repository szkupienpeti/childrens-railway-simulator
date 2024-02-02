using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek;
using Moq;
using Gyermekvasut.Grpc;
using Grpc.Core;
using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Halozat.Tests.VonatAllomaskozbolKilep;

[TestClass]
public class HalozatiAllomasVonatAllomaskozbolKilepTests : MockHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a HalozatiAllomas objektumon VonatotAllomaskozbolKileptet() függvényt hívunk, akkor az meghívja
    /// a megfelelő irányú kliens VonatAllomaskozbolKilep() függvényét, megfelelő tartalmú VonatAllomaskozbolKilepRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void AllomasVonatotAllomaskozbolKileptet_WhenHasSzomszed_ShouldCallClient(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        VonatAllomaskozbolKilepRequest? grpcRequest = null;
        GetMockClient(irany)
            .Setup(client => client.VonatAllomaskozbolKilep(It.IsAny<VonatAllomaskozbolKilepRequest>(), null, null, default))
            .Callback<VonatAllomaskozbolKilepRequest, Metadata, DateTime?, CancellationToken>((req, _, _, _) => grpcRequest = req);
        // Act
        var vonatInfo = VonatTestsUtil.GetErkezoVonat(irany);
        var vonatszam = vonatInfo.Vonatszam;
        Allomas.VonatotAllomaskozolKileptet(irany, vonatszam);
        // Assert
        GetMockClient(irany).Verify(a => a.VonatAllomaskozbolKilep(It.IsAny<VonatAllomaskozbolKilepRequest>(), null, null, default), Times.Once());
        Assert.IsNotNull(grpcRequest);
        Assert.AreEqual(allomasNev, GrpcToModelMapper.MapAllomasNev(grpcRequest.Kuldo));
        Assert.AreEqual(vonatszam, grpcRequest.Vonatszam);
    }
}
