namespace Gyermekvasut.Modellek.Topologia.ObjektumSzerepek;

public abstract class FojelzoSzerep : TopologiaiObjektumSzerep
{
    public Irany AllomasOldal { get; }
    public Irany JelzoIrany { get; }
    public IsmetlojelzoSzerep? Ismetlojelzo { get; set; }

    protected FojelzoSzerep(string nev, Irany allomasOldal, Irany jelzoIrany) : base(nev)
    {
        AllomasOldal = allomasOldal;
        JelzoIrany = jelzoIrany;
    }
}
