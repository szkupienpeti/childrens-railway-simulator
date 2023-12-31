using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Palya.Jelzok;
using Gyermekvasut.Modellek.Topologia.ObjektumSzerepek;

namespace Gyermekvasut.Modellek.Topologia;

public static class AllomasiTopologiaAdatokFactory
{
    public static AllomasiTopologiaAdatok Create(AllomasNev allomasNev)
        => allomasNev switch
        {
            AllomasNev.Szechenyihegy => CreateSzechenyihegy(),
            AllomasNev.Csilleberc => CreateCsilleberc(),
            AllomasNev.Viragvolgy => CreateViragvolgy(),
            AllomasNev.Janoshegy => CreateJanoshegy(),
            AllomasNev.Szepjuhaszne => CreateSzepjuhaszne(),
            AllomasNev.Harshegy => CreateHarshegy(),
            AllomasNev.Huvosvolgy => CreateHuvosvolgy(),
            _ => throw new NotImplementedException()
        };

    // TODO Szelvényszámokat külön tárolni, hogy az állomásközök tudják azt felhasználni, ne kelljen duplikálni a számokat

    private static readonly int SZECHENYIHEGY_TV_CSONKA_HOSSZ = 40;
    /// <summary>
    /// <code>
    ///        Széchenyihegy             (U) O
    ///        /-  II.  C-\
    ///       /            3-- Tj1 - ...
    /// 	 /	  		    \
    /// [-- 2----   I.  B----1
    ///                       \- A - AEj
    /// </code>
    /// </summary>
    private static AllomasiTopologiaAdatok CreateSzechenyihegy()
        => new AllomasiTopologiaAdatokBuilder(AllomasNev.Szechenyihegy,
                JelzoForma.AlakJelzo, ElojelzoTipus.HaromFogalmu)
            // Vágánytengelyugrás: páratlan váltó(körzet) állásai
            .AddVaganyValtoAllas(VaganySzerep.Elso, ValtoAllas.Kitero)
            .AddVaganyValtoAllas(VaganySzerep.Masodik, ValtoAllas.Egyenes)
            .AddVaganyHossz(VaganySzerep.Elso, 110)
            .AddVaganyHossz(VaganySzerep.Masodik, 98)
            .AddAllomasOldalAdat(Irany.KezdopontFele, SZECHENYIHEGY_TV_CSONKA_HOSSZ, ValtoTajolas.Balos)
            .AddAllomasOldalAdat(Irany.VegpontFele, 1018 - 0613, ValtoTajolas.Jobbos)
            .AddSzelvenyszam(ValtoSzerep.Valto2,        new(00, -62))
            .AddSzelvenyszam(KijaratiJelzoSzerep.V1,    new(00, 65))
            .AddNevFeluliras(KijaratiJelzoSzerep.V1,    "B")
            .AddSzelvenyszam(KijaratiJelzoSzerep.V2,    new(00, 65))
            .AddNevFeluliras(KijaratiJelzoSzerep.V2,    "C")
            .AddSzelvenyszam(ValtoSzerep.Valto1,        new(01, 01))
            .AddSzelvenyszam(BejaratiJelzoSzerep.B,     new(02, 11))
            .AddNevFeluliras(BejaratiJelzoSzerep.B,     "A")
            .AddSzelvenyszam(ElojelzoSzerep.BEj,        new(06, 13))
            .AddNevFeluliras(ElojelzoSzerep.BEj,        "AEj")
            .Build();

    /// <summary>
    /// <code>
    /// A               Csillebérc                     (L) O
    ///   AEj - A - 2--K1   I.  V1--1 - B - BIsm - BEj
    ///              \-K2  II.  V2-/
    /// </code>
    /// </summary>
    private static AllomasiTopologiaAdatok CreateCsilleberc()
        => new AllomasiTopologiaAdatokBuilder(AllomasNev.Csilleberc,
                JelzoForma.FenyJelzo, ElojelzoTipus.HaromFogalmu)
            .AddVaganyValtoAllas(VaganySzerep.Elso, ValtoAllas.Egyenes)
            .AddVaganyValtoAllas(VaganySzerep.Masodik, ValtoAllas.Kitero)
            .AddAllomasOldalAdat(Irany.KezdopontFele, 1018 - 0613, ValtoTajolas.Balos)
            .AddAllomasOldalAdat(Irany.VegpontFele, 2389 - 2314, ValtoTajolas.Jobbos)
            .AddSzelvenyszam(ElojelzoSzerep.AEj,        new(10, 18))
            .AddSzelvenyszam(BejaratiJelzoSzerep.A,     new(14, 37))
            .AddSzelvenyszam(ValtoSzerep.Valto2,        new(15, 93))
            .AddSzelvenyszam(KijaratiJelzoSzerep.K1,    new(16, 27))
            .AddSzelvenyszam(KijaratiJelzoSzerep.K2,    new(16, 27))
            .AddSzelvenyszam(KijaratiJelzoSzerep.V1,    new(17, 19))
            .AddSzelvenyszam(KijaratiJelzoSzerep.V2,    new(17, 19))
            .AddSzelvenyszam(ValtoSzerep.Valto1,        new(17, 52))
            .AddSzelvenyszam(BejaratiJelzoSzerep.B,     new(19, 01))
            .AddSzelvenyszam(IsmetlojelzoSzerep.BIsm,   new(19, 90))
            .AddSzelvenyszam(ElojelzoSzerep.BEj,        new(23, 14))
            .Build();

