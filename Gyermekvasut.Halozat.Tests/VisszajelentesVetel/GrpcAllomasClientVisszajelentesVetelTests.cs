using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.VisszajelentesVetel;

[TestClass]
public class GrpcAllomasClientVisszajelentesVetelTests : RealHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a szomszédos GrpcAllomasClient objektumon VisszajelentesVetel() függvényt hívunk,
    /// lezajlik a hálózaton a <i>tényleges</i> gRPC kommunikáció, és meghívódik
    /// a GrpcAllomasServer VisszajelentesVetel() függvénye, megfelelő tartalmú VisszajelentesVetelRequest objektummal
    /// <br />
    /// Ez egy integrációs teszt.
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ClientVisszajelentesVetel_ShouldCallSomszedServer(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        AllomasEsSzomszedClientFelepit(allomasNev);
        VisszajelentesVetelRequest? requestReceived = null;
        GrpcServerMock
            .Setup(server => server.VisszajelentesVetel(It.IsAny<VisszajelentesVetelRequest>(), It.IsAny<ServerCallContext>()))
            .Callback<VisszajelentesVetelRequest, ServerCallContext>((req, _) => requestReceived = req);
        // Act
        var requestToSend = HalozatTestsUtil.CreateBejovoVisszajelentesVetelRequest(allomasNev, irany);
        SzomszedClient.VisszajelentesVetel(requestToSend);
        // Assert
        Assert.IsNotNull(requestReceived);
        Assert.AreEqual(requestToSend, requestReceived);
    }
}
