using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Client;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.VonatNS;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public class HalozatiAllomasVonatAllomaskozbeBelepEventTests : HalozatiAllomasTestBase
{
    [TestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void VonatAllomaskozbeBelepEvent_WhenAllomaskozSzabad_ShouldLeteszWithoutRaise(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        var vonat = CreateTestVonat(irany.Fordit());
        // Act
        ActRaiseVonatAllomaskozbeBelepEvent(irany, vonat);
        // Assert
        var allomaskoz = Allomas.Topologia.Allomaskozok[irany]!;
        Assert.IsNotNull(allomaskoz.Szerelveny);
        AssertUtil.AssertAreVonatokEqual(vonat, (allomaskoz.Szerelveny as Vonat)!);
        var szomszedClientMock = GetMockSzomszedClient(irany);
        szomszedClientMock.Verify(
            client => client.VonatAllomaskozbeBelep(It.IsAny<VonatAllomaskozbeBelepRequest>(), It.IsAny<CallOptions>()),
            Times.Never());
    }

    [TestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void VonatAllomaskozbeBelepEvent_WhenRosszIrany_ShouldThrow(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        var vonat = CreateTestVonat(irany);
        // Act and assert
        Assert.ThrowsException<InvalidOperationException>(() => ActRaiseVonatAllomaskozbeBelepEvent(irany, vonat),
            $"Állomásközbe belépõ vonat irány inkonzisztencia: {allomasNev.Szomszed(irany)} felöl, {irany} irányú vonat");
    }

    [TestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void VonatAllomaskozbeBelepEvent_WhenAllomaskozFoglalt_ShouldThrow(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        CreateErkezoTestVonatAllomaskozben(irany);
        // Act and assert
        var vonat = CreateTestVonat(irany.Fordit());
        Assert.ThrowsException<ArgumentException>(() => ActRaiseVonatAllomaskozbeBelepEvent(irany, vonat),
            "Foglalt szakaszt próbál elfoglalni egy szerelvény");
    }

    private void ActRaiseVonatAllomaskozbeBelepEvent(Irany irany, Vonat vonat)
    {
        var request = new VonatAllomaskozbeBelepRequest()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(Allomas.AllomasNev.Szomszed(irany)!.Value),
            Vonat = ModelToGrpcMapper.MapVonat(vonat)
        };
        var eventArgs = new GrpcVonatAllomaskozbeBelepEventArgs(request);
        GrpcServerMock.Raise(a => a.GrpcVonatAllomaskozbeBelepEvent += null, eventArgs);
    }
}