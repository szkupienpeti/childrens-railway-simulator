using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Palya.Jelzok;
using Gyermekvasut.Modellek.Topologia.ObjektumSzerepek;

namespace Gyermekvasut.Modellek.Topologia;

public record AllomasiTopologiaAdatok(
    AltalanosAllomasiTopologiaAdat AltalanosAllomasAdat,
    Dictionary<Irany, AllomasOldalTopologiaAdat> AllomasOldalAdatok);

public record AltalanosAllomasiTopologiaAdat(
    AllomasNev AllomasNev,
    JelzoForma JelzoForma,
    ElojelzoTipus ElojelzoTipus,
    int ValtoAllitasIdo,
    Dictionary<VaganySzerep, ValtoAllas> VaganyValtoAllasok,
    Dictionary<VaganySzerep, int> VaganyHosszok,
    Dictionary<TopologiaiObjektumSzerep, Szelvenyszam> Szelvenyszamok,
    Dictionary<TopologiaiObjektumSzerep, string> NevFelulirasok);

public record AllomasOldalTopologiaAdat(
    AllomasNev AllomasNev,
    Irany AllomasOldal,
    AllomasNev? SzomszedAllomasNev,
    int AllomaskozHossz,
    ValtoTajolas ValtoTajolas);
