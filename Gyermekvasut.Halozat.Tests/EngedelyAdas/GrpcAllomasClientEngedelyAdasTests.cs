using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.EngedelyAdas;

[TestClass]
public class GrpcAllomasClientEngedelyAdasTests : RealHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a szomszédos GrpcAllomasClient objektumon EngedelyAdas() függvényt hívunk (azonos irányú vonat volt útban),
    /// lezajlik a hálózaton a <i>tényleges</i> gRPC kommunikáció, és meghívódik
    /// a GrpcAllomasServer EngedelyAdas() függvénye, megfelelő tartalmú EngedelyAdasRequest objektummal
    /// <br />
    /// Ez egy integrációs teszt.
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ClientEngedelyAdas_WhenAzonosIranyu_ShouldCallSomszedServer(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        AllomasEsSzomszedClientFelepit(allomasNev);
        EngedelyAdasRequest? requestReceived = null;
        GrpcServerMock
            .Setup(server => server.EngedelyAdas(It.IsAny<EngedelyAdasRequest>(), It.IsAny<ServerCallContext>()))
            .Callback<EngedelyAdasRequest, ServerCallContext>((req, _) => requestReceived = req);
        // Act
        var requestToSend = HalozatTestsUtil.CreateBejovoAzonosIranyuEngedelyAdasRequest(allomasNev, irany);
        SzomszedClient.EngedelyAdas(requestToSend);
        // Assert
        Assert.IsNotNull(requestReceived);
        Assert.AreEqual(requestToSend, requestReceived);
    }

    /// <summary>
    /// Ha a szomszédos GrpcAllomasClient objektumon EngedelyAdas() függvényt hívunk (ellenkező irányú vonat volt/van útban),
    /// lezajlik a hálózaton a <i>tényleges</i> gRPC kommunikáció, és meghívódik
    /// a GrpcAllomasServer EngedelyAdas() függvénye, megfelelő tartalmú EngedelyAdasRequest objektummal
    /// <br />
    /// Ez egy integrációs teszt.
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ClientEngedelyAdas_WhenEllenkezoIranyu_ShouldCallSomszedServer(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        AllomasEsSzomszedClientFelepit(allomasNev);
        EngedelyAdasRequest? requestReceived = null;
        GrpcServerMock
            .Setup(server => server.EngedelyAdas(It.IsAny<EngedelyAdasRequest>(), It.IsAny<ServerCallContext>()))
            .Callback<EngedelyAdasRequest, ServerCallContext>((req, _) => requestReceived = req);
        // Act
        var requestToSend = HalozatTestsUtil.CreateBejovoEllenkezoIranyuEngedelyAdasRequest(allomasNev, irany);
        SzomszedClient.EngedelyAdas(requestToSend);
        // Assert
        Assert.IsNotNull(requestReceived);
        Assert.AreEqual(requestToSend, requestReceived);
    }
}