    /// <summary>
    /// <code>
    ///  A (U)             Virágvölgy            (I) O
    ///                   /-  II.  -\
    ///        AEj - A - 2--   I.  --1 - B - BEj
    /// </code>
    /// </summary>
    private static AllomasiTopologiaAdatok CreateViragvolgy()
        => new AllomasiTopologiaAdatokBuilder(AllomasNev.Viragvolgy,
                JelzoForma.AlakJelzo, ElojelzoTipus.KetFogalmu)
            .AddVaganyValtoAllas(VaganySzerep.Elso, ValtoAllas.Egyenes)
            .AddVaganyValtoAllas(VaganySzerep.Masodik, ValtoAllas.Kitero)
            .AddVaganyHossz(VaganySzerep.Elso, 74)
            .AddVaganyHossz(VaganySzerep.Masodik, 74)
            .AddAllomasOldalAdat(Irany.KezdopontFele, 2389 - 2314, ValtoTajolas.Balos)
            .AddAllomasOldalAdat(Irany.VegpontFele, 3861 - 3555, ValtoTajolas.Jobbos)
            .AddSzelvenyszam(ElojelzoSzerep.AEj,    new(23, 89))
            .AddSzelvenyszam(BejaratiJelzoSzerep.A, new(27, 91))
            .AddSzelvenyszam(ValtoSzerep.Valto2,    new(28, 88))
            .AddSzelvenyszam(ValtoSzerep.Valto1,    new(30, 49))
            .AddSzelvenyszam(BejaratiJelzoSzerep.B, new(31, 47))
            .AddSzelvenyszam(ElojelzoSzerep.BEj,    new(35, 55))
            .Build();

    /// <summary>
    /// <code>
    ///  O (S)             Jánoshegy             (L) A
    ///                   /-  II.  -\
    ///        BEj - B - 1--   I.  --2 - A - AEj
    /// </code>
    /// </summary>
    private static AllomasiTopologiaAdatok CreateJanoshegy()
        => new AllomasiTopologiaAdatokBuilder(AllomasNev.Janoshegy,
                JelzoForma.AlakJelzo, ElojelzoTipus.KetFogalmu)
            .AddVaganyValtoAllas(VaganySzerep.Elso, ValtoAllas.Egyenes)
            .AddVaganyValtoAllas(VaganySzerep.Masodik, ValtoAllas.Kitero)
            .AddVaganyHossz(VaganySzerep.Elso, 58)
            .AddVaganyHossz(VaganySzerep.Masodik, 58)
            .AddAllomasOldalAdat(Irany.KezdopontFele, 3861 - 3555, ValtoTajolas.Jobbos)
            .AddAllomasOldalAdat(Irany.VegpontFele, 6108 - 5065, ValtoTajolas.Balos)
            .AddSzelvenyszam(ElojelzoSzerep.AEj,    new(38, 61))
            .AddSzelvenyszam(BejaratiJelzoSzerep.A, new(43, 67))
            .AddSzelvenyszam(ValtoSzerep.Valto2,    new(44, 71))
            .AddSzelvenyszam(ValtoSzerep.Valto1,    new(46, 01))
            .AddSzelvenyszam(BejaratiJelzoSzerep.B, new(47, 02))
            .AddSzelvenyszam(ElojelzoSzerep.BEj,    new(50, 65))
            .Build();

    /// <summary>
    /// <code>
    /// A (I)                    Szépjuhászné              (H) O
    ///       AEj - AIsm - A - 2--K2  II.  V2--1 - B - BEj
    ///                         \-K1   I.  V1-/
    /// </code>
    /// </summary>
    private static AllomasiTopologiaAdatok CreateSzepjuhaszne()
        => new AllomasiTopologiaAdatokBuilder(AllomasNev.Szepjuhaszne,
                JelzoForma.FenyJelzo, ElojelzoTipus.HaromFogalmu)
            .AddVaganyValtoAllas(VaganySzerep.Elso, ValtoAllas.Kitero)
            .AddVaganyValtoAllas(VaganySzerep.Masodik, ValtoAllas.Egyenes)
            .AddAllomasOldalAdat(Irany.KezdopontFele, 6108 - 5065, ValtoTajolas.Jobbos)
            .AddAllomasOldalAdat(Irany.VegpontFele, 8139 - 7335, ValtoTajolas.Balos)
            .AddSzelvenyszam(ElojelzoSzerep.AEj,        new(61, 08))
            .AddSzelvenyszam(IsmetlojelzoSzerep.AIsm,   new(64, 10))
            .AddSzelvenyszam(BejaratiJelzoSzerep.A,     new(65, 08))
            .AddSzelvenyszam(ValtoSzerep.Valto2,        new(66, 20))
            .AddSzelvenyszam(KijaratiJelzoSzerep.K1,    new(66, 64))
            .AddSzelvenyszam(KijaratiJelzoSzerep.K2,    new(66, 64))
            .AddSzelvenyszam(KijaratiJelzoSzerep.V1,    new(67, 50))
            .AddSzelvenyszam(KijaratiJelzoSzerep.V2,    new(67, 50))
            .AddSzelvenyszam(ValtoSzerep.Valto1,        new(68, 00))
            .AddSzelvenyszam(BejaratiJelzoSzerep.B,     new(69, 33))
            .AddSzelvenyszam(ElojelzoSzerep.BEj,        new(73, 35))
            .Build();

