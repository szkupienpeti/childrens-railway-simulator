namespace Gyermekvasut.Modellek.Topologia.ObjektumSzerepek;

public class VaganySzerep : TopologiaiObjektumSzerep
{
    public string ArabSzam { get; }
    public string RomaiSzam { get; }

    public static readonly VaganySzerep Elso = new("1", "I.");
    public static readonly VaganySzerep Masodik = new("2", "II.");

    private VaganySzerep(string arabSzam, string romaiSzam)
        : base($"{romaiSzam} vágány")
    {
        ArabSzam = arabSzam;
        RomaiSzam = romaiSzam;
    }
}