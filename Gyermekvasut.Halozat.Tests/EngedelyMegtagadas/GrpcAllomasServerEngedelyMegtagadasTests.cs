using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.EngedelyMegtagadas;

[TestClass]
public class GrpcAllomasServerEngedelyMegtagadasTests : MockHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a GrpcAllomasServer objektumon EngedelyMegtagadas() függvényt hívunk, akkor az triggereli
    /// a GrpcEngedelyMegtagadasEvent eseményt, az eredeti EngedelyMegtagadasRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ServerEngedelyMegtagadas_ShouldRaiseGrpcEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        var server = new GrpcAllomasServer();
        var eventCapturer = new GrpcRequestEventCapturer<EngedelyMegtagadasRequest>(handler => server.GrpcEngedelyMegtagadasEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoEngedelyMegtagadasRequest(allomasNev, irany);
        server.EngedelyMegtagadas(grpcRequest, Mock.Of<ServerCallContext>());
        // Assert
        HalozatAssertUtil.AssertExpectedGrpcRequestEventWasCaptured(eventCapturer, grpcRequest);
    }

    /// <summary>
    /// Ha triggereljük a GrpcAllomasServer objektum GrpcEngedelyMegtagadasEvent eseményét, akkor az
    /// triggereli a HalozatiAllomas objektum EngedelyMegtagadasEvent eseményét, megfelelő tartalmú EngedelyMegtagadasEventArgs objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcEngedelyMegtagadasEvent_ShouldRaiseModelEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        var eventCapturer = new EventCapturer<EngedelyMegtagadasEventArgs>(handler => Allomas.EngedelyMegtagadasEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoEngedelyMegtagadasRequest(allomasNev, irany);
        GrpcServerMock.Raise(a => a.GrpcEngedelyMegtagadasEvent += null, new GrpcRequestEventArgs<EngedelyMegtagadasRequest>(grpcRequest));
        // Assert
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var modelEventArgs = eventCapturer.CapturedEventArgs!;
        Assert.AreEqual(GrpcToModelMapper.MapAllomasNev(grpcRequest.Kuldo), modelEventArgs.Kuldo);
        Assert.AreEqual(grpcRequest.Vonatszam, modelEventArgs.Vonatszam);
        Assert.AreEqual(grpcRequest.Ok, modelEventArgs.Ok);
        Assert.AreEqual(grpcRequest.PercMulva, modelEventArgs.PercMulva);
        Assert.AreEqual(grpcRequest.Nev, modelEventArgs.Nev);
    }
}
