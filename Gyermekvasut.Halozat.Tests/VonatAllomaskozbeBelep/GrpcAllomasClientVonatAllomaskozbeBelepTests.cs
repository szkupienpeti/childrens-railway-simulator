using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.VonatAllomaskozbeBelep;

[TestClass]
public class GrpcAllomasClientVonatAllomaskozbeBelepTests : RealHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a szomszédos GrpcAllomasClient objektumon VonatAllomaskozbeBelep() függvényt hívunk,
    /// lezajlik a hálózaton a <i>tényleges</i> gRPC kommunikáció, és meghívódik
    /// a GrpcAllomasServer VonatAllomaskozbeBelep() függvénye, megfelelő tartalmú VonatAllomaskozbeBelepRequest objektummal
    /// <br />
    /// Ez egy integrációs teszt.
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ClientVonatAllomaskozbeBelep_ShouldCallSomszedServer(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        AllomasEsSzomszedClientFelepit(allomasNev);
        VonatAllomaskozbeBelepRequest? requestReceived = null;
        GrpcServerMock
            .Setup(server => server.VonatAllomaskozbeBelep(It.IsAny<VonatAllomaskozbeBelepRequest>(), It.IsAny<ServerCallContext>()))
            .Callback<VonatAllomaskozbeBelepRequest, ServerCallContext>((req, _) => requestReceived = req);
        // Act
        var requestToSend = HalozatTestsUtil.CreateBejovoVonatAllomaskozbeBelepRequest(allomasNev, irany);
        SzomszedClient.VonatAllomaskozbeBelep(requestToSend);
        // Assert
        Assert.IsNotNull(requestReceived);
        Assert.AreEqual(requestToSend, requestReceived);
    }
}
