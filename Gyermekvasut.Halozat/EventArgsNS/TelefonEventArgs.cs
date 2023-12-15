using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Halozat.EventArgsNS;
public abstract class TelefonEventArgs : EventArgs
{
    public AllomasNev Kuldo { get; }

    protected TelefonEventArgs(AllomasNev kuldo)
    {
        Kuldo = kuldo;
    }
}
