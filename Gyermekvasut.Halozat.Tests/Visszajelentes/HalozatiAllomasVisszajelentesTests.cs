using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek;
using Moq;
using Gyermekvasut.Grpc;
using Grpc.Core;
using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Halozat.Tests.Visszajelentes;

[TestClass]
public class HalozatiAllomasVisszajelentesTests : MockHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a HalozatiAllomas objektumon Visszajelent() függvényt hívunk, akkor az meghívja
    /// a megfelelő irányú kliens Visszajelentes() függvényét, megfelelő tartalmú VisszajelentesRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void AllomasVisszajelent_WhenHasSzomszed_ShouldCallClient(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        VisszajelentesRequest? grpcRequest = null;
        GetMockClient(irany)
            .Setup(client => client.Visszajelentes(It.IsAny<VisszajelentesRequest>(), null, null, default))
            .Callback<VisszajelentesRequest, Metadata, DateTime?, CancellationToken>((req, _, _, _) => grpcRequest = req);
        // Act
        var vonatInfo = VonatTestsUtil.GetErkezoVonatInfo(irany);
        var vonatszam = vonatInfo.Vonatszam;
        var nev = TelefonTestsUtil.TEST_NEV;
        Allomas.Visszajelent(irany, vonatszam, nev);
        // Assert
        GetMockClient(irany).Verify(a => a.Visszajelentes(It.IsAny<VisszajelentesRequest>(), null, null, default), Times.Once());
        Assert.IsNotNull(grpcRequest);
        Assert.AreEqual(allomasNev, GrpcToModelMapper.MapAllomasNev(grpcRequest.Kuldo));
        Assert.AreEqual(vonatszam, grpcRequest.Vonatszam);
        Assert.AreEqual(nev, grpcRequest.Nev);
    }
}
