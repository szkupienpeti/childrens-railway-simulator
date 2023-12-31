using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public abstract class MockHalozatiAllomasEventTestBase<TEventArgs> : MockHalozatiAllomasTestBase
    where TEventArgs : HalozatiAllomasEventArgs
{
    protected TEventArgs? EventArgs { get; set; }

    protected abstract Action<EventHandler<TEventArgs>> Subscriber();

    protected override void MockAllomasFelepit(AllomasNev allomasNev)
    {
        base.MockAllomasFelepit(allomasNev);
        SubscribeToEvent();
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

    protected void AssertEventRaisedBySzomszed(Irany irany)
    {
        AssertEventRaised();
        Assert.AreEqual(GetSzomszedAllomasNev(irany), EventArgs!.Kuldo);
    }
}
