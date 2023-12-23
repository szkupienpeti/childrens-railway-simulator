namespace Gyermekvasut.Modellek.Tests;

[TestClass]
public class SzimulacioTests
{
    private static readonly int SEBESSEG_SZORZO = 2;
    private static readonly TimeOnly KEZDO_IDO = new(9, 10);

    [TestMethod]
    public void Build_WhenNotBuilt_ShouldBuild()
    {
        // Act
        Szimulacio.Build(SEBESSEG_SZORZO);
        // Assert
        Assert.IsNotNull(Szimulacio.Instance);
    }

    [TestMethod]
    public void Build_WhenAlreadyBuilt_ShouldThrow()
    {
        // Arrange
        Szimulacio.Build(SEBESSEG_SZORZO);
        // Act and assert
        Assert.ThrowsException<InvalidOperationException>(() => Szimulacio.Build(SEBESSEG_SZORZO),
            "A szimuláció már felépült");
    }

    [TestMethod]
    public void Start_WhenNotStarted_ShouldStart()
    {
        // Arrange
        Szimulacio.Build(SEBESSEG_SZORZO);
        // Act
        Szimulacio.Instance.Start(KEZDO_IDO);
        // Assert
        Assert.IsTrue(Szimulacio.Instance.Enabled);
        Assert.IsTrue(Szimulacio.Instance.Ora.Enabled);
    }

    [TestMethod]
    public void Start_WhenAlreadyStarted_ShouldThrow()
    {
        // Arrange
        Szimulacio.Build(SEBESSEG_SZORZO);
        Szimulacio.Instance.Start(KEZDO_IDO);
        // Act and assert
        Assert.ThrowsException<InvalidOperationException>(() => Szimulacio.Instance.Start(KEZDO_IDO),
            "A szimulációt már elindították");
    }

    [TestMethod]
    public void CleanUp_WhenBuilt_ShouldCleanUp()
    {
        // Arrange
        Szimulacio.Build(SEBESSEG_SZORZO);
        // Act
        Szimulacio.CleanUp();
        // Assert
        Assert.IsNull(Szimulacio.Instance);
    }

    [TestCleanup]
    public void TestCleanUp()
    {
        Szimulacio.CleanUp();
    }
}
