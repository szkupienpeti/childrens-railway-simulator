using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.IndulasiIdoKozles;

[TestClass]
public class GrpcAllomasClientIndulasiIdoKozlesTests : RealHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a szomszédos GrpcAllomasClient objektumon IndulasiIdoKozles() függvényt hívunk,
    /// lezajlik a hálózaton a <i>tényleges</i> gRPC kommunikáció, és meghívódik
    /// a GrpcAllomasServer VisszaCsengetes() függvénye, megfelelő tartalmú VisszaCsengetesRequest objektummal
    /// <br />
    /// Ez egy integrációs teszt.
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ClientIndulasiIdoKozles_ShouldCallSomszedServer(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        AllomasEsSzomszedClientFelepit(allomasNev);
        IndulasiIdoKozlesRequest? requestReceived = null;
        GrpcServerMock
            .Setup(server => server.IndulasiIdoKozles(It.IsAny<IndulasiIdoKozlesRequest>(), It.IsAny<ServerCallContext>()))
            .Callback<IndulasiIdoKozlesRequest, ServerCallContext>((req, _) => requestReceived = req);
        // Act
        var requestToSend = HalozatTestsUtil.CreateBejovoIndulasiIdoKozlesRequest(allomasNev, irany);
        SzomszedClient.IndulasiIdoKozles(requestToSend);
        // Assert
        Assert.IsNotNull(requestReceived);
        Assert.AreEqual(requestToSend, requestReceived);
    }
}
