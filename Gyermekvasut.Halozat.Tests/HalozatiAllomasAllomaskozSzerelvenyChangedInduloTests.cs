//using Gyermekvasut.Halozat.EventArgsNS;
//using Gyermekvasut.Modellek;
//using Gyermekvasut.Modellek.AllomasNS;
//using Gyermekvasut.Modellek.VonatNS;
//using Gyermekvasut.Tests.Util;

//namespace Gyermekvasut.Halozat.Tests;

//[TestClass]
//public class HalozatiAllomasAllomaskozSzerelvenyChangedInduloTests
//    : RealHalozatiAllomasSzomszedTestBase<VonatAllomaskozbeBelepEventArgs>
//{
//    private Vonat? _vonat;
//    private Vonat Vonat => _vonat!;

//    protected override Action<EventHandler<VonatAllomaskozbeBelepEventArgs>> Subscriber()
//        => handler => SzomszedAllomas.VonatAllomaskozbeBelepEvent += handler;

//    [TestMethod]
//    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
//    public void AllomaskozSzerelvenyChanged_WhenIndul_ShouldAllomaskozbeBeleptet(AllomasNev allomasNev, Irany irany)
//    {
//        // Arrange
//        AllomasokFelepit(allomasNev, irany);
//        // Act
//        var allomaskoz = Allomas.Topologia.Allomaskozok[irany]!;
//        _vonat = CreateTestVonat(irany, allomaskoz);
//        // Assert
//        AssertAllomaskozbeBelepRaised();
//    }

//    private void AssertAllomaskozbeBelepRaised()
//    {
//        AssertEventRaisedByAllomas();
//        AssertUtil.AssertAreVonatokEqual(Vonat, EventArgs!.Vonat);
//    }
//}
