//using Gyermekvasut.Halozat.EventArgsNS;
//using Gyermekvasut.Modellek;
//using Gyermekvasut.Modellek.AllomasNS;
//using Gyermekvasut.Modellek.Palya;
//using Gyermekvasut.Modellek.VonatNS;
//using Gyermekvasut.Tests.Util;

//namespace Gyermekvasut.Halozat.Tests;

//[TestClass]
//public class HalozatiAllomasAllomaskozSzerelvenyChangedErkezoTests
//    : RealHalozatiAllomasSzomszedTestBase<VonatAllomaskozbolKilepEventArgs>
//{
//    private Szakasz? _allomaskoz;
//    private Szakasz Allomaskoz => _allomaskoz!;
//    private Szerelveny? _vonat;
//    private Szerelveny Vonat => _vonat!;

//    protected override Action<EventHandler<VonatAllomaskozbolKilepEventArgs>> Subscriber()
//        => handler => SzomszedAllomas.VonatAllomaskozbolKilepEvent += handler;

//    [TestMethod]
//    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
//    public void AllomaskozSzerelvenyChanged_WhenErkezik_ShouldAllomaskozbolKileptet(AllomasNev allomasNev, Irany irany)
//    {
//        // Arrange
//        ArrangeAllomaskozbolKilepTest(allomasNev, irany);
//        // Act
//        Allomaskoz.Felszabadit(Vonat);
//        // Assert
//        AssertAllomaskozbolKilepRaised();
//    }

//    private void ArrangeAllomaskozbolKilepTest(AllomasNev allomasNev, Irany irany)
//    {
//        AllomasokFelepit(allomasNev, irany);
//        var szomszedAllomaskoz = SzomszedAllomas.Topologia.Allomaskozok[irany.Fordit()]!;
//        CreateTestVonat(irany.Fordit(), szomszedAllomaskoz);
//        _allomaskoz = Allomas.Topologia.Allomaskozok[irany]!;
//        _vonat = Allomaskoz.Szerelveny;
//    }

//    private void AssertAllomaskozbolKilepRaised()
//    {
//        AssertEventRaisedByAllomas();
//        Assert.AreEqual(Vonat.Nev, EventArgs!.Vonatszam);
//    }
//}