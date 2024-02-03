using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.VonatAllomaskozbeBelep;

[TestClass]
public class GrpcAllomasServerVonatAllomaskozbeBelepTests : MockHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a GrpcAllomasServer objektumon VonatAllomaskozbeBelep() függvényt hívunk, akkor az triggereli
    /// a GrpcVonatAllomaskozbeBelepEvent eseményt, az eredeti VonatAllomaskozbeBelepRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ServerVonatAllomaskozbeBelep_ShouldRaiseGrpcEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        var server = new GrpcAllomasServer();
        var eventCapturer = new GrpcRequestEventCapturer<VonatAllomaskozbeBelepRequest>(handler => server.GrpcVonatAllomaskozbeBelepEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoVonatAllomaskozbeBelepRequest(allomasNev, irany);
        server.VonatAllomaskozbeBelep(grpcRequest, Mock.Of<ServerCallContext>());
        // Assert
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var eventArgsRequest = eventCapturer.CapturedRequest!;
        Assert.AreEqual(grpcRequest, eventArgsRequest);
    }

    /// <summary>
    /// Ha triggereljük a GrpcAllomasServer objektum GrpcVonatAllomaskozbeBelepEvent eseményét, akkor az triggereli
    /// a HalozatiAllomas objektum VonatAllomaskozbeBelepEvent eseményét, megfelelő tartalmú VonatAllomaskozbeBelepEventArgs objektummal,
    /// feltéve, hogy az állomásköz üres
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcVonatAllomaskozbeBelepEvent_WhenAllomaskozUres_ShouldRaiseModelEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        var eventCapturer = new EventCapturer<VonatAllomaskozbeBelepEventArgs>(handler => Allomas.VonatAllomaskozbeBelepEvent += handler);
        // Act
        var vonat = VonatTestsUtil.CreateErkezoTestVonat(irany);
        var grpcRequest = HalozatTestsUtil.CreateBejovoVonatAllomaskozbeBelepRequest(allomasNev, irany, vonat);
        GrpcServerMock.Raise(a => a.GrpcVonatAllomaskozbeBelepEvent += null, new GrpcRequestEventArgs<VonatAllomaskozbeBelepRequest>(grpcRequest));
        // Assert
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var modelEventArgs = eventCapturer.CapturedEventArgs!;
        Assert.AreEqual(grpcRequest.Kuldo, ModelToGrpcMapper.MapAllomasNev(modelEventArgs.Kuldo));
        AssertUtil.AssertAreVonatokEqual(vonat, modelEventArgs.Vonat);
    }

    /// <summary>
    /// Ha triggereljük a GrpcAllomasServer objektum GrpcVonatAllomaskozbeBelepEvent eseményét, akkor az triggereli
    /// a HalozatiAllomas objektum VonatAllomaskozbeBelepEvent eseményét, megfelelő tartalmú VonatAllomaskozbeBelepEventArgs objektummal,
    /// azonban ha az állomásköz foglalt, akkor InvalidOperationException kivételt dob
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcVonatAllomaskozbeBelepEvent_WhenAllomaskozFoglalt_ShouldThrow(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        CreateInduloTestVonatAllomaskozben(irany);
        // Act and assert
        var grpcRequest = HalozatTestsUtil.CreateBejovoVonatAllomaskozbeBelepRequest(allomasNev, irany);
        var exception = Assert.ThrowsException<InvalidOperationException>(
            () => GrpcServerMock.Raise(a => a.GrpcVonatAllomaskozbeBelepEvent += null, new GrpcRequestEventArgs<VonatAllomaskozbeBelepRequest>(grpcRequest)));
        Assert.AreEqual("Foglalt szakaszt próbál elfoglalni egy szerelvény", exception.Message);
    }

    /// <summary>
    /// Ha triggereljük a GrpcAllomasServer objektum GrpcVonatAllomaskozbeBelepEvent eseményét, akkor az triggereli
    /// a HalozatiAllomas objektum VonatAllomaskozbeBelepEvent eseményét, megfelelő tartalmú VonatAllomaskozbeBelepEventArgs objektummal,
    /// azonban ha a vonat iránya inkonzisztens (pl. kezdőpont felől lép be egy kezdőpont felé közlekedő vonat), akkor InvalidOperationException kivételt dob
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcVonatAllomaskozbeBelepEvent_WhenIranyInkonzisztens_ShouldThrow(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        // Act and assert
        var inkonzisztensIranyuVonat = VonatTestsUtil.CreateInduloTestVonat(irany);
        var grpcRequest = HalozatTestsUtil.CreateBejovoVonatAllomaskozbeBelepRequest(allomasNev, irany, inkonzisztensIranyuVonat);
        var exception = Assert.ThrowsException<InvalidOperationException>(
            () => GrpcServerMock.Raise(a => a.GrpcVonatAllomaskozbeBelepEvent += null, new GrpcRequestEventArgs<VonatAllomaskozbeBelepRequest>(grpcRequest)));
        Assert.AreEqual($"{allomasNev}: Állomásközbe belépő vonat irány inkonzisztencia: {GetSzomszedAllomasNev(irany)} felől, {inkonzisztensIranyuVonat.Irany} irányú vonat", exception.Message);
    }
}
