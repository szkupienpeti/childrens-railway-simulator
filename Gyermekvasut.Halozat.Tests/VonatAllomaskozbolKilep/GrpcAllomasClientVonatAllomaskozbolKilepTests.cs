using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.VonatAllomaskozbolKilep;

[TestClass]
public class GrpcAllomasClientVonatAllomaskozbolKilepTests : RealHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a szomszédos GrpcAllomasClient objektumon VonatAllomaskozbolKilep() függvényt hívunk,
    /// lezajlik a hálózaton a <i>tényleges</i> gRPC kommunikáció, és meghívódik
    /// a GrpcAllomasServer VonatAllomaskozbolKilep() függvénye, megfelelő tartalmú VonatAllomaskozbolKilepRequest objektummal
    /// <br />
    /// Ez egy integrációs teszt.
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ClientVonatAllomaskozbolKilep_ShouldCallSomszedServer(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        AllomasEsSzomszedClientFelepit(allomasNev);
        // A vonat lehelyezésekor a mock client ne hívja meg a szomszéd állomást, mert annak a servere nincs felépítve
        GetMockClient(irany)
            .Setup(client => client.VonatAllomaskozbeBelep(It.IsAny<VonatAllomaskozbeBelepRequest>(), null, null, default))
            .Returns(new Google.Protobuf.WellKnownTypes.Empty());
        CreateInduloTestVonatAllomaskozben(irany);
        VonatAllomaskozbolKilepRequest? requestReceived = null;
        GrpcServerMock
            .Setup(server => server.VonatAllomaskozbolKilep(It.IsAny<VonatAllomaskozbolKilepRequest>(), It.IsAny<ServerCallContext>()))
            .Callback<VonatAllomaskozbolKilepRequest, ServerCallContext>((req, _) => requestReceived = req);
        // Act
        var requestToSend = HalozatTestsUtil.CreateBejovoVonatAllomaskozbolKilepRequest(allomasNev, irany);
        SzomszedClient.VonatAllomaskozbolKilep(requestToSend);
        // Assert
        Assert.IsNotNull(requestReceived);
        Assert.AreEqual(requestToSend, requestReceived);
    }
}
