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

    public TPalyaElem? GetKovetkezoFeltetelesPalyaElem<TPalyaElem>(Irany irany,
            Func<TPalyaElem, bool>? feltetel = null)
        where TPalyaElem : PalyaElem
    {
        PalyaElem? iter = this;
        while ((iter = iter.Kovetkezo(irany)) != null)
        {
            if (iter is TPalyaElem)
            {
                TPalyaElem tipusosIter = (iter as TPalyaElem)!;
                if (feltetel == null || feltetel!.Invoke(tipusosIter))
                {
                    return tipusosIter;
                }
            }
        }
        return null;
    }

    public override string ToString() => Nev;
}
