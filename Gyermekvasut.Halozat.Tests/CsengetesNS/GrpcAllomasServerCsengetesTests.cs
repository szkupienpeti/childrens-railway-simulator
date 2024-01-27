using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.CsengetesNS;

[TestClass]
public class GrpcAllomasServerCsengetesTests : MockHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a GrpcAllomasServer objektumon Csengetes() függvényt hívunk, akkor az triggereli
    /// a GrpcCsengetesEvent eseményt, az eredeti CsengetesRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ServerCsengetes_ShouldRaiseGrpcEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        var server = new GrpcAllomasServer();
        var eventCapturer = new GrpcRequestEventCapturer<CsengetesRequest>(handler => server.GrpcCsengetesEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoCsengetesRequest(allomasNev, irany);
        server.Csengetes(grpcRequest, Mock.Of<ServerCallContext>());
        // Assert
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var eventArgsRequest = eventCapturer.CapturedRequest!;
        Assert.AreEqual(grpcRequest, eventArgsRequest);
    }

    /// <summary>
    /// Ha triggereljük a GrpcAllomasServer objektum GrpcCsengetesEvent eseményét, akkor az triggereli
    /// a HalozatiAllomas objektum CsengetesEvent eseményét, megfelelő tartalmú CsengetesEventArgs objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcCsengetesEvent_ShouldRaiseModelEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        var eventCapturer = new EventCapturer<CsengetesEventArgs>(handler => Allomas.CsengetesEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoCsengetesRequest(allomasNev, irany);
        GrpcServerMock.Raise(a => a.GrpcCsengetesEvent += null, new GrpcRequestEventArgs<CsengetesRequest>(grpcRequest));
        // Assert
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var modelEventArgs = eventCapturer.CapturedEventArgs!;
        Assert.AreEqual(GrpcToModelMapper.MapAllomasNev(grpcRequest.Kuldo), modelEventArgs.Kuldo);
        CollectionAssert.AreEqual(grpcRequest.Csengetesek, ModelToGrpcMapper.MapList(modelEventArgs.Csengetesek, ModelToGrpcMapper.MapCsengetes));
    }
}
