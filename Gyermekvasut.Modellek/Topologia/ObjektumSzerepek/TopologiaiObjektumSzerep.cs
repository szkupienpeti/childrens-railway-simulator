namespace Gyermekvasut.Modellek.Topologia.ObjektumSzerepek;

public abstract class TopologiaiObjektumSzerep
{
    public string Nev { get; }

    protected TopologiaiObjektumSzerep(string nev)
    {
        Nev = nev;
    }

    public override string ToString() => Nev;
}
