namespace Gyermekvasut.Modellek.Topologia.ObjektumSzerepek;

public class TolatasjelzoSzerep : TopologiaiObjektumSzerep
{
    public static readonly TolatasjelzoSzerep T1 = new("T1");
    public static readonly TolatasjelzoSzerep E1 = new("E1");
    public static readonly TolatasjelzoSzerep E2 = new("E2");

    private TolatasjelzoSzerep(string nev) : base(nev) { }
}
