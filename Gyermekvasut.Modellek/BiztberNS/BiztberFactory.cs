using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Modellek.BiztberNS;

public abstract class BiztberFactory<TBiztber>
    where TBiztber : Biztber
{
    protected Allomas Allomas { get; }

    protected BiztberFactory(Allomas allomas)
    {
        Allomas = allomas;
    }

    public abstract TBiztber Create();
}
