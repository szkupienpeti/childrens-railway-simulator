using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.EngedelyAdas;

[TestClass]
public class GrpcAllomasServerEngedelyAdasTests : MockHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a GrpcAllomasServer objektumon EngedelyAdas() függvényt hívunk (azonos irányú vonat volt útban),
    /// akkor az triggereli a GrpcEngedelyAdasEvent eseményt, az eredeti EngedelyAdasRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ServerEngedelyAdas_WhenAzonosIranyu_ShouldRaiseGrpcEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        var server = new GrpcAllomasServer();
        var eventCapturer = new GrpcRequestEventCapturer<EngedelyAdasRequest>(handler => server.GrpcEngedelyAdasEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoAzonosIranyuEngedelyAdasRequest(allomasNev, irany);
        server.EngedelyAdas(grpcRequest, Mock.Of<ServerCallContext>());
        // Assert
        HalozatAssertUtil.AssertExpectedGrpcRequestEventWasCaptured(eventCapturer, grpcRequest);
    }

    /// <summary>
    /// Ha a GrpcAllomasServer objektumon EngedelyAdas() függvényt hívunk (ellenkező irányú vonat volt/van útban),
    /// akkor az triggereli a GrpcEngedelyAdasEvent eseményt, az eredeti EngedelyAdasRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ServerEngedelyAdas_WhenEllenkezoIranyu_ShouldRaiseGrpcEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        var server = new GrpcAllomasServer();
        var eventCapturer = new GrpcRequestEventCapturer<EngedelyAdasRequest>(handler => server.GrpcEngedelyAdasEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoEllenkezoIranyuEngedelyAdasRequest(allomasNev, irany);
        server.EngedelyAdas(grpcRequest, Mock.Of<ServerCallContext>());
        // Assert
        HalozatAssertUtil.AssertExpectedGrpcRequestEventWasCaptured(eventCapturer, grpcRequest);
    }

    /// <summary>
    /// Ha triggereljük a GrpcAllomasServer objektum GrpcEngedelyAdasEvent eseményét (azonos irányú vonat volt útban),
    /// akkor az triggereli a HalozatiAllomas objektum EngedelyAdasEvent eseményét, megfelelő tartalmú EngedelyAdasEventArgs objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcEngedelyAdasEvent_WhenAzonosIranyu_ShouldRaiseModelEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        var eventCapturer = new EventCapturer<EngedelyAdasEventArgs>(handler => Allomas.EngedelyAdasEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoAzonosIranyuEngedelyAdasRequest(allomasNev, irany);
        GrpcServerMock.Raise(a => a.GrpcEngedelyAdasEvent += null, new GrpcRequestEventArgs<EngedelyAdasRequest>(grpcRequest));
        // Assert
        AssertExpectedModelEventWasCaptured(eventCapturer, grpcRequest);
    }

    /// <summary>
    /// Ha triggereljük a GrpcAllomasServer objektum GrpcEngedelyAdasEvent eseményét (ellenkező irányú vonat volt/van útban),
    /// akkor az triggereli a HalozatiAllomas objektum EngedelyAdasEvent eseményét, megfelelő tartalmú EngedelyAdasEventArgs objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcEngedelyAdasEvent_WhenEllenkezoIranyu_ShouldRaiseModelEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        var eventCapturer = new EventCapturer<EngedelyAdasEventArgs>(handler => Allomas.EngedelyAdasEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoEllenkezoIranyuEngedelyAdasRequest(allomasNev, irany);
        GrpcServerMock.Raise(a => a.GrpcEngedelyAdasEvent += null, new GrpcRequestEventArgs<EngedelyAdasRequest>(grpcRequest));
        // Assert
        AssertExpectedModelEventWasCaptured(eventCapturer, grpcRequest);
    }

    private static void AssertExpectedModelEventWasCaptured(EventCapturer<EngedelyAdasEventArgs> eventCapturer, EngedelyAdasRequest grpcRequest)
    {
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var modelEventArgs = eventCapturer.CapturedEventArgs!;
        Assert.AreEqual(GrpcToModelMapper.MapAllomasNev(grpcRequest.Kuldo), modelEventArgs.Kuldo);
        Assert.AreEqual(GrpcToModelMapper.MapEngedelyAdasTipus(grpcRequest.Tipus), modelEventArgs.Tipus);
        if (modelEventArgs.Tipus == EngedelyAdasTipus.AzonosIranyu)
        {
            Assert.IsNull(modelEventArgs.UtolsoVonat);
        }
        else
        {
            Assert.AreEqual(grpcRequest.UtolsoVonat, modelEventArgs.UtolsoVonat);
        }
        Assert.AreEqual(grpcRequest.Vonatszam, modelEventArgs.Vonatszam);
        Assert.AreEqual(grpcRequest.Nev, modelEventArgs.Nev);
    }
}
