using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.IndulasiIdoKozlesVetel;

[TestClass]
public class GrpcAllomasServerIndulasiIdoKozlesVetelTests : MockHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a GrpcAllomasServer objektumon IndulasiIdoKozlesVetel() függvényt hívunk, akkor az triggereli
    /// a GrpcIndulasiIdoKozlesVetelEvent eseményt, az eredeti IndulasiIdoKozlesVetelRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ServerIndulasiIdoKozlesVetel_ShouldRaiseGrpcEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        var server = new GrpcAllomasServer();
        var eventCapturer = new GrpcRequestEventCapturer<IndulasiIdoKozlesVetelRequest>(handler => server.GrpcIndulasiIdoKozlesVetelEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoIndulasiIdoKozlesVetelRequest(allomasNev, irany);
        server.IndulasiIdoKozlesVetel(grpcRequest, Mock.Of<ServerCallContext>());
        // Assert
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var eventArgsRequest = eventCapturer.CapturedRequest!;
        Assert.AreEqual(grpcRequest, eventArgsRequest);
    }

    /// <summary>
    /// Ha triggereljük a GrpcAllomasServer objektum GrpcIndulasiIdoKozlesVetelEvent eseményét, akkor az triggereli
    /// a HalozatiAllomas objektum IndulasiIdoKozlesVetelEvent eseményét, megfelelő tartalmú IndulasiIdoKozlesVetelEventArgs objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcIndulasiIdoKozlesVetelEvent_ShouldRaiseModelEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        var eventCapturer = new EventCapturer<IndulasiIdoKozlesVetelEventArgs>(handler => Allomas.IndulasiIdoKozlesVetelEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoIndulasiIdoKozlesVetelRequest(allomasNev, irany);
        GrpcServerMock.Raise(a => a.GrpcIndulasiIdoKozlesVetelEvent += null, new GrpcRequestEventArgs<IndulasiIdoKozlesVetelRequest>(grpcRequest));
        // Assert
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var modelEventArgs = eventCapturer.CapturedEventArgs!;
        Assert.AreEqual(grpcRequest.Kuldo, ModelToGrpcMapper.MapAllomasNev(modelEventArgs.Kuldo));
        Assert.AreEqual(grpcRequest.Vonatszam, modelEventArgs.Vonatszam);
        Assert.AreEqual(grpcRequest.Ido, ModelToGrpcMapper.MapTimeOnly(modelEventArgs.Ido));
        Assert.AreEqual(grpcRequest.Nev, modelEventArgs.Nev);
    }
}
