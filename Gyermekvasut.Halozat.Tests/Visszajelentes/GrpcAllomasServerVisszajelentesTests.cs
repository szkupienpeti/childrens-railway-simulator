using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.Visszajelentes;

[TestClass]
public class GrpcAllomasServerVisszajelentesTests : MockHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a GrpcAllomasServer objektumon Visszajelentes() függvényt hívunk, akkor az triggereli
    /// a GrpcVisszajelentesEvent eseményt, az eredeti VisszajelentesRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ServerVisszajelentes_ShouldRaiseGrpcEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        var server = new GrpcAllomasServer();
        var eventCapturer = new GrpcRequestEventCapturer<VisszajelentesRequest>(handler => server.GrpcVisszajelentesEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoVisszajelentesRequest(allomasNev, irany);
        server.Visszajelentes(grpcRequest, Mock.Of<ServerCallContext>());
        // Assert
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var eventArgsRequest = eventCapturer.CapturedRequest!;
        Assert.AreEqual(grpcRequest, eventArgsRequest);
    }

    /// <summary>
    /// Ha triggereljük a GrpcAllomasServer objektum GrpcVisszajelentesEvent eseményét, akkor az triggereli
    /// a HalozatiAllomas objektum VisszajelentesEvent eseményét, megfelelő tartalmú VisszajelentesEventArgs objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcVisszajelentesEvent_ShouldRaiseModelEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        var eventCapturer = new EventCapturer<VisszajelentesEventArgs>(handler => Allomas.VisszajelentesEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoVisszajelentesRequest(allomasNev, irany);
        GrpcServerMock.Raise(a => a.GrpcVisszajelentesEvent += null, new GrpcRequestEventArgs<VisszajelentesRequest>(grpcRequest));
        // Assert
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var modelEventArgs = eventCapturer.CapturedEventArgs!;
        Assert.AreEqual(grpcRequest.Kuldo, ModelToGrpcMapper.MapAllomasNev(modelEventArgs.Kuldo));
        Assert.AreEqual(grpcRequest.Vonatszam, modelEventArgs.Vonatszam);
        Assert.AreEqual(grpcRequest.Nev, modelEventArgs.Nev);
    }
}
