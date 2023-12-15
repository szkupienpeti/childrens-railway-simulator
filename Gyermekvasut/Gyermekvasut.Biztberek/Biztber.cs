using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Biztberek;

public abstract class Biztber
{
    public Allomas Allomas { get; }

    public Biztber(Allomas allomas)
    {
        Allomas = allomas;
    }
}
