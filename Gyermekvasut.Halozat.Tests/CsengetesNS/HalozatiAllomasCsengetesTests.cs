using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek;
using Moq;
using Gyermekvasut.Grpc;
using Grpc.Core;
using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Halozat.Tests.CsengetesNS;

[TestClass]
public class HalozatiAllomasCsengetesTests : MockHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a HalozatiAllomas objektumon Csenget() függvényt hívunk, akkor az meghívja
    /// a megfelelő irányú kliens Csengetes() függvényét, megfelelő tartalmú CsengetesRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void AllomasCsenget_WhenHasSzomszed_ShouldCallClient(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        CsengetesRequest? grpcRequest = null;
        GetMockClient(irany)
            .Setup(client => client.Csengetes(It.IsAny<CsengetesRequest>(), null, null, default))
            .Callback<CsengetesRequest, Metadata, DateTime?, CancellationToken>((req, _, _, _) => grpcRequest = req);
        // Act
        var csengetesek = TelefonTestsUtil.GetKimenoCsengetes(irany);
        Allomas.Csenget(irany, csengetesek);
        // Assert
        GetMockClient(irany).Verify(a => a.Csengetes(It.IsAny<CsengetesRequest>(), null, null, default), Times.Once());
        Assert.IsNotNull(grpcRequest);
        Assert.AreEqual(allomasNev, GrpcToModelMapper.MapAllomasNev(grpcRequest.Kuldo));
        CollectionAssert.AreEqual(csengetesek, GrpcToModelMapper.MapRepeated(grpcRequest.Csengetesek, GrpcToModelMapper.MapCsengetes));
    }
}
