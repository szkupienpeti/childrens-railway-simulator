using Gyermekvasut.Modellek.Ido;
using Moq;

namespace Gyermekvasut.Modellek.Tests;

[TestClass]
public class KozpontiOraTests
{
    private static readonly int SEBESSEG_SZORZO = 2;
    private static readonly TimeOnly KEZDO_IDO = new(9, 10);

    [TestMethod]
    public void Start_WhenNotStarted_ShouldStart()
    {
        // Arrange
        var timer = new Mock<ITimer>();
        var ora = new KozpontiOra(SEBESSEG_SZORZO, timer.Object);
        // Act
        ora.Start(KEZDO_IDO);
        // Assert
        Assert.IsTrue(ora.Enabled);
    }

    [TestMethod]
    public void Start_WhenAlreadyStarted_ShouldThrow()
    {
        // Arrange
        var timer = new Mock<ITimer>();
        var ora = new KozpontiOra(SEBESSEG_SZORZO, timer.Object);
        ora.Start(KEZDO_IDO);
        // Act and assert
        Assert.ThrowsException<InvalidOperationException>(() => ora.Start(KEZDO_IDO),
            "A központi órát már elindították");
    }

    [TestMethod]
    public void KozpontiIdo_WhenTimerElapsed_ShouldPercetLep()
    {
        // Arrange
        var timer = new Mock<ITimer>();
        var ora = new KozpontiOra(SEBESSEG_SZORZO, timer.Object);
        ora.Start(KEZDO_IDO);
        // Act
        timer.Raise(t => t.Elapsed += null, EventArgs.Empty);
        // Assert
        Assert.AreEqual(KEZDO_IDO.AddMinutes(1), ora.KozpontiIdo);
    }

    [TestMethod]
    public void KozpontiIdo_WhenTimerElapsed_ShouldRaiseEvent()
    {
        // Arrange
        var timer = new Mock<ITimer>();
        var ora = new KozpontiOra(SEBESSEG_SZORZO, timer.Object);
        var eventRaised = false;
        ora.KozpontiIdoChanged += delegate (object? sender, EventArgs e)
        {
            eventRaised = true;
        };
        ora.Start(KEZDO_IDO);
        // Act
        timer.Raise(t => t.Elapsed += null, EventArgs.Empty);
        // Assert
        Assert.IsTrue(eventRaised);
    }
}
