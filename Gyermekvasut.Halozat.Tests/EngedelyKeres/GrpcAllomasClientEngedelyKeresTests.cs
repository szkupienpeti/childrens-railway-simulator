using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.EngedelyKeres;

[TestClass]
public class GrpcAllomasClientEngedelyKeresTests : RealHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a szomszédos GrpcAllomasClient objektumon EngedelyKeres() függvényt hívunk (azonos irányú vonat volt útban),
    /// lezajlik a hálózaton a <i>tényleges</i> gRPC kommunikáció, és meghívódik
    /// a GrpcAllomasServer EngedelyKeres() függvénye, megfelelő tartalmú EngedelyKeresRequest objektummal
    /// <br />
    /// Ez egy integrációs teszt.
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ClientEngedelyKeres_WhenAzonosIranyu_ShouldCallSomszedServer(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        AllomasEsSzomszedClientFelepit(allomasNev);
        EngedelyKeresRequest? requestReceived = null;
        GrpcServerMock
            .Setup(server => server.EngedelyKeres(It.IsAny<EngedelyKeresRequest>(), It.IsAny<ServerCallContext>()))
            .Callback<EngedelyKeresRequest, ServerCallContext>((req, _) => requestReceived = req);
        // Act
        var requestToSend = HalozatTestsUtil.CreateBejovoAzonosIranyuEngedelyKeresRequest(allomasNev, irany);
        SzomszedClient.EngedelyKeres(requestToSend);
        // Assert
        Assert.IsNotNull(requestReceived);
        Assert.AreEqual(requestToSend, requestReceived);
    }

    /// <summary>
    /// Ha a szomszédos GrpcAllomasClient objektumon EngedelyKeres() függvényt hívunk (ellenkező irányú vonat volt útban),
    /// lezajlik a hálózaton a <i>tényleges</i> gRPC kommunikáció, és meghívódik
    /// a GrpcAllomasServer EngedelyKeres() függvénye, megfelelő tartalmú EngedelyKeresRequest objektummal
    /// <br />
    /// Ez egy integrációs teszt.
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ClientEngedelyKeres_WhenEllenkezoIranyuVolt_ShouldCallSomszedServer(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        AllomasEsSzomszedClientFelepit(allomasNev);
        EngedelyKeresRequest? requestReceived = null;
        GrpcServerMock
            .Setup(server => server.EngedelyKeres(It.IsAny<EngedelyKeresRequest>(), It.IsAny<ServerCallContext>()))
            .Callback<EngedelyKeresRequest, ServerCallContext>((req, _) => requestReceived = req);
        // Act
        var requestToSend = HalozatTestsUtil.CreateBejovoEllenkezoIranyuVoltEngedelyKeresRequest(allomasNev, irany);
        SzomszedClient.EngedelyKeres(requestToSend);
        // Assert
        Assert.IsNotNull(requestReceived);
        Assert.AreEqual(requestToSend, requestReceived);
    }

    /// <summary>
    /// Ha a szomszédos GrpcAllomasClient objektumon EngedelyKeres() függvényt hívunk (ellenkező irányú vonat van útban),
    /// lezajlik a hálózaton a <i>tényleges</i> gRPC kommunikáció, és meghívódik
    /// a GrpcAllomasServer EngedelyKeres() függvénye, megfelelő tartalmú EngedelyKeresRequest objektummal
    /// <br />
    /// Ez egy integrációs teszt.
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ClientEngedelyKeres_WhenEllenkezoIranyuVan_ShouldCallSomszedServer(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        AllomasEsSzomszedClientFelepit(allomasNev);
        EngedelyKeresRequest? requestReceived = null;
        GrpcServerMock
            .Setup(server => server.EngedelyKeres(It.IsAny<EngedelyKeresRequest>(), It.IsAny<ServerCallContext>()))
            .Callback<EngedelyKeresRequest, ServerCallContext>((req, _) => requestReceived = req);
        // Act
        var requestToSend = HalozatTestsUtil.CreateBejovoEllenkezoIranyuVanEngedelyKeresRequest(allomasNev, irany);
        SzomszedClient.EngedelyKeres(requestToSend);
        // Assert
        Assert.IsNotNull(requestReceived);
        Assert.AreEqual(requestToSend, requestReceived);
    }
}
