using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Modellek.BiztberNS;

public abstract class Biztber
{
    public Allomas Allomas { get; }

    protected Biztber(Allomas allomas)
    {
        Allomas = allomas;
    }
}
