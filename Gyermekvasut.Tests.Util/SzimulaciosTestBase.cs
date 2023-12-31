using Gyermekvasut.Modellek;

namespace Gyermekvasut.Tests.Util;

[TestClass]
public abstract class SzimulaciosTestBase
{
    protected virtual int SebessegSzorzo { get; } = 2;
    protected virtual TimeOnly KezdoIdo { get; } = new(9, 10);

    [TestInitialize]
    public virtual void TestInitialize()
    {
        Szimulacio.Build(SebessegSzorzo);
    }

    [TestCleanup]
    public virtual void TestCleanup()
    {
        Szimulacio.Cleanup();
    }
}