    /// <summary>
    /// <code>
    ///  A (S)             Hárshegy              O
    ///                   /-  II.  -\
    ///        AEj - A - 2--   I.  --1 - B - BEj
    /// </code>
    /// </summary>
    private static AllomasiTopologiaAdatok CreateHarshegy()
        => new AllomasiTopologiaAdatokBuilder(AllomasNev.Harshegy,
                JelzoForma.AlakJelzo, ElojelzoTipus.KetFogalmu)
            .AddVaganyValtoAllas(VaganySzerep.Elso, ValtoAllas.Egyenes)
            .AddVaganyValtoAllas(VaganySzerep.Masodik, ValtoAllas.Kitero)
            .AddVaganyHossz(VaganySzerep.Elso, 118)
            .AddVaganyHossz(VaganySzerep.Masodik, 120)
            .AddAllomasOldalAdat(Irany.KezdopontFele, 8139 - 7335, ValtoTajolas.Balos)
            .AddAllomasOldalAdat(Irany.VegpontFele, 10474 - 9328, ValtoTajolas.Jobbos)
            .AddSzelvenyszam(ElojelzoSzerep.AEj,    new(81, 39))
            .AddSzelvenyszam(BejaratiJelzoSzerep.A, new(85, 36))
            .AddSzelvenyszam(ValtoSzerep.Valto2,    new(86, 50))
            .AddSzelvenyszam(ValtoSzerep.Valto1,    new(88, 22))
            .AddSzelvenyszam(BejaratiJelzoSzerep.B, new(89, 20))
            .AddSzelvenyszam(ElojelzoSzerep.BEj,    new(93, 28))
            .Build();

    /// <summary>
    /// <code>
    ///                        Hűvösvölgy                              (H) A
    ///  ... - T1 - E1 - 1--V1 - B -   I.  K1--2 - E2 - A - AIsm - AEj
    ///                   \-V2        II.  K2-/
    /// </code>
    /// </summary>
    private static AllomasiTopologiaAdatok CreateHuvosvolgy()
        => new AllomasiTopologiaAdatokBuilder(AllomasNev.Huvosvolgy,
                JelzoForma.FenyJelzo, ElojelzoTipus.HaromFogalmu)
            .AddVaganyValtoAllas(VaganySzerep.Elso, ValtoAllas.Egyenes)
            .AddVaganyValtoAllas(VaganySzerep.Masodik, ValtoAllas.Kitero)
            .AddAllomasOldalAdat(Irany.KezdopontFele, 10474 - 9328, ValtoTajolas.Balos)
            .AddAllomasOldalAdat(Irany.VegpontFele, 50, ValtoTajolas.Jobbos)
            .AddSzelvenyszam(ElojelzoSzerep.AEj,        new(104, 74))
            .AddSzelvenyszam(IsmetlojelzoSzerep.AIsm,   new(107, 40))
            .AddSzelvenyszam(BejaratiJelzoSzerep.A,     new(108, 74))
            .AddSzelvenyszam(TolatasjelzoSzerep.E2,     new(110, 44))
            .AddSzelvenyszam(ValtoSzerep.Valto2,        new(110, 46))
            .AddSzelvenyszam(KijaratiJelzoSzerep.K1,    new(110, 80))
            .AddSzelvenyszam(KijaratiJelzoSzerep.K2,    new(110, 93))
            .AddSzelvenyszam(KijaratiJelzoSzerep.V1_B,  new(111, 86))
            .AddNevFeluliras(KijaratiJelzoSzerep.V1_B,  "B")
            .AddSzelvenyszam(KijaratiJelzoSzerep.V1,    new(112, 83))
            .AddSzelvenyszam(KijaratiJelzoSzerep.V2,    new(112, 74))
            .AddSzelvenyszam(ValtoSzerep.Valto1,        new(113, 20))
            // TODO 1-es váltó és/vagy E1 törpe szelvényszáma nem stimmel
            .AddSzelvenyszam(TolatasjelzoSzerep.E1,     new(113, 25))
            .AddSzelvenyszam(TolatasjelzoSzerep.T1,     new(113, 30))
            .Build();
}
