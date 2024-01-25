using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;

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
        var eventCapturer = new GrpcRequestEventCapturer<CsengetesRequest>(handler => Allomas.AllomasServer.GrpcCsengetesEvent += handler);
        // Act
        var requestToSend = CsengetesTestsUtil.CreateBejovoCsengetesRequest(allomasNev, irany);
        SzomszedClient.Csengetes(requestToSend);
        // Assert
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var requestReceived = eventCapturer.CapturedRequest!;
        Assert.AreEqual(requestToSend, requestReceived);
    }
}
