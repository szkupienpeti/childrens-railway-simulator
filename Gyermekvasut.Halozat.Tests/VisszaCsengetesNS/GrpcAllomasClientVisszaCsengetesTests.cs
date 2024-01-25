using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Halozat.Factory;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.VisszaCsengetesNS;

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
        var eventCapturer = new GrpcRequestEventCapturer<VisszaCsengetesRequest>(handler => Allomas.AllomasServer.GrpcVisszaCsengetesEvent += handler);
        // Act
        var requestToSend = VisszaCsengetesTestsUtil.CreateBejovoVisszaCsengetesRequest(allomasNev, irany);
        SzomszedClient.VisszaCsengetes(requestToSend);
        // Assert
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var requestReceived = eventCapturer.CapturedRequest!;
        Assert.AreEqual(requestToSend, requestReceived);
    }
}
