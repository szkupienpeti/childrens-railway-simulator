namespace Gyermekvasut.Modellek.Palya;

public abstract class PalyaElem
{
    public string Nev { get; }

    public PalyaElem(string nev)
    {
        Nev = nev;
    }

    public abstract void Szomszedolas(Irany irany, PalyaElem szomszed);
    public abstract PalyaElem? Kovetkezo(Irany irany);

    public override string ToString() => Nev;
}
