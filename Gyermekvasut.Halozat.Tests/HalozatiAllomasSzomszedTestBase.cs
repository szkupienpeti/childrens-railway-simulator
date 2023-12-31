using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.VonatNS;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public abstract class HalozatiAllomasSzomszedTestBase<TEventArgs> : HalozatiAllomasTestBase
    where TEventArgs : EventArgs
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
    
    [TestCleanup]
    public override void TestCleanup()
    {
        base.TestCleanup();
        StopIfNotNull(_szomszedAllomas);
    }
}

public record TestVonatInfo(string Vonatszam, Menetrend Menetrend)
{
    public TestVonatInfo(string vonatszam, VonatIrany vonatIrany)
        : this(vonatszam, new Menetrend(vonatszam, vonatIrany))
    { }
}
