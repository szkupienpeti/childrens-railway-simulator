using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.CsengetesNS;

[TestClass]
public class GrpcAllomasClientCsengetesTests : RealHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a szomszédos GrpcAllomasClient objektumon Csengetes() függvényt hívunk,
    /// lezajlik a hálózaton a <i>tényleges</i> gRPC kommunikáció, és meghívódik
    /// a GrpcAllomasServer Csengetes() függvénye, megfelelő tartalmú CsengetesRequest objektummal
    /// <br />
    /// Ez egy integrációs teszt.
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ClientCsengetes_ShouldCallSomszedServer(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        AllomasEsSzomszedClientFelepit(allomasNev);
        CsengetesRequest? requestReceived = null;
        GrpcServerMock
            .Setup(server => server.Csengetes(It.IsAny<CsengetesRequest>(), It.IsAny<ServerCallContext>()))
            .Callback<CsengetesRequest, ServerCallContext>((req, _) => requestReceived = req);
        // Act
        var requestToSend = HalozatTestsUtil.CreateBejovoCsengetesRequest(allomasNev, irany);
        SzomszedClient.Csengetes(requestToSend);
        // Assert
        Assert.IsNotNull(requestReceived);
        Assert.AreEqual(requestToSend, requestReceived);
    }
}
