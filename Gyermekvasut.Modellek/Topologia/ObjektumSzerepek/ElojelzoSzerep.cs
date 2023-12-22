namespace Gyermekvasut.Modellek.Topologia.ObjektumSzerepek;

public class ElojelzoSzerep : TopologiaiObjektumSzerep
{
    public BejaratiJelzoSzerep BejaratiJelzo { get; }

    private static readonly string ELOJELZO_POSTFIX = "Ej";
    public static readonly ElojelzoSzerep AEj = new(BejaratiJelzoSzerep.A);
    public static readonly ElojelzoSzerep BEj = new(BejaratiJelzoSzerep.B);

    private ElojelzoSzerep(BejaratiJelzoSzerep bejaratiJelzo)
        : base($"{bejaratiJelzo.Nev}{ELOJELZO_POSTFIX}")
    {
        BejaratiJelzo = bejaratiJelzo;
        BejaratiJelzo.Elojelzo = this;
    }
}
