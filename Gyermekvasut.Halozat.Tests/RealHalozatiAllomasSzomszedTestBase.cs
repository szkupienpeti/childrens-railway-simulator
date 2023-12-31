using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public abstract class RealHalozatiAllomasSzomszedTestBase<TEventArgs> : RealHalozatiAllomasTestBase
    where TEventArgs : HalozatiAllomasEventArgs
{
    private HalozatiAllomas? _szomszedAllomas;
    protected HalozatiAllomas SzomszedAllomas => _szomszedAllomas!;
    protected TEventArgs? EventArgs { get; set; }

    protected abstract Action<EventHandler<TEventArgs>> Subscriber();

    protected void AllomasokFelepit(AllomasNev allomasNev, Irany irany)
    {
        AllomasFelepit(allomasNev);
        SzomszedAllomasFelepit(allomasNev, irany);
        SubscribeToEvent();
    }

    private void SzomszedAllomasFelepit(AllomasNev allomasNev, Irany irany)
    {
        var szomszedAllomasNev = allomasNev.Szomszed(irany)!.Value;
        _szomszedAllomas = AllomasFactory.Create(szomszedAllomasNev);
    }

    private void SubscribeToEvent()
        => Subscriber().Invoke(HandleEvent);

    private void HandleEvent(object? sender, TEventArgs e)
    {
        EventArgs = e;
    }

    protected void AssertEventRaised()
        => Assert.IsNotNull(EventArgs);

    protected void AssertEventNotRaised()
        => Assert.IsNull(EventArgs);

    protected void AssertEventRaisedByAllomas()
    {
        AssertEventRaised();
        Assert.AreEqual(Allomas.AllomasNev, EventArgs!.Kuldo);
    }

    [TestCleanup]
    public override void TestCleanup()
    {
        base.TestCleanup();
        StopIfNotNull(_szomszedAllomas);
    }
}
