using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.EngedelyKeres;

[TestClass]
public class GrpcAllomasServerEngedelyKeresTests : MockHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a GrpcAllomasServer objektumon EngedelyKeres() függvényt hívunk (azonos irányú vonat volt útban),
    /// akkor az triggereli a GrpcEngedelyKeresEvent eseményt, az eredeti EngedelyKeresRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ServerEngedelyKeres_WhenAzonosIranyu_ShouldRaiseGrpcEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        var server = new GrpcAllomasServer();
        var eventCapturer = new GrpcRequestEventCapturer<EngedelyKeresRequest>(handler => server.GrpcEngedelyKeresEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoAzonosIranyuEngedelyKeresRequest(allomasNev, irany);
        server.EngedelyKeres(grpcRequest, Mock.Of<ServerCallContext>());
        // Assert
        HalozatAssertUtil.AssertExpectedGrpcRequestEventWasCaptured(eventCapturer, grpcRequest);
    }

    /// <summary>
    /// Ha a GrpcAllomasServer objektumon EngedelyKeres() függvényt hívunk (ellenkező irányú vonat volt útban),
    /// akkor az triggereli a GrpcEngedelyKeresEvent eseményt, az eredeti EngedelyKeresRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ServerEngedelyKeres_WhenEllenkezoIranyuVolt_ShouldRaiseGrpcEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        var server = new GrpcAllomasServer();
        var eventCapturer = new GrpcRequestEventCapturer<EngedelyKeresRequest>(handler => server.GrpcEngedelyKeresEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoEllenkezoIranyuVoltEngedelyKeresRequest(allomasNev, irany);
        server.EngedelyKeres(grpcRequest, Mock.Of<ServerCallContext>());
        // Assert
        HalozatAssertUtil.AssertExpectedGrpcRequestEventWasCaptured(eventCapturer, grpcRequest);
    }

    /// <summary>
    /// Ha a GrpcAllomasServer objektumon EngedelyKeres() függvényt hívunk (ellenkező irányú vonat van útban),
    /// akkor az triggereli a GrpcEngedelyKeresEvent eseményt, az eredeti EngedelyKeresRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ServerEngedelyKeres_WhenEllenkezoIranyuVan_ShouldRaiseGrpcEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        var server = new GrpcAllomasServer();
        var eventCapturer = new GrpcRequestEventCapturer<EngedelyKeresRequest>(handler => server.GrpcEngedelyKeresEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoEllenkezoIranyuVanEngedelyKeresRequest(allomasNev, irany);
        server.EngedelyKeres(grpcRequest, Mock.Of<ServerCallContext>());
        // Assert
        HalozatAssertUtil.AssertExpectedGrpcRequestEventWasCaptured(eventCapturer, grpcRequest);
    }

    /// <summary>
    /// Ha triggereljük a GrpcAllomasServer objektum GrpcEngedelyKeresEvent eseményét (azonos irányú vonat volt útban),
    /// akkor az triggereli a HalozatiAllomas objektum EngedelyKeresEvent eseményét, megfelelő tartalmú EngedelyKeresEventArgs objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcEngedelyKeresEvent_WhenAzonosIranyu_ShouldRaiseModelEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        var eventCapturer = new EventCapturer<EngedelyKeresEventArgs>(handler => Allomas.EngedelyKeresEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoAzonosIranyuEngedelyKeresRequest(allomasNev, irany);
        GrpcServerMock.Raise(a => a.GrpcEngedelyKeresEvent += null, new GrpcRequestEventArgs<EngedelyKeresRequest>(grpcRequest));
        // Assert
        AssertExpectedModelEventWasCaptured(eventCapturer, grpcRequest);
    }

    /// <summary>
    /// Ha triggereljük a GrpcAllomasServer objektum GrpcEngedelyKeresEvent eseményét (ellenkező irányú vonat volt útban),
    /// akkor az triggereli a HalozatiAllomas objektum EngedelyKeresEvent eseményét, megfelelő tartalmú EngedelyKeresEventArgs objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcEngedelyKeresEvent_WhenEllenkezoIranyuVolt_ShouldRaiseModelEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        var eventCapturer = new EventCapturer<EngedelyKeresEventArgs>(handler => Allomas.EngedelyKeresEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoEllenkezoIranyuVoltEngedelyKeresRequest(allomasNev, irany);
        GrpcServerMock.Raise(a => a.GrpcEngedelyKeresEvent += null, new GrpcRequestEventArgs<EngedelyKeresRequest>(grpcRequest));
        // Assert
        AssertExpectedModelEventWasCaptured(eventCapturer, grpcRequest);
    }

    /// <summary>
    /// Ha triggereljük a GrpcAllomasServer objektum GrpcEngedelyKeresEvent eseményét (ellenkező irányú vonat van útban),
    /// akkor az triggereli a HalozatiAllomas objektum EngedelyKeresEvent eseményét, megfelelő tartalmú EngedelyKeresEventArgs objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcEngedelyKeresEvent_WhenEllenkezoIranyuVan_ShouldRaiseModelEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        var eventCapturer = new EventCapturer<EngedelyKeresEventArgs>(handler => Allomas.EngedelyKeresEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoEllenkezoIranyuVanEngedelyKeresRequest(allomasNev, irany);
        GrpcServerMock.Raise(a => a.GrpcEngedelyKeresEvent += null, new GrpcRequestEventArgs<EngedelyKeresRequest>(grpcRequest));
        // Assert
        AssertExpectedModelEventWasCaptured(eventCapturer, grpcRequest);
    }

    private static void AssertExpectedModelEventWasCaptured(EventCapturer<EngedelyKeresEventArgs> eventCapturer, EngedelyKeresRequest grpcRequest)
    {
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var modelEventArgs = eventCapturer.CapturedEventArgs!;
        Assert.AreEqual(GrpcToModelMapper.MapAllomasNev(grpcRequest.Kuldo), modelEventArgs.Kuldo);
        Assert.AreEqual(GrpcToModelMapper.MapEngedelyKeresTipus(grpcRequest.Tipus), modelEventArgs.Tipus);
        if (modelEventArgs.Tipus == EngedelyKeresTipus.EllenkezoIranyuVolt || modelEventArgs.Tipus == EngedelyKeresTipus.EllenkezoIranyuVan)
        {
            Assert.AreEqual(grpcRequest.UtolsoVonat, modelEventArgs.UtolsoVonat);
        }
        else
        {
            Assert.IsNull(modelEventArgs.UtolsoVonat);
        }
        Assert.AreEqual(grpcRequest.Vonatszam, modelEventArgs.Vonatszam);
        Assert.AreEqual(GrpcToModelMapper.MapIdo(grpcRequest.Ido), modelEventArgs.Ido);
        Assert.AreEqual(grpcRequest.Nev, modelEventArgs.Nev);
    }
}
