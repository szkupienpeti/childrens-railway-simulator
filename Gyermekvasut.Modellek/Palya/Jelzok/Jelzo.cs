using Gyermekvasut.Modellek.VonatNS;

namespace Gyermekvasut.Modellek.Palya.Jelzok;

public abstract class Jelzo : EgyenesPalyaElem
{
    public Irany Irany { get; }
    public JelzoForma Forma { get; }
    public event EventHandler? JelzesChanged;
    public Jelzo(string nev, Irany irany, JelzoForma forma) : base(nev)
    {
        Irany = irany;
        Forma = forma;
    }
    protected void OnJelzesChanged()
    {
        JelzesChanged?.Invoke(this, EventArgs.Empty);
    }
}

public enum JelzoForma
{
    AlakJelzo = 1,
    FenyJelzo = 2
}
