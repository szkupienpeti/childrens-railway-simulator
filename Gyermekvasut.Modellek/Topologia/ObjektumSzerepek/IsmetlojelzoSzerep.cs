namespace Gyermekvasut.Modellek.Topologia.ObjektumSzerepek;

public class IsmetlojelzoSzerep : TopologiaiObjektumSzerep
{
    public FojelzoSzerep Fojelzo { get; }

    private static readonly string ISMETLOJELZO_POSTFIX = "Ism";
    public static readonly IsmetlojelzoSzerep AIsm = new(BejaratiJelzoSzerep.A);
    public static readonly IsmetlojelzoSzerep BIsm = new(BejaratiJelzoSzerep.B);

    private IsmetlojelzoSzerep(FojelzoSzerep fojelzo)
        : base($"{fojelzo.Nev}{ISMETLOJELZO_POSTFIX}")
    {
        Fojelzo = fojelzo;
        Fojelzo.Ismetlojelzo = this;
    }
}
