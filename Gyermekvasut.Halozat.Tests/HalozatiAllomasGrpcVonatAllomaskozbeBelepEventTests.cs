using Grpc.Core;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Halozat.Factory;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.VonatNS;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public class HalozatiAllomasGrpcVonatAllomaskozbeBelepEventTests : MockHalozatiAllomasTestBase
{
    [TestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcVonatAllomaskozbeBelepEvent_WhenAllomaskozSzabad_ShouldLeteszWithoutRaise(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        var vonat = CreateTestVonat(irany.Fordit());
        // Act
        ActRaiseGrpcVonatAllomaskozbeBelepEvent(irany, vonat);
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
    public void GrpcVonatAllomaskozbeBelepEvent_WhenRosszIrany_ShouldThrow(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        var vonat = CreateTestVonat(irany);
        // Act and assert
        Assert.ThrowsException<InvalidOperationException>(() => ActRaiseGrpcVonatAllomaskozbeBelepEvent(irany, vonat),
            $"Állomásközbe belépõ vonat irány inkonzisztencia: {allomasNev.Szomszed(irany)} felöl, {irany} irányú vonat");
    }

    [TestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcVonatAllomaskozbeBelepEvent_WhenAllomaskozFoglalt_ShouldThrow(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        CreateErkezoTestVonatAllomaskozben(irany);
        // Act and assert
        var vonat = CreateTestVonat(irany.Fordit());
        Assert.ThrowsException<ArgumentException>(() => ActRaiseGrpcVonatAllomaskozbeBelepEvent(irany, vonat),
            "Foglalt szakaszt próbál elfoglalni egy szerelvény");
    }

    private void ActRaiseGrpcVonatAllomaskozbeBelepEvent(Irany irany, Vonat vonat)
    {
        var request = GrpcRequestFactory.CreateVonatAllomaskozbeBelepRequest(GetSzomszedAllomasNev(irany), vonat);
        var eventArgs = new GrpcVonatAllomaskozbeBelepEventArgs(request);
        GrpcServerMock.Raise(a => a.GrpcVonatAllomaskozbeBelepEvent += null, eventArgs);
    }
}