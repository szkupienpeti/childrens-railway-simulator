namespace Gyermekvasut.Modellek.Palya.Jelzok;

public abstract class Jelzo : EgyenesPalyaElem, IHelyhezKotottPalyaElem
{
    public Szelvenyszam Szelvenyszam { get; }
    public Irany Irany { get; }
    public JelzoForma Forma { get; }

    public event EventHandler? JelzesChanged;
    protected Jelzo(string nev, Irany irany, JelzoForma forma, Szelvenyszam szelvenyszam) : base(nev)
    {
        Irany = irany;
        Forma = forma;
        Szelvenyszam = szelvenyszam;
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
