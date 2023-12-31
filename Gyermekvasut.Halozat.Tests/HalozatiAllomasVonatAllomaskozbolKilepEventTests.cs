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
public class HalozatiAllomasVonatAllomaskozbolKilepEventTests : HalozatiAllomasTestBase
{
    [TestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void VonatAllomaskozbolKilepEvent_WhenAzonosVonatszam_ShouldFelszabaditWithoutRaise(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        var vonat = CreateInduloTestVonatAllomaskozben(irany);
        // Act
        ActRaiseVonatAllomaskozbolKilepEvent(irany, vonat.Nev);
        // Assert
        var allomaskoz = Allomas.Topologia.Allomaskozok[irany]!;
        Assert.IsNull(allomaskoz.Szerelveny);
        Assert.IsTrue(vonat.Megszuntetve);
        var szomszedClientMock = GetMockSzomszedClient(irany);
        szomszedClientMock.Verify(
            client => client.VonatAllomaskozbolKilep(It.IsAny<VonatAllomaskozbolKilepRequest>(), It.IsAny<CallOptions>()),
            Times.Never());
    }

    [TestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void VonatAllomaskozbolKilepEvent_WhenElteroVonatszam_ShouldThrow(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        var vonat = CreateInduloTestVonatAllomaskozben(irany);
        // Act and assert
        var elteroVonatszam = $"{vonat.Nev}_OTHER";
        Assert.ThrowsException<ArgumentException>(() => ActRaiseVonatAllomaskozbolKilepEvent(irany, elteroVonatszam),
            $"Nem az állomásköz szerelvényét ({vonat.Nev}) próbálja kiléptetni, hanem a(z) {elteroVonatszam} sz. vonatot");
    }

    [TestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void VonatAllomaskozbolKilepEvent_WhenSzabad_ShouldThrow(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        // Act and assert
        var nemLetezoVonatszam = "NON_EXISTING_VONAT";
        Assert.ThrowsException<InvalidOperationException>(() => ActRaiseVonatAllomaskozbolKilepEvent(irany, nemLetezoVonatszam),
            $"Az állomásköz üres, így nem léptethetõ ki a(z) {nemLetezoVonatszam} sz. vonat");
    }

    private void ActRaiseVonatAllomaskozbolKilepEvent(Irany irany, string vonatszam)
    {
        var request = new VonatAllomaskozbolKilepRequest()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(Allomas.AllomasNev.Szomszed(irany)!.Value),
            Vonatszam = vonatszam
        };
        var eventArgs = new GrpcVonatAllomaskozbolKilepEventArgs(request);
        GrpcServerMock.Raise(a => a.GrpcVonatAllomaskozbolKilepEvent += null, eventArgs);
    }
}