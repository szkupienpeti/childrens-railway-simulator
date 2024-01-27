﻿using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Grpc.Server.EventArgsNS;
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
    /// Ha triggereljük a GrpcAllomasServer objektum GrpcVisszaCsengetesEvent eseményét, akkor az triggereli
    /// a HalozatiAllomas objektum VisszaCsengetesEvent eseményét, megfelelő tartalmú VisszaCsengetesEventArgs objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcVisszaCsengetesEvent_ShouldRaiseModelEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        var eventCapturer = new EventCapturer<VisszaCsengetesEventArgs>(handler => Allomas.VisszaCsengetesEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoVisszaCsengetesRequest(allomasNev, irany);
        GrpcServerMock.Raise(a => a.GrpcVisszaCsengetesEvent += null, new GrpcRequestEventArgs<VisszaCsengetesRequest>(grpcRequest));
        // Assert
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var modelEventArgs = eventCapturer.CapturedEventArgs!;
        Assert.AreEqual(GrpcToModelMapper.MapAllomasNev(grpcRequest.Kuldo), modelEventArgs.Kuldo);
        CollectionAssert.AreEqual(grpcRequest.Csengetesek, ModelToGrpcMapper.MapList(modelEventArgs.Csengetesek, ModelToGrpcMapper.MapCsengetes));
    }
}
