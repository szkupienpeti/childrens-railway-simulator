using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Palya.Jelzok;
using Gyermekvasut.Modellek.Topologia.ObjektumSzerepek;

namespace Gyermekvasut.Modellek.Topologia;

internal class AllomasiTopologiaAdatokBuilder
{
    private AllomasNev AllomasNev { get; }
    private JelzoForma JelzoForma { get; }
    private ElojelzoTipus ElojelzoTipus { get; }
    private int ValtoAllitasIdo { get; }
    public Dictionary<VaganySzerep, ValtoAllas> VaganyValtoAllasok { get; } = new();
    public Dictionary<VaganySzerep, int> VaganyHosszok { get; } = new();
    public Dictionary<TopologiaiObjektumSzerep, Szelvenyszam> Szelvenyszamok { get; } = new();
    public Dictionary<TopologiaiObjektumSzerep, string> NevFelulirasok { get; } = new();
    public Dictionary<Irany, AllomasOldalTopologiaAdat> AllomasOldalAdatok { get; } = new();

    public AllomasiTopologiaAdatokBuilder(AllomasNev allomasNev,
        JelzoForma jelzoForma, ElojelzoTipus elojelzoTipus, int valtoAllitasIdo)
    {
        AllomasNev = allomasNev;
        JelzoForma = jelzoForma;
        ElojelzoTipus = elojelzoTipus;
        ValtoAllitasIdo = valtoAllitasIdo;
    }

    public AllomasiTopologiaAdatokBuilder AddVaganyValtoAllas(VaganySzerep vaganySzerep, ValtoAllas valtoAllas)
    {
        VaganyValtoAllasok.Add(vaganySzerep, valtoAllas);
        return this;
    }

    public AllomasiTopologiaAdatokBuilder AddVaganyHossz(VaganySzerep vaganySzerep, int vaganyHossz)
    {
        VaganyHosszok.Add(vaganySzerep, vaganyHossz);
        return this;
    }

    public AllomasiTopologiaAdatokBuilder AddSzelvenyszam(TopologiaiObjektumSzerep topologiaiObjektumSzerep,
        Szelvenyszam szelvenyszam)
    {
        if (JelzoForma == JelzoForma.AlakJelzo && topologiaiObjektumSzerep is IsmetlojelzoSzerep)
        {
            throw new ArgumentException("Alakjelzős állomáson nem lehet ismétlőjelző");
        }
        Szelvenyszamok.Add(topologiaiObjektumSzerep, szelvenyszam);
        return this;
    }
    public AllomasiTopologiaAdatokBuilder AddNevFeluliras(TopologiaiObjektumSzerep topologiaiObjektumSzerep, string nevFeluliras)
    {
        NevFelulirasok.Add(topologiaiObjektumSzerep, nevFeluliras);
        return this;
    }

    public AllomasiTopologiaAdatokBuilder AddAllomasOldalAdat(Irany allomasOldal, int allomaskozHossz, ValtoTajolas valtoTajolas)
    {
        AllomasOldalAdatok.Add(allomasOldal, new(AllomasNev, allomasOldal, AllomasNev.Szomszed(allomasOldal),
            allomaskozHossz, valtoTajolas));
        return this;
    }


    public AllomasiTopologiaAdatok Build()
    {
        bool mindketOldalonKijaratiJelzok = HasKijaratiJelzo(Irany.KezdopontFele) && HasKijaratiJelzo(Irany.VegpontFele);
        bool vaganyHosszMegadva = VaganyHosszok.Count > 0;
        if (mindketOldalonKijaratiJelzok == vaganyHosszMegadva)
        {
            throw new InvalidOperationException("Inkonzisztens kijárati jelző és vágányhosszmegadás kombináció");
        }
        return new(new(AllomasNev, JelzoForma, ElojelzoTipus, ValtoAllitasIdo,
            VaganyValtoAllasok, VaganyHosszok, Szelvenyszamok, NevFelulirasok),
            AllomasOldalAdatok);
    }

    private bool HasKijaratiJelzo(Irany allomasOldal)
        => Szelvenyszamok.Keys
            .OfType<KijaratiJelzoSzerep>()
            .Any(kj => kj.AllomasOldal == allomasOldal);
}
