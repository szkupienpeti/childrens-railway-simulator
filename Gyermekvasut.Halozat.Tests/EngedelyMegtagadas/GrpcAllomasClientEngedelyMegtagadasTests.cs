using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.EngedelyMegtagadas;

[TestClass]
public class GrpcAllomasClientEngedelyMegtagadasTests : RealHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a szomszédos GrpcAllomasClient objektumon EngedelyMegtagadas() függvényt hívunk,
    /// lezajlik a hálózaton a <i>tényleges</i> gRPC kommunikáció, és meghívódik
    /// a GrpcAllomasServer EngedelyMegtagadas() függvénye, megfelelő tartalmú EngedelyMegtagadasRequest objektummal
    /// <br />
    /// Ez egy integrációs teszt.
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ClientEngedelyMegtagadas_ShouldCallSomszedServer(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        AllomasEsSzomszedClientFelepit(allomasNev);
        EngedelyMegtagadasRequest? requestReceived = null;
        GrpcServerMock
            .Setup(server => server.EngedelyMegtagadas(It.IsAny<EngedelyMegtagadasRequest>(), It.IsAny<ServerCallContext>()))
            .Callback<EngedelyMegtagadasRequest, ServerCallContext>((req, _) => requestReceived = req);
        // Act
        var requestToSend = HalozatTestsUtil.CreateBejovoEngedelyMegtagadasRequest(allomasNev, irany);
        SzomszedClient.EngedelyMegtagadas(requestToSend);
        // Assert
        Assert.IsNotNull(requestReceived);
        Assert.AreEqual(requestToSend, requestReceived);
    }
}
