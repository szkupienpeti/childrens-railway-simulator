using Gyermekvasut.Modellek.Ido;
using Gyermekvasut.Tests.Util;
using Moq;

namespace Gyermekvasut.Modellek.Tests;

[TestClass]
public class KozpontiOraTests : SzimulaciosTestBase
{
    public override void TestInitialize()
    {
        // Do not initialize Szimulacio
    }

    [TestMethod]
    public void Start_WhenNotStarted_ShouldStart()
    {
        // Arrange
        var timer = new Mock<ITimer>();
        var ora = new KozpontiOra(timer.Object);
        // Act
        ora.Start(KezdoIdo);
        // Assert
        Assert.IsTrue(ora.Enabled);
    }

    [TestMethod]
    public void Start_WhenAlreadyStarted_ShouldThrow()
    {
        // Arrange
        var timer = new Mock<ITimer>();
        var ora = new KozpontiOra(timer.Object);
        ora.Start(KezdoIdo);
        // Act and assert
        Assert.ThrowsException<InvalidOperationException>(() => ora.Start(KezdoIdo),
            "A központi órát már elindították");
    }

    [TestMethod]
    public void KozpontiIdo_WhenTimerElapsed_ShouldPercetLep()
    {
        // Arrange
        var timer = new Mock<ITimer>();
        var ora = new KozpontiOra(timer.Object);
        ora.Start(KezdoIdo);
        // Act
        timer.Raise(t => t.Elapsed += null, EventArgs.Empty);
        // Assert
        Assert.AreEqual(KezdoIdo.AddMinutes(1), ora.KozpontiIdo);
    }

    [TestMethod]
    public void KozpontiIdo_WhenTimerElapsed_ShouldRaiseEvent()
    {
        // Arrange
        var timer = new Mock<ITimer>();
        var ora = new KozpontiOra(timer.Object);
        var eventRaised = false;
        ora.KozpontiIdoChanged += delegate
        {
            eventRaised = true;
        };
        ora.Start(KezdoIdo);
        // Act
        timer.Raise(t => t.Elapsed += null, EventArgs.Empty);
        // Assert
        Assert.IsTrue(eventRaised);
    }
}
