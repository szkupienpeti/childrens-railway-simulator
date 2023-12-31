using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public class HalozatiAllomasCsengetesTests
    : RealHalozatiAllomasSzomszedTestBase<CsengetesEventArgs>
{
    protected override Action<EventHandler<CsengetesEventArgs>> Subscriber()
        => handler => SzomszedAllomas.CsengetesEvent += handler;

    [TestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void Csengetes_WhenHasSzomszed_ShouldRaiseEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        AllomasokFelepit(allomasNev, irany);
        // Act
        ActCsengetes(irany);
        // Assert
        AssertCsengetesRaised(allomasNev, irany);
    }

    [DataTestMethod]
    [DataRow(AllomasNev.Szechenyihegy,  Irany.KezdopontFele)]
    [DataRow(AllomasNev.Huvosvolgy,     Irany.VegpontFele)]
    public void Csengetes_WhenNoSzomszed_ShouldThrow(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        AllomasFelepit(allomasNev);
        // Act and assert
        Assert.ThrowsException<NullReferenceException>(() => ActCsengetes(irany));
    }

    private void ActCsengetes(Irany irany)
    {
        var csengetes = GetCsengetes(irany);
        Allomas.Csenget(irany, csengetes);
    }

    private void AssertCsengetesRaised(AllomasNev expectedKuldo, Irany irany)
    {
        AssertEventRaised();
        Assert.AreEqual(expectedKuldo, EventArgs!.Kuldo);
        var expectedCsengetes = GetCsengetes(irany);
        CollectionAssert.AreEqual(expectedCsengetes, EventArgs!.Csengetesek);
    }
}