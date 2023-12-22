namespace Gyermekvasut.Modellek.Topologia.ObjektumSzerepek;

public class ValtoSzerep : TopologiaiObjektumSzerep
{
    public Irany AllomasOldal { get; }

    private static Dictionary<Irany, ValtoSzerep> Valtok { get; } = new();

    public static readonly ValtoSzerep Valto1 = new("1-es váltó", Irany.VegpontFele);
    public static readonly ValtoSzerep Valto2 = new("2-es váltó", Irany.KezdopontFele);

    private ValtoSzerep(string nev, Irany allomasOldal) : base(nev)
    {
        AllomasOldal = allomasOldal;
        Valtok.Add(allomasOldal, this);
    }

    public static ValtoSzerep GetByOldal(Irany allomasOldal)
        => Valtok[allomasOldal];
}