//using Grpc.Core;
//using Gyermekvasut.Grpc;
//using Gyermekvasut.Grpc.Server.EventArgsNS;
//using Gyermekvasut.Halozat.Factory;
//using Gyermekvasut.Modellek;
//using Gyermekvasut.Modellek.AllomasNS;
//using Gyermekvasut.Tests.Util;
//using Moq;

//namespace Gyermekvasut.Halozat.Tests;

//[TestClass]
//public class HalozatiAllomasGrpcVonatAllomaskozbolKilepEventTests : MockHalozatiAllomasTestBase
//{
//    [TestMethod]
//    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
//    public void GrpcVonatAllomaskozbolKilepEvent_WhenAzonosVonatszam_ShouldFelszabaditWithoutRaise(AllomasNev allomasNev, Irany irany)
//    {
//        // Arrange
//        MockAllomasFelepit(allomasNev);
//        var vonat = CreateInduloTestVonatAllomaskozben(irany);
//        // Act
//        ActRaiseGrpcVonatAllomaskozbolKilepEvent(irany, vonat.Nev);
//        // Assert
//        var allomaskoz = Allomas.Topologia.Allomaskozok[irany]!;
//        Assert.IsNull(allomaskoz.Szerelveny);
//        Assert.IsTrue(vonat.Megszuntetve);
//        var szomszedClientMock = GetMockSzomszedClient(irany);
//        szomszedClientMock.Verify(
//            client => client.VonatAllomaskozbolKilep(It.IsAny<VonatAllomaskozbolKilepRequest>(), It.IsAny<CallOptions>()),
//            Times.Never());
//    }

//    [TestMethod]
//    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
//    public void GrpcVonatAllomaskozbolKilepEvent_WhenElteroVonatszam_ShouldThrow(AllomasNev allomasNev, Irany irany)
//    {
//        // Arrange
//        MockAllomasFelepit(allomasNev);
//        var vonat = CreateInduloTestVonatAllomaskozben(irany);
//        // Act and assert
//        var elteroVonatszam = $"{vonat.Nev}_OTHER";
//        Assert.
//
//        <ArgumentException>(() => ActRaiseGrpcVonatAllomaskozbolKilepEvent(irany, elteroVonatszam),
//            $"Nem az állomásköz szerelvényét ({vonat.Nev}) próbálja kiléptetni, hanem a(z) {elteroVonatszam} sz. vonatot");
//    }

//    [TestMethod]
//    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
//    public void GrpcVonatAllomaskozbolKilepEvent_WhenSzabad_ShouldThrow(AllomasNev allomasNev, Irany irany)
//    {
//        // Arrange
//        MockAllomasFelepit(allomasNev);
//        // Act and assert
//        var nemLetezoVonatszam = "NON_EXISTING_VONAT";
//        Assert.ThrowsException<InvalidOperationException>(() => ActRaiseGrpcVonatAllomaskozbolKilepEvent(irany, nemLetezoVonatszam),
//            $"Az állomásköz üres, így nem léptethetõ ki a(z) {nemLetezoVonatszam} sz. vonat");
//    }

//    private void ActRaiseGrpcVonatAllomaskozbolKilepEvent(Irany irany, string vonatszam)
//    {
//        var request = GrpcRequestFactory.CreateVonatAllomaskozbolKilepRequest(GetSzomszedAllomasNev(irany), vonatszam);
//        var eventArgs = new GrpcVonatAllomaskozbolKilepEventArgs(request);
//        GrpcServerMock.Raise(a => a.GrpcVonatAllomaskozbolKilepEvent += null, eventArgs);
//    }
//}