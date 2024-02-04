using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek;
using Moq;
using Gyermekvasut.Grpc;
using Grpc.Core;
using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Halozat.Tests.EngedelyMegtagadas;

[TestClass]
public class HalozatiAllomasEngedelyMegtagadasTests : MockHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a HalozatiAllomas objektumon EngedelytMegtagad() függvényt hívunk, akkor az meghívja
    /// a megfelelő irányú kliens EngedelyMegtagadas() függvényét, megfelelő tartalmú EngedelyMegtagadasRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void AllomasEngedelytMegtagad_WhenHasSzomszed_ShouldCallClient(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        EngedelyMegtagadasRequest? grpcRequest = null;
        GetMockClient(irany)
            .Setup(client => client.EngedelyMegtagadas(It.IsAny<EngedelyMegtagadasRequest>(), null, null, default))
            .Callback<EngedelyMegtagadasRequest, Metadata, DateTime?, CancellationToken>((req, _, _, _) => grpcRequest = req);
        // Act
        var vonatInfo = VonatTestsUtil.GetErkezoVonatInfo(irany);
        var vonatszam = vonatInfo.Vonatszam;
        var ok = TelefonTestsUtil.TEST_ENGEDELY_MEGTAGADAS_OK;
        var percMulva = TelefonTestsUtil.TEST_ENGEDELY_MEGTAGADAS_PERC_MULVA;
        var nev = TelefonTestsUtil.TEST_NEV;
        Allomas.EngedelytMegtagad(irany, vonatszam, ok, percMulva, nev);
        // Assert
        GetMockClient(irany).Verify(a => a.EngedelyMegtagadas(It.IsAny<EngedelyMegtagadasRequest>(), null, null, default), Times.Once());
        Assert.IsNotNull(grpcRequest);
        Assert.AreEqual(allomasNev, GrpcToModelMapper.MapAllomasNev(grpcRequest.Kuldo));
        Assert.AreEqual(vonatszam, grpcRequest.Vonatszam);
        Assert.AreEqual(ok, grpcRequest.Ok);
        Assert.AreEqual(percMulva, grpcRequest.PercMulva);
        Assert.AreEqual(nev, grpcRequest.Nev);
    }
}
