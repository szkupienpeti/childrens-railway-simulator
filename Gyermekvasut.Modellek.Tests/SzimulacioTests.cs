using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Modellek.Tests;

[TestClass]
public class SzimulacioTests : SzimulaciosTestBase
{
    public override void TestInitialize()
    {
        // Do not initialize Szimulacio
    }

    [TestMethod]
    public void Build_WhenNotBuilt_ShouldBuild()
    {
        // Act
        Szimulacio.Build(SebessegSzorzo);
        // Assert
        Assert.IsNotNull(Szimulacio.Instance);
    }

    [TestMethod]
    public void Build_WhenAlreadyBuilt_ShouldThrow()
    {
        // Arrange
        Szimulacio.Build(SebessegSzorzo);
        // Act and assert
        var exception = Assert.ThrowsException<InvalidOperationException>(() => Szimulacio.Build(SebessegSzorzo));
        Assert.AreEqual("A szimuláció már felépült", exception.Message);
    }

    [TestMethod]
    public void Start_WhenNotStarted_ShouldStart()
    {
        // Arrange
        Szimulacio.Build(SebessegSzorzo);
        // Act
        Szimulacio.Instance.Start(KezdoIdo);
        // Assert
        Assert.IsTrue(Szimulacio.Instance.Enabled);
        Assert.IsTrue(Szimulacio.Instance.Ora.Enabled);
    }

    [TestMethod]
    public void Start_WhenAlreadyStarted_ShouldThrow()
    {
        // Arrange
        Szimulacio.Build(SebessegSzorzo);
        Szimulacio.Instance.Start(KezdoIdo);
        // Act and assert
        var exception = Assert.ThrowsException<InvalidOperationException>(() => Szimulacio.Instance.Start(KezdoIdo));
        Assert.AreEqual("A szimulációt már elindították", exception.Message);
    }

    [TestMethod]
    public void Cleanup_WhenBuilt_ShouldCleanUp()
    {
        // Arrange
        Szimulacio.Build(SebessegSzorzo);
        // Act
        Szimulacio.Cleanup();
        // Assert
        Assert.IsNull(Szimulacio.Instance);
    }
}
