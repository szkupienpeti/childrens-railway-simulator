using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.IndulasiIdoKozlesVetel;

[TestClass]
public class GrpcAllomasClientIndulasiIdoKozlesVetelTests : RealHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a szomszédos GrpcAllomasClient objektumon IndulasiIdoKozlesVetel() függvényt hívunk,
    /// lezajlik a hálózaton a <i>tényleges</i> gRPC kommunikáció, és meghívódik
    /// a GrpcAllomasServer IndulasiIdoKozlesVetel() függvénye, megfelelő tartalmú IndulasiIdoKozlesVetelRequest objektummal
    /// <br />
    /// Ez egy integrációs teszt.
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ClientIndulasiIdoKozlesVetel_ShouldCallSomszedServer(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        AllomasEsSzomszedClientFelepit(allomasNev);
        IndulasiIdoKozlesVetelRequest? requestReceived = null;
        GrpcServerMock
            .Setup(server => server.IndulasiIdoKozlesVetel(It.IsAny<IndulasiIdoKozlesVetelRequest>(), It.IsAny<ServerCallContext>()))
            .Callback<IndulasiIdoKozlesVetelRequest, ServerCallContext>((req, _) => requestReceived = req);
        // Act
        var requestToSend = HalozatTestsUtil.CreateBejovoIndulasiIdoKozlesVetelRequest(allomasNev, irany);
        SzomszedClient.IndulasiIdoKozlesVetel(requestToSend);
        // Assert
        Assert.IsNotNull(requestReceived);
        Assert.AreEqual(requestToSend, requestReceived);
    }
}
