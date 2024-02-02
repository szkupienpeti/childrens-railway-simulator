using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests.VonatAllomaskozbolKilep;

[TestClass]
public class GrpcAllomasServerVonatAllomaskozbolKilepTests : MockHalozatiAllomasTestBase
{
    /// <summary>
    /// Ha a GrpcAllomasServer objektumon VonatAllomaskozbolKilep() függvényt hívunk, akkor az triggereli
    /// a GrpcVonatAllomaskozbolKilepEvent eseményt, az eredeti VonatAllomaskozbolKilepRequest objektummal
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void ServerVonatAllomaskozbolKilep_ShouldRaiseGrpcEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        var server = new GrpcAllomasServer();
        var eventCapturer = new GrpcRequestEventCapturer<VonatAllomaskozbolKilepRequest>(handler => server.GrpcVonatAllomaskozbolKilepEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoVonatAllomaskozbolKilepRequest(allomasNev, irany);
        server.VonatAllomaskozbolKilep(grpcRequest, Mock.Of<ServerCallContext>());
        // Assert
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var eventArgsRequest = eventCapturer.CapturedRequest!;
        Assert.AreEqual(grpcRequest, eventArgsRequest);
    }

    /// <summary>
    /// Ha triggereljük a GrpcAllomasServer objektum GrpcVonatAllomaskozbolKilepEvent eseményét, akkor az triggereli
    /// a HalozatiAllomas objektum VonatAllomaskozbolKilepEvent eseményét, megfelelő tartalmú VonatAllomaskozbolKilepEventArgs objektummal,
    /// feltéve, hogy a VonatAllomaskozbolKilepEventArgs-beli vonatszám megegyezik az állomásközben lévő vonat vonatszámával
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcVonatAllomaskozbolKilepEvent_WhenAzonosVonatszam_ShouldRaiseModelEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        CreateInduloTestVonatAllomaskozben(irany);
        var eventCapturer = new EventCapturer<VonatAllomaskozbolKilepEventArgs>(handler => Allomas.VonatAllomaskozbolKilepEvent += handler);
        // Act
        var grpcRequest = HalozatTestsUtil.CreateBejovoVonatAllomaskozbolKilepRequest(allomasNev, irany);
        GrpcServerMock.Raise(a => a.GrpcVonatAllomaskozbolKilepEvent += null, new GrpcRequestEventArgs<VonatAllomaskozbolKilepRequest>(grpcRequest));
        // Assert
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var modelEventArgs = eventCapturer.CapturedEventArgs!;
        Assert.AreEqual(grpcRequest.Kuldo, ModelToGrpcMapper.MapAllomasNev(modelEventArgs.Kuldo));
        Assert.AreEqual(grpcRequest.Vonatszam, modelEventArgs.Vonatszam);
    }

    /// <summary>
    /// Ha triggereljük a GrpcAllomasServer objektum GrpcVonatAllomaskozbolKilepEvent eseményét, akkor az triggereli
    /// a HalozatiAllomas objektum VonatAllomaskozbolKilepEvent eseményét, megfelelő tartalmú VonatAllomaskozbolKilepEventArgs objektummal,
    /// azonban ha a VonatAllomaskozbolKilepEventArgs-beli vonatszám eltér az állomásközben lévő vonat vonatszámától, akkor ArgumentException kivételt dob
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcVonatAllomaskozbolKilepEvent_WhenElteroVonatszam_ShouldThrow(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        CreateInduloTestVonatAllomaskozben(irany);
        var eventCapturer = new EventCapturer<VonatAllomaskozbolKilepEventArgs>(handler => Allomas.VonatAllomaskozbolKilepEvent += handler);
        // Act and assert
        var grpcRequest = HalozatTestsUtil.CreateElteroBejovoVonatAllomaskozbolKilepRequest(allomasNev, irany);
        var exception = Assert.ThrowsException<ArgumentException>(
            () => GrpcServerMock.Raise(a => a.GrpcVonatAllomaskozbolKilepEvent += null, new GrpcRequestEventArgs<VonatAllomaskozbolKilepRequest>(grpcRequest)));
        Assert.AreEqual($"Nem az állomásköz szerelvényét ({VonatTestsUtil.GetInduloVonat(irany).Vonatszam}) próbálja kiléptetni, hanem a(z) {VonatTestsUtil.MASIK_VONATSZAM} sz. vonatot", exception.Message);
    }

    /// <summary>
    /// Ha triggereljük a GrpcAllomasServer objektum GrpcVonatAllomaskozbolKilepEvent eseményét, akkor az triggereli
    /// a HalozatiAllomas objektum VonatAllomaskozbolKilepEvent eseményét, megfelelő tartalmú VonatAllomaskozbolKilepEventArgs objektummal,
    /// azonban ha az állomásköz üres, akkor InvalidOperationException kivételt dob
    /// </summary>
    [DataTestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcVonatAllomaskozbolKilepEvent_WhenAllomaskozUres_ShouldThrow(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        var eventCapturer = new EventCapturer<VonatAllomaskozbolKilepEventArgs>(handler => Allomas.VonatAllomaskozbolKilepEvent += handler);
        // Act and assert
        var grpcRequest = HalozatTestsUtil.CreateBejovoVonatAllomaskozbolKilepRequest(allomasNev, irany);
        var exception = Assert.ThrowsException<InvalidOperationException>(
            () => GrpcServerMock.Raise(a => a.GrpcVonatAllomaskozbolKilepEvent += null, new GrpcRequestEventArgs<VonatAllomaskozbolKilepRequest>(grpcRequest)));
        Assert.AreEqual($"Az állomásköz üres, így nem léptethető ki a(z) {VonatTestsUtil.GetInduloVonat(irany).Vonatszam} sz. vonat", exception.Message);
    }
}
