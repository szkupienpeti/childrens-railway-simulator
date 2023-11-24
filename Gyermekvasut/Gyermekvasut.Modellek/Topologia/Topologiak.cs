using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Palya.Jelzok;

namespace Gyermekvasut.Modellek.Topologia;

public static class Topologiak
{
    public static AllomasiTopologia Felepit(AllomasNev allomasNev)
    {
        return allomasNev switch
        {
            AllomasNev.Szechenyihegy => throw new NotImplementedException(),
            AllomasNev.Csilleberc => throw new NotImplementedException(),
            AllomasNev.Viragvolgy => throw new NotImplementedException(),
            AllomasNev.Janoshegy => JanoshegyFelepit(),
            AllomasNev.Szepjuhaszne => SzepjuhaszneFelepit(),
            AllomasNev.Harshegy => throw new NotImplementedException(),
            AllomasNev.Huvosvolgy => throw new NotImplementedException(),
            _ => throw new NotImplementedException()
        };
    }
    private static AllomasiTopologia JanoshegyFelepit()
    {
        // TODO Just copied from Szepjuhaszne, change it!!!
        /* 
         * A (I)                                       (H) O
         *       AEj - A - 2--K2  II.  V2--1 - B - BEj
         *                  \-K1   I.  V1-/
         */
        Szakasz allomaskozJanoshegy = new("I-S állomásköz", 3000);
        Elojelzo aej = new("AEj", VonatNS.Irany.Paros, JelzoForma.FenyJelzo, ElojelzoTipus.HaromFogalmu);
        Szakasz aej_a = new("AEj-A", 400);
        Fojelzo a = new("A", VonatNS.Irany.Paros, JelzoForma.FenyJelzo, FojelzoRendeltetes.Bejarati);
        Szakasz csucs2 = new("2-es váltó csúcs", 100);
        Valto valto2 = new("2", VonatNS.Irany.Paros, ValtoTajolas.Jobbos, 3);
        Szakasz valto2e = new("2-es váltó E", 100);
        Szakasz valto2k = new("2-es váltó K", 100);
        Fojelzo k2 = new("K2", VonatNS.Irany.Paratlan, JelzoForma.FenyJelzo, FojelzoRendeltetes.Kijarati);
        Fojelzo k1 = new("K1", VonatNS.Irany.Paratlan, JelzoForma.FenyJelzo, FojelzoRendeltetes.Kijarati);
        Vagany vg2 = new("II.", AllomasNev.Szepjuhaszne, 100);
        Vagany vg1 = new("I.", AllomasNev.Szepjuhaszne, 100);
        Fojelzo v2 = new("V2", VonatNS.Irany.Paros, JelzoForma.FenyJelzo, FojelzoRendeltetes.Kijarati);
        Fojelzo v1 = new("V1", VonatNS.Irany.Paros, JelzoForma.FenyJelzo, FojelzoRendeltetes.Kijarati);
        Szakasz valto1e = new("1-es váltó E", 100);
        Szakasz valto1k = new("1-es váltó K", 100);
        Valto valto1 = new("1", VonatNS.Irany.Paratlan, ValtoTajolas.Balos, 3);
        Szakasz csucs1 = new("1-es váltó csúcs", 100);
        Fojelzo b = new("B", VonatNS.Irany.Paratlan, JelzoForma.FenyJelzo, FojelzoRendeltetes.Bejarati);
        Szakasz b_bej = new("B-BEj", 400);
        Elojelzo bej = new("BEj", VonatNS.Irany.Paratlan, JelzoForma.FenyJelzo, ElojelzoTipus.HaromFogalmu);
        Szakasz allomaskozHarshegy = new("S-H állomásköz", 3000);
        AllomasiTopologia topologia = new(allomaskozJanoshegy, allomaskozHarshegy);
        topologia.EgyenesFeltolt(allomaskozJanoshegy, aej, aej_a, a, csucs2, valto2, valto2e, k2,
            vg2, v2, valto1e, valto1, csucs1, b, b_bej, bej, allomaskozHarshegy);
        topologia.EgyenesFeltolt(valto2k, k1, vg1, v1, valto1k);
        topologia.ValtoKiteroSzomszedol(valto2, valto2k);
        topologia.ValtoKiteroSzomszedol(valto1, valto1k);
        return topologia;
    }

    private static AllomasiTopologia SzepjuhaszneFelepit()
    {
        /* 
         * A (I)                                       (H) O
         *       AEj - A - 2--K2  II.  V2--1 - B - BEj
         *                  \-K1   I.  V1-/
         */
        Szakasz allomaskozJanoshegy = new("I-S állomásköz", 3000);
        Elojelzo aej = new("AEj", VonatNS.Irany.Paros, JelzoForma.FenyJelzo, ElojelzoTipus.HaromFogalmu);
        Szakasz aej_a = new("AEj-A", 400);
        Fojelzo a = new("A", VonatNS.Irany.Paros, JelzoForma.FenyJelzo, FojelzoRendeltetes.Bejarati);
        Szakasz csucs2 = new("2-es váltó csúcs", 100);
        Valto valto2 = new("2", VonatNS.Irany.Paros, ValtoTajolas.Jobbos, 3);
        Szakasz valto2e = new("2-es váltó E", 100);
        Szakasz valto2k = new("2-es váltó K", 100);
        Fojelzo k2 = new("K2", VonatNS.Irany.Paratlan, JelzoForma.FenyJelzo, FojelzoRendeltetes.Kijarati);
        Fojelzo k1 = new("K1", VonatNS.Irany.Paratlan, JelzoForma.FenyJelzo, FojelzoRendeltetes.Kijarati);
        Vagany vg2 = new("II.", AllomasNev.Szepjuhaszne, 100);
        Vagany vg1 = new("I.", AllomasNev.Szepjuhaszne, 100);
        Fojelzo v2 = new("V2", VonatNS.Irany.Paros, JelzoForma.FenyJelzo, FojelzoRendeltetes.Kijarati);
        Fojelzo v1 = new("V1", VonatNS.Irany.Paros, JelzoForma.FenyJelzo, FojelzoRendeltetes.Kijarati);
        Szakasz valto1e = new("1-es váltó E", 100);
        Szakasz valto1k = new("1-es váltó K", 100);
        Valto valto1 = new("1", VonatNS.Irany.Paratlan, ValtoTajolas.Balos, 3);
        Szakasz csucs1 = new("1-es váltó csúcs", 100);
        Fojelzo b = new("B", VonatNS.Irany.Paratlan, JelzoForma.FenyJelzo, FojelzoRendeltetes.Bejarati);
        Szakasz b_bej = new("B-BEj", 400);
        Elojelzo bej = new("BEj", VonatNS.Irany.Paratlan, JelzoForma.FenyJelzo, ElojelzoTipus.HaromFogalmu);
        Szakasz allomaskozHarshegy = new("S-H állomásköz", 3000);
        AllomasiTopologia topologia = new(allomaskozJanoshegy, allomaskozHarshegy);
        topologia.EgyenesFeltolt(allomaskozJanoshegy, aej, aej_a, a, csucs2, valto2, valto2e, k2,
            vg2, v2, valto1e, valto1, csucs1, b, b_bej, bej, allomaskozHarshegy);
        topologia.EgyenesFeltolt(valto2k, k1, vg1, v1, valto1k);
        topologia.ValtoKiteroSzomszedol(valto2, valto2k);
        topologia.ValtoKiteroSzomszedol(valto1, valto1k);
        return topologia;
    }
}
