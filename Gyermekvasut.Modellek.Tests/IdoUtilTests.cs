using Gyermekvasut.Modellek.Ido;

namespace Gyermekvasut.Modellek.Tests;

[TestClass]
public class IdoUtilTests
{
    [TestCleanup]
    public void TestCleanup()
    {
        Szimulacio.Cleanup();
    }

    [DataTestMethod]
    [DataRow(1,     1,      1000)]
    [DataRow(2,     1,      2000)]
    [DataRow(1.5,   1,      1500)]
    [DataRow(1,     2,      500)]
    [DataRow(2,     2,      1000)]
    [DataRow(1.5,   2,      750)]
    [DataRow(1,     2.5,    400)]
    [DataRow(2,     2.5,    800)]
    [DataRow(1.5,   2.5,    600)]
    public void SecondsToTimerInterval_WhenSzimulacioNotBuilt(double seconds,
        double sebessegSzorzo, double expectedInterval)
    {
        // Act
        double calculatedInterval = IdoUtil.SecondsToTimerInterval(seconds, sebessegSzorzo);
        // Assert
        Assert.AreEqual(expectedInterval, calculatedInterval);
    }

    [TestMethod]
    public void SecondsToTimerInterval_WhenSzimulacioNotBuiltNoSzorzo_ShouldThrow()
    {
        // Act and assert
        Assert.ThrowsException<NullReferenceException>(() => IdoUtil.SecondsToTimerInterval(1));
    }

    [TestMethod]
    public void SecondsToTimerInterval_WhenNegativSeconds_ShouldThrow()
    {
        // Act and assert
        Assert.ThrowsException<ArgumentException>(() => IdoUtil.SecondsToTimerInterval(-1, 1),
            "Az időintervallum pozitív kell, hogy legyen");
    }

    [TestMethod]
    public void SecondsToTimerInterval_WhenNegativSzorzo_ShouldThrow()
    {
        // Act and assert
        Assert.ThrowsException<ArgumentException>(() => IdoUtil.SecondsToTimerInterval(1, -1),
            "A sebességszorzó pozitív kell, hogy legyen");
    }

    [DataTestMethod]
    [DataRow(1,     500)]
    [DataRow(2,     1000)]
    [DataRow(1.5,   750)]
    public void SecondsToTimerInterval_WhenSzimulacioBuilt(double seconds, double expectedInterval)
    {
        // Arrange
        Szimulacio.Build(2);
        // Act
        double calculatedInterval = IdoUtil.SecondsToTimerInterval(seconds);
        // Assert
        Assert.AreEqual(expectedInterval, calculatedInterval);
    }

    [DataTestMethod]
    [DataRow(1,     60_000)]
    [DataRow(2,     120_000)]
    [DataRow(1.5,   90_000)]
    public void MinutesToTimerInterval_WhenSzimulacioBuilt(double seconds, double expectedInterval)
    {
        // Arrange
        Szimulacio.Build(1);
        // Act
        double calculatedInterval = IdoUtil.MinutesToTimerInterval(seconds);
        // Assert
        Assert.AreEqual(expectedInterval, calculatedInterval);
    }
}
