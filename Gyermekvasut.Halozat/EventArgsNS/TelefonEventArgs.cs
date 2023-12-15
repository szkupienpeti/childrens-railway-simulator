using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Halozat.EventArgsNS;
public abstract class TelefonEventArgs : EventArgs
{
    public AllomasNev Kuldo { get; }

    public TelefonEventArgs(AllomasNev kuldo)
    {
        Kuldo = kuldo;
    }
}
