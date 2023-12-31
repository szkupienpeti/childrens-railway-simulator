using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public abstract class RealHalozatiAllomasTestBase : HalozatiAllomasTestBase
{
    private HalozatiAllomasFactory? _allomasFactory;
    protected HalozatiAllomasFactory AllomasFactory => _allomasFactory!;

    protected void AllomasFelepit(AllomasNev allomasNev)
    {
        _allomasFactory = new();
        _allomas = AllomasFactory.Create(allomasNev);
    }
}
