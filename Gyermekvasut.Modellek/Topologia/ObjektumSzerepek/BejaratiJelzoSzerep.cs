namespace Gyermekvasut.Modellek.Topologia.ObjektumSzerepek;

public class BejaratiJelzoSzerep : FojelzoSzerep
{
    public ElojelzoSzerep? Elojelzo { get; set; }
    
    private static Dictionary<Irany, BejaratiJelzoSzerep> BejaratiJelzok { get; } = new();

    public static readonly BejaratiJelzoSzerep A = new("A", Irany.KezdopontFele);
    public static readonly BejaratiJelzoSzerep B = new("B", Irany.VegpontFele);

    private BejaratiJelzoSzerep(string nev, Irany allomasOldal) : base(nev, allomasOldal, allomasOldal.Fordit())
    {
        BejaratiJelzok.Add(allomasOldal, this);
    }

    public static BejaratiJelzoSzerep GetByOldal(Irany allomasOldal)
        => BejaratiJelzok[allomasOldal];
}
