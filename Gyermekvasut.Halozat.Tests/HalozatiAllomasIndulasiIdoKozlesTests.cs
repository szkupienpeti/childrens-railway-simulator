using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public class HalozatiAllomasIndulasiIdoKozlesTests
    : RealHalozatiAllomasSzomszedTestBase<IndulasiIdoKozlesEventArgs>
{
    protected override Action<EventHandler<IndulasiIdoKozlesEventArgs>> Subscriber()
        => handler => SzomszedAllomas.IndulasiIdoKozlesEvent += handler;

    [TestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void IndulasiIdoKozles_WhenHasSzomszed_ShouldRaiseEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        AllomasokFelepit(allomasNev, irany);
        // Act
        ActIndulasiIdoKozles(irany);
        // Assert
        AssertIndulasiIdoKozlesRaised(irany);
    }

    [DataTestMethod]
    [DataRow(AllomasNev.Szechenyihegy,  Irany.KezdopontFele)]
    [DataRow(AllomasNev.Huvosvolgy,     Irany.VegpontFele)]
    public void IndulasiIdoKozles_WhenNoSzomszed_ShouldThrow(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        AllomasFelepit(allomasNev);
        // Act and assert
        Assert.ThrowsException<NullReferenceException>(() => ActIndulasiIdoKozles(irany));
    }

    private void ActIndulasiIdoKozles(Irany irany)
    {
        Allomas.IndulasiIdotKozol(irany, VONAT_INFOS[irany].Vonatszam, TEST_IDO, TEST_NEV);
    }

    private void AssertIndulasiIdoKozlesRaised(Irany irany)
    {
        AssertEventRaisedByAllomas();
        Assert.AreEqual(VONAT_INFOS[irany].Vonatszam, EventArgs!.Vonatszam);
        Assert.AreEqual(TEST_IDO, EventArgs!.Ido);
        Assert.AreEqual(TEST_NEV, EventArgs!.Nev);
    }
}