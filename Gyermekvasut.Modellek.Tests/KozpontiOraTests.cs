using Gyermekvasut.Modellek.Ido;
using Moq;

namespace Gyermekvasut.Modellek.Tests;

[TestClass]
public class KozpontiOraTests
{
    private static readonly int SEBESSEG_SZORZO = 2;

    [TestMethod]
    public void KozpontiIdo_TimerElapsed_PercetLep()
    {
        // Arrange
        var timer = new Mock<ITimer>();
        var ora = new KozpontiOra(SEBESSEG_SZORZO, timer.Object);
        bool eventRaised = false;
        ora.KozpontiIdoChanged += delegate(object? sender, EventArgs e)
        {
            eventRaised = true;
        };
        var kezdoIdo = new TimeOnly(9, 10);
        ora.Start(kezdoIdo);
        // Act
        timer.Raise(t => t.Elapsed += null, EventArgs.Empty);
        // Assert
        Assert.AreEqual(true, eventRaised);
        Assert.AreEqual(kezdoIdo.AddMinutes(1), ora.KozpontiIdo);
    }
}
