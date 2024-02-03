using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.IndulasiIdoKozles;

[TestClass]
public class GrpcAllomasServerIndulasiIdoKozlesTests : MockHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a GrpcAllomasServer objektumon IndulasiIdoKozles() függvényt hívunk, akkor az triggereli
    /// a GrpcIndulasiIdoKozlesEvent eseményt, az eredeti IndulasiIdoKozlesRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ServerIndulasiIdoKozles_ShouldRaiseGrpcEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        var server = new GrpcAllomasServer();
        var eventCapturer = new GrpcRequestEventCapturer<IndulasiIdoKozlesRequest>(handler => server.GrpcIndulasiIdoKozlesEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoIndulasiIdoKozlesRequest(allomasNev, irany);
        server.IndulasiIdoKozles(grpcRequest, Mock.Of<ServerCallContext>());
        // Assert
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var eventArgsRequest = eventCapturer.CapturedRequest!;
        Assert.AreEqual(grpcRequest, eventArgsRequest);
    }

    /// <summary>
    /// Ha triggereljük a GrpcAllomasServer objektum GrpcIndulasiIdoKozlesEvent eseményét, akkor az triggereli
    /// a HalozatiAllomas objektum IndulasiIdoKozlesEvent eseményét, megfelelő tartalmú IndulasiIdoKozlesEventArgs objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcIndulasiIdoKozlesEvent_ShouldRaiseModelEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        var eventCapturer = new EventCapturer<IndulasiIdoKozlesEventArgs>(handler => Allomas.IndulasiIdoKozlesEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoIndulasiIdoKozlesRequest(allomasNev, irany);
        GrpcServerMock.Raise(a => a.GrpcIndulasiIdoKozlesEvent += null, new GrpcRequestEventArgs<IndulasiIdoKozlesRequest>(grpcRequest));
        // Assert
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var modelEventArgs = eventCapturer.CapturedEventArgs!;
        Assert.AreEqual(grpcRequest.Kuldo, ModelToGrpcMapper.MapAllomasNev(modelEventArgs.Kuldo));
        Assert.AreEqual(grpcRequest.Vonatszam, modelEventArgs.Vonatszam);
        Assert.AreEqual(grpcRequest.Ido, ModelToGrpcMapper.MapTimeOnly(modelEventArgs.Ido));
        Assert.AreEqual(grpcRequest.Nev, modelEventArgs.Nev);
    }
}
