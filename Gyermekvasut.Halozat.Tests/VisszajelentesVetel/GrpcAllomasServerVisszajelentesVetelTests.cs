using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.VisszajelentesVetel;

[TestClass]
public class GrpcAllomasServerVisszajelentesVetelTests : MockHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a GrpcAllomasServer objektumon VisszajelentesVetel() függvényt hívunk, akkor az triggereli
    /// a GrpcVisszajelentesVetelEvent eseményt, az eredeti VisszajelentesVetelRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ServerVisszajelentesVetel_ShouldRaiseGrpcEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        var server = new GrpcAllomasServer();
        var eventCapturer = new GrpcRequestEventCapturer<VisszajelentesVetelRequest>(handler => server.GrpcVisszajelentesVetelEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoVisszajelentesVetelRequest(allomasNev, irany);
        server.VisszajelentesVetel(grpcRequest, Mock.Of<ServerCallContext>());
        // Assert
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var eventArgsRequest = eventCapturer.CapturedRequest!;
        Assert.AreEqual(grpcRequest, eventArgsRequest);
    }

    /// <summary>
    /// Ha triggereljük a GrpcAllomasServer objektum GrpcVisszajelentesVetelEvent eseményét, akkor az triggereli
    /// a HalozatiAllomas objektum VisszajelentesVetelEvent eseményét, megfelelő tartalmú VisszajelentesVetelEventArgs objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcVisszajelentesVetelEvent_ShouldRaiseModelEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        var eventCapturer = new EventCapturer<VisszajelentesVetelEventArgs>(handler => Allomas.VisszajelentesVetelEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoVisszajelentesVetelRequest(allomasNev, irany);
        GrpcServerMock.Raise(a => a.GrpcVisszajelentesVetelEvent += null, new GrpcRequestEventArgs<VisszajelentesVetelRequest>(grpcRequest));
        // Assert
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var modelEventArgs = eventCapturer.CapturedEventArgs!;
        Assert.AreEqual(grpcRequest.Kuldo, ModelToGrpcMapper.MapAllomasNev(modelEventArgs.Kuldo));
        Assert.AreEqual(grpcRequest.Vonatszam, modelEventArgs.Vonatszam);
        Assert.AreEqual(grpcRequest.Nev, modelEventArgs.Nev);
    }
}
