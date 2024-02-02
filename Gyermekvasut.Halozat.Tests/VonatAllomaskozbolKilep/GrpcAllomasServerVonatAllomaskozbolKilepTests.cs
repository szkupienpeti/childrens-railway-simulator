using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.VonatAllomaskozbolKilep;

[TestClass]
public class GrpcAllomasServerVonatAllomaskozbolKilepTests : MockHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a GrpcAllomasServer objektumon VonatAllomaskozbolKilep() függvényt hívunk, akkor az triggereli
    /// a GrpcVonatAllomaskozbolKilepEvent eseményt, az eredeti VonatAllomaskozbolKilepRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ServerVonatAllomaskozbolKilep_ShouldRaiseGrpcEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        var server = new GrpcAllomasServer();
        var eventCapturer = new GrpcRequestEventCapturer<VonatAllomaskozbolKilepRequest>(handler => server.GrpcVonatAllomaskozbolKilepEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoVonatAllomaskozbolKilepRequest(allomasNev, irany);
        server.VonatAllomaskozbolKilep(grpcRequest, Mock.Of<ServerCallContext>());
        // Assert
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var eventArgsRequest = eventCapturer.CapturedRequest!;
        Assert.AreEqual(grpcRequest, eventArgsRequest);
    }

    /// <summary>
    /// Ha triggereljük a GrpcAllomasServer objektum GrpcVonatAllomaskozbolKilepEvent eseményét, akkor az triggereli
    /// a HalozatiAllomas objektum VonatAllomaskozbolKilepEvent eseményét, megfelelő tartalmú VonatAllomaskozbolKilepEventArgs objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcVonatAllomaskozbolKilepEvent_ShouldRaiseModelEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        CreateInduloTestVonatAllomaskozben(irany);
        var eventCapturer = new EventCapturer<VonatAllomaskozbolKilepEventArgs>(handler => Allomas.VonatAllomaskozbolKilepEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoVonatAllomaskozbolKilepRequest(allomasNev, irany);
        GrpcServerMock.Raise(a => a.GrpcVonatAllomaskozbolKilepEvent += null, new GrpcRequestEventArgs<VonatAllomaskozbolKilepRequest>(grpcRequest));
        // Assert
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var modelEventArgs = eventCapturer.CapturedEventArgs!;
        Assert.AreEqual(grpcRequest.Kuldo, ModelToGrpcMapper.MapAllomasNev(modelEventArgs.Kuldo));
        Assert.AreEqual(grpcRequest.Vonatszam, modelEventArgs.Vonatszam);
    }
}
