using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek;
using Moq;
using Gyermekvasut.Grpc;
using Grpc.Core;
using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Halozat.Tests.VisszaCsengetes;

[TestClass]
public class HalozatiAllomasVisszaCsengetesTests : MockHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a HalozatiAllomas objektumon VisszaCsenget() függvényt hívunk, akkor az meghívja
    /// a megfelelő irányú kliens VisszaCsengetes() függvényét, megfelelő tartalmú VisszaCsengetesRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void AllomasVisszaCsenget_WhenHasSzomszed_ShouldCallClient(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        VisszaCsengetesRequest? grpcRequest = null;
        GetMockSzomszedClient(irany)
            .Setup(client => client.VisszaCsengetes(It.IsAny<VisszaCsengetesRequest>(), null, null, default))
            .Callback<VisszaCsengetesRequest, Metadata, DateTime?, CancellationToken>((req, _, _, _) => grpcRequest = req);
        // Act
        var csengetesek = TelefonTestsUtil.GetKimenoVisszaCsengetes(irany);
        Allomas.VisszaCsenget(irany, csengetesek);
        // Assert
        GetMockSzomszedClient(irany).Verify(a => a.VisszaCsengetes(It.IsAny<VisszaCsengetesRequest>(), null, null, default), Times.Once());
        Assert.IsNotNull(grpcRequest);
        Assert.AreEqual(allomasNev, GrpcToModelMapper.MapAllomasNev(grpcRequest.Kuldo));
        CollectionAssert.AreEqual(csengetesek, GrpcToModelMapper.MapRepeated(grpcRequest.Csengetesek, GrpcToModelMapper.MapCsengetes));
    }
}
