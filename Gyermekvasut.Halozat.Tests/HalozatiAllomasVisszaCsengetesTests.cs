using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public class HalozatiAllomasVisszaCsengetesTests
    : RealHalozatiAllomasSzomszedTestBase<VisszaCsengetesEventArgs>
{
    protected override Action<EventHandler<VisszaCsengetesEventArgs>> Subscriber()
        => handler => SzomszedAllomas.VisszaCsengetesEvent += handler;

    [TestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void VisszaCsengetes_WhenHasSzomszed_ShouldRaiseEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        AllomasokFelepit(allomasNev, irany);
        // Act
        ActVisszaCsengetes(irany);
        // Assert
        AssertVisszaCsengetesRaised(irany);
    }

    [DataTestMethod]
    [DataRow(AllomasNev.Szechenyihegy,  Irany.KezdopontFele)]
    [DataRow(AllomasNev.Huvosvolgy,     Irany.VegpontFele)]
    public void VisszaCsengetes_WhenNoSzomszed_ShouldThrow(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        AllomasFelepit(allomasNev);
        // Act and assert
        Assert.ThrowsException<NullReferenceException>(() => ActVisszaCsengetes(irany));
    }

    private void ActVisszaCsengetes(Irany irany)
    {
        var csengetes = GetCsengetes(irany);
        Allomas.VisszaCsenget(irany, csengetes);
    }

    private void AssertVisszaCsengetesRaised(Irany irany)
    {
        AssertEventRaisedByAllomas();
        var expectedCsengetes = GetCsengetes(irany);
        CollectionAssert.AreEqual(expectedCsengetes, EventArgs!.Csengetesek);
    }
}