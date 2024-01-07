namespace Gyermekvasut.Modellek.Palya;

public abstract class PalyaElem : IPalyaElem
{
    public string Nev { get; }

    protected PalyaElem(string nev)
    {
        Nev = nev;
    }

    public abstract void Szomszedolas(Irany irany, PalyaElem szomszed);
    public abstract PalyaElem? Kovetkezo(Irany irany);

    public TPalyaElem? GetKovetkezoFeltetelesPalyaElem<TPalyaElem>(Irany irany,
            Func<TPalyaElem, bool>? feltetel = null)
        where TPalyaElem : PalyaElem
    {
        PalyaElem? iter = this;
        while ((iter = iter.Kovetkezo(irany)) != null)
        {
            if (iter is TPalyaElem tipusosIter
                && (feltetel == null || feltetel.Invoke(tipusosIter)))
            {
                return tipusosIter;
            }
        }
        return null;
    }

    public override string ToString() => Nev;
}
