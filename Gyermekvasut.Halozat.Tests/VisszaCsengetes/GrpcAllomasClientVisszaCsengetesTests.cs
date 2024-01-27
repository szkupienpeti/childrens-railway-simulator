using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.VisszaCsengetes;

[TestClass]
public class GrpcAllomasClientVisszaCsengetesTests : RealHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a szomszédos GrpcAllomasClient objektumon VisszaCsengetes() függvényt hívunk,
    /// lezajlik a hálózaton a <i>tényleges</i> gRPC kommunikáció, és meghívódik
    /// a GrpcAllomasServer VisszaCsengetes() függvénye, megfelelő tartalmú VisszaCsengetesRequest objektummal
    /// <br />
    /// Ez egy integrációs teszt.
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ClientVisszaCsengetes_ShouldCallSomszedServer(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        AllomasEsSzomszedClientFelepit(allomasNev);
        VisszaCsengetesRequest? requestReceived = null;
        GrpcServerMock
            .Setup(server => server.VisszaCsengetes(It.IsAny<VisszaCsengetesRequest>(), It.IsAny<ServerCallContext>()))
            .Callback<VisszaCsengetesRequest, ServerCallContext>((req, _) => requestReceived = req);
        // Act
        var requestToSend = HalozatTestsUtil.CreateBejovoVisszaCsengetesRequest(allomasNev, irany);
        SzomszedClient.VisszaCsengetes(requestToSend);
        // Assert
        Assert.IsNotNull(requestReceived);
        Assert.AreEqual(requestToSend, requestReceived);
    }
}
