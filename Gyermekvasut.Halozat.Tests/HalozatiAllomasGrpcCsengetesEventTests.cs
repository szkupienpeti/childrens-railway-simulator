using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Halozat.Factory;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public class HalozatiAllomasGrpcCsengetesEventTests : MockHalozatiAllomasEventTestBase<CsengetesEventArgs>
{
    protected override Action<EventHandler<CsengetesEventArgs>> Subscriber()
        => handler => Allomas.CsengetesEvent += handler;

    [TestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcCsngetesEvent_WhenRaised_ShouldRaiseCsengetesEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        // Act
        ActRaiseGrpcCsengetesEvent(irany);
        // Assert
        AssertCsengetesRaised(irany);
    }


    private void ActRaiseGrpcCsengetesEvent(Irany irany)
    {
        var request = GrpcRequestFactory.CreateCsengetesRequest(GetSzomszedAllomasNev(irany), GetCsengetes(irany));
        var eventArgs = new GrpcCsengetesEventArgs(request);
        GrpcServerMock.Raise(a => a.GrpcCsengetesEvent += null, eventArgs);
    }

    private void AssertCsengetesRaised(Irany irany)
    {
        AssertEventRaisedBySzomszed(irany);
        CollectionAssert.AreEqual(GetCsengetes(irany), EventArgs!.Csengetesek);
    }
}