using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek;
using Moq;
using Gyermekvasut.Grpc;
using Grpc.Core;
using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Halozat.Tests.VisszajelentesVetel;

[TestClass]
public class HalozatiAllomasVisszajelentesVetelTests : MockHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a HalozatiAllomas objektumon VisszajelentestVesz() függvényt hívunk, akkor az meghívja
    /// a megfelelő irányú kliens VisszajelentesVetel() függvényét, megfelelő tartalmú VisszajelentesVetelRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void AllomasVisszajelentestVesz_WhenHasSzomszed_ShouldCallClient(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        VisszajelentesVetelRequest? grpcRequest = null;
        GetMockClient(irany)
            .Setup(client => client.VisszajelentesVetel(It.IsAny<VisszajelentesVetelRequest>(), null, null, default))
            .Callback<VisszajelentesVetelRequest, Metadata, DateTime?, CancellationToken>((req, _, _, _) => grpcRequest = req);
        // Act
        var vonatInfo = VonatTestsUtil.GetInduloVonatInfo(irany);
        var vonatszam = vonatInfo.Vonatszam;
        var nev = TelefonTestsUtil.TEST_NEV;
        Allomas.VisszajelentestVesz(irany, vonatszam, nev);
        // Assert
        GetMockClient(irany).Verify(a => a.VisszajelentesVetel(It.IsAny<VisszajelentesVetelRequest>(), null, null, default), Times.Once());
        Assert.IsNotNull(grpcRequest);
        Assert.AreEqual(allomasNev, GrpcToModelMapper.MapAllomasNev(grpcRequest.Kuldo));
        Assert.AreEqual(vonatszam, grpcRequest.Vonatszam);
        Assert.AreEqual(nev, grpcRequest.Nev);
    }
}
