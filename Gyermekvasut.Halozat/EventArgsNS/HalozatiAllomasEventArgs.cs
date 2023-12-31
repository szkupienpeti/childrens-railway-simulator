using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Halozat.EventArgsNS;
public abstract class HalozatiAllomasEventArgs : EventArgs
{
    public AllomasNev Kuldo { get; }

    protected HalozatiAllomasEventArgs(AllomasNev kuldo)
    {
        Kuldo = kuldo;
    }
}
