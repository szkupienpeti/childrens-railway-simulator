using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Halozat.Factory;
using Gyermekvasut.Halozat.Tests.CsengetesNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public class HalozatiAllomasGrpcVisszaCsengetesEventTests : MockHalozatiAllomasEventTestBase<VisszaCsengetesEventArgs>
{
    protected override Action<EventHandler<VisszaCsengetesEventArgs>> Subscriber()
        => handler => Allomas.VisszaCsengetesEvent += handler;

    [TestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevSzomszedIranyok), typeof(DynamicTestDataUtil))]
    public void GrpcVisszaCsngetesEvent_WhenRaised_ShouldRaiseVisszaCsengetesEvent(AllomasNev allomasNev, Irany irany)
    {
        // Arrange
        MockAllomasFelepit(allomasNev);
        // Act
        ActRaiseGrpcVisszaCsengetesEvent(irany);
        // Assert
        AssertVisszaCsengetesRaised(irany);
    }


    private void ActRaiseGrpcVisszaCsengetesEvent(Irany irany)
    {
        var request = GrpcRequestFactory.CreateVisszaCsengetesRequest(GetSzomszedAllomasNev(irany), CsengetesTestsUtil.GetKimenoCsengetes(irany));
        var eventArgs = new GrpcVisszaCsengetesEventArgs(request);
        GrpcServerMock.Raise(a => a.GrpcVisszaCsengetesEvent += null, eventArgs);
    }

    private void AssertVisszaCsengetesRaised(Irany irany)
    {
        AssertEventRaisedBySzomszed(irany);
        CollectionAssert.AreEqual(CsengetesTestsUtil.GetKimenoCsengetes(irany), EventArgs!.Csengetesek);
    }
}