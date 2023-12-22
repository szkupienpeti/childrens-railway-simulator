namespace Gyermekvasut.Modellek.Topologia.ObjektumSzerepek;

public class KijaratiJelzoSzerep : FojelzoSzerep
{
    public VaganySzerep Vagany { get; }

    private static Dictionary<VaganySzerep, Dictionary<Irany, KijaratiJelzoSzerep>> VaganyKijaratiJelzok { get; } = new();

    public static readonly KijaratiJelzoSzerep K1 = new(VaganySzerep.Elso, Irany.KezdopontFele);
    public static readonly KijaratiJelzoSzerep V1 = new(VaganySzerep.Elso, Irany.VegpontFele);
    // Hűvösvölgy I. vágányt felező B jelű főjelző
    public static readonly KijaratiJelzoSzerep V1_B = new(VaganySzerep.Elso, Irany.VegpontFele, false);
    public static readonly KijaratiJelzoSzerep K2 = new(VaganySzerep.Masodik, Irany.KezdopontFele);
    public static readonly KijaratiJelzoSzerep V2 = new(VaganySzerep.Masodik, Irany.VegpontFele);

    private KijaratiJelzoSzerep(VaganySzerep vagany, Irany allomasOldal, bool figyelembeVesz = true)
        : base($"{GetKijaratiJelzoPrefix(allomasOldal)}{vagany.ArabSzam}", allomasOldal, allomasOldal)
    {
        Vagany = vagany;
        if (figyelembeVesz)
        {
            VaganyKijaratiJelzok.TryAdd(vagany, new());
            VaganyKijaratiJelzok[vagany].Add(allomasOldal, this);
        }
    }

    public static KijaratiJelzoSzerep GetByVaganyOldal(VaganySzerep vagany, Irany allomasOldal)
        => VaganyKijaratiJelzok[vagany][allomasOldal];

    private static string GetKijaratiJelzoPrefix(Irany allomasOldal)
    => allomasOldal switch
    {
        Irany.KezdopontFele => "K",
        Irany.VegpontFele => "V",
        _ => throw new NotImplementedException()
    };
}
