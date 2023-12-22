using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Palya.Jelzok;
using Gyermekvasut.Modellek.Topologia.ObjektumSzerepek;

namespace Gyermekvasut.Modellek.Topologia;

public static class AllomasiTopologiaFactory
{
    public static AllomasiTopologia Create(AllomasNev allomasNev)
    {
        AllomasiTopologiaAdatok topologiaAdatok = AllomasiTopologiaAdatokFactory.Create(allomasNev);
        AllomasiTopologia topologia = KetvaganyosAllomasFelepit(topologiaAdatok);
        // TODO Hűvösvölgy kivételek kezelése: B jelző, tolatásjelzők
        LezarasiTablazatFactory lezarasiTablazatFactory = new(topologia);
        topologia.LezarasiTablazat = lezarasiTablazatFactory.Create();
        return topologia;
    }

    private static AllomasiTopologia KetvaganyosAllomasFelepit(AllomasiTopologiaAdatok topologiaAdatok)
    {
        List<PalyaElem> atmenoElemek = new();
        List<PalyaElem> kiteroElemek = new();

        AllomasVegTopologiaElemek kpElemek = AllomasVegFelepit(topologiaAdatok, Irany.KezdopontFele, atmenoElemek, kiteroElemek);
        VaganyokFelepit(topologiaAdatok.AltalanosAllomasAdat, atmenoElemek, kiteroElemek);
        AllomasVegTopologiaElemek vpElemek = AllomasVegFelepit(topologiaAdatok, Irany.VegpontFele, atmenoElemek, kiteroElemek);

        AllomasiTopologia topologia = new(kpElemek.SzelsoSzakasz, vpElemek.SzelsoSzakasz);
        topologia.EgyenesFeltolt(atmenoElemek);
        topologia.EgyenesFeltolt(kiteroElemek);
        ValtoKiteroSzomszedol(topologia, kpElemek);
        ValtoKiteroSzomszedol(topologia, vpElemek);
        SzakaszHosszSzamolo.HianyzoHosszokatKiszamol(topologia.GetHianyzoHosszuSzakaszok());
        return topologia;
    }

    private static AllomasVegTopologiaElemek AllomasVegFelepit(AllomasiTopologiaAdatok topologiaAdatok, Irany allomasOldal,
        List<PalyaElem> allomasAtmenoElemek, List<PalyaElem> allomasKiteroElemek)
    {
        AltalanosAllomasiTopologiaAdat altalanosAllomasAdatok = topologiaAdatok.AltalanosAllomasAdat;
        AllomasOldalTopologiaAdat allomasOldalAdatok = topologiaAdatok.AllomasOldalAdatok[allomasOldal];
        List<PalyaElem> oldalAtmenoElemek = new();
        List<PalyaElem> oldalKiteroElemek = new();
        // TODO Kezelni a vágánytengelyugrást Széchenyihegyen
        Szakasz? allomaskoz = BejaratFelepit(altalanosAllomasAdatok, allomasOldalAdatok, oldalAtmenoElemek);
        ValtoTopologiaiElemek valtoElemek = ValtoFelepit(altalanosAllomasAdatok, allomasOldalAdatok, oldalAtmenoElemek, oldalKiteroElemek);
        KijaratFelepit(altalanosAllomasAdatok, allomasOldalAdatok.AllomasOldal, oldalAtmenoElemek, oldalKiteroElemek);

        if (allomasOldal == Irany.VegpontFele)
        {
            oldalAtmenoElemek.Reverse();
            oldalKiteroElemek.Reverse();
        }
        allomasAtmenoElemek.AddRange(oldalAtmenoElemek);
        allomasKiteroElemek.AddRange(oldalKiteroElemek);
        Szakasz szelsoSzakasz = allomaskoz ?? valtoElemek.CsucsSzar;
        return new(szelsoSzakasz, valtoElemek.Valto, valtoElemek.KiteroSzar);
    }

    private static Szakasz? BejaratFelepit(AltalanosAllomasiTopologiaAdat altalanosAllomasAdatok,
        AllomasOldalTopologiaAdat allomasOldalAdatok, List<PalyaElem> atmenoElemek)
    {
        if (!allomasOldalAdatok.SzomszedAllomasNev.HasValue)
        {
            return null;
        }
        Irany allomasOldal = allomasOldalAdatok.AllomasOldal;
        BejaratiJelzoSzerep bejaratiJelzoSzerep = BejaratiJelzoSzerep.GetByOldal(allomasOldal);
        Szakasz allomaskoz = CreateAllomaskoz(allomasOldalAdatok);
        Elojelzo ej = CreateElojelzo(altalanosAllomasAdatok, bejaratiJelzoSzerep);
        Fojelzo bej = CreateBejaratiJelzo(altalanosAllomasAdatok, bejaratiJelzoSzerep);
        atmenoElemek.Add(allomaskoz);
        atmenoElemek.Add(ej);
        Ismetlojelzo? ism = CreateIsmetlojelzo(altalanosAllomasAdatok, bejaratiJelzoSzerep);
        if (ism != null)
        {
            Szakasz ej_ism = CreateSzakasz(ej, ism!, allomasOldal);
            Szakasz ism_bej = CreateSzakasz(ism!, bej, allomasOldal);
            atmenoElemek.Add(ej_ism);
            atmenoElemek.Add(ism!);
            atmenoElemek.Add(ism_bej);
        }
        else
        {
            Szakasz ej_bej = CreateSzakasz(ej, bej, allomasOldal);
            atmenoElemek.Add(ej_bej);
        }
        atmenoElemek.Add(bej);
        return allomaskoz;
    }

    private static ValtoTopologiaiElemek ValtoFelepit(AltalanosAllomasiTopologiaAdat altalanosAllomasAdatok,
        AllomasOldalTopologiaAdat allomasOldalAdatok, List<PalyaElem> atmenoElemek, List<PalyaElem> kiteroElemek)
    {
        Valto valto = CreateValto(altalanosAllomasAdatok, allomasOldalAdatok);
        Szakasz csucsSzar = CreateSzakasz($"{valto.Nev} csúcs");
        Szakasz egyenesSzar = CreateSzakasz($"{valto.Nev} E");
        Szakasz kiteroSzar = CreateSzakasz($"{valto.Nev} K");
        atmenoElemek.Add(csucsSzar);
        atmenoElemek.Add(valto);
        atmenoElemek.Add(egyenesSzar);
        kiteroElemek.Add(kiteroSzar);
        return new(valto, csucsSzar, kiteroSzar);
    }

    private static void KijaratFelepit(AltalanosAllomasiTopologiaAdat altalanosAllomasAdatok,
        Irany allomasOldal, List<PalyaElem> atmenoElemek, List<PalyaElem> kiteroElemek)
    {
        foreach (var vagany in altalanosAllomasAdatok.VaganyValtoAllasok.Keys)
        {
            Fojelzo? kij = CreateKijaratiJelzo(altalanosAllomasAdatok, vagany, allomasOldal);
            if (kij != null)
            {
                switch (altalanosAllomasAdatok.VaganyValtoAllasok[vagany])
                {
                    case ValtoAllas.Egyenes:
                        atmenoElemek.Add(kij!);
                        break;
                    case ValtoAllas.Kitero:
                        kiteroElemek.Add(kij!);
                        break;
                }
            }
        }
    }

    private static void VaganyokFelepit(AltalanosAllomasiTopologiaAdat altalanosAllomasAdatok,
        List<PalyaElem> atmenoElemek, List<PalyaElem> kiteroElemek)
    {
        // TODO Vagany iterálások közös fgvbe (Kijarat, Vagany felépítés)
        foreach (var topologiaiVagany in altalanosAllomasAdatok.VaganyValtoAllasok.Keys)
        {
            Vagany vagany = CreateVagany(altalanosAllomasAdatok, topologiaiVagany);
            switch (altalanosAllomasAdatok.VaganyValtoAllasok[topologiaiVagany])
            {
                case ValtoAllas.Egyenes:
                    atmenoElemek.Add(vagany);
                    break;
                case ValtoAllas.Kitero:
                    kiteroElemek.Add(vagany);
                    break;
            }
        }
    }

    private static void ValtoKiteroSzomszedol(AllomasiTopologia topologia, AllomasVegTopologiaElemek allomasVegElemek)
    {
        topologia.ValtoKiteroSzomszedol(allomasVegElemek.Valto, allomasVegElemek.ValtoKiteroSzar);
    }

    private static Szakasz CreateAllomaskoz(AllomasOldalTopologiaAdat allomasOldalAdat)
    {
        AllomasNev allomasNev = allomasOldalAdat.AllomasNev;
        Irany allomasOldal = allomasOldalAdat.AllomasOldal;
        AllomasNev szomszedAllomasNev = allomasOldalAdat.SzomszedAllomasNev!.Value;
        string nev = GetAllomaskozNev(szomszedAllomasNev, allomasNev, allomasOldal);
        return new(nev, allomasOldalAdat.AllomaskozHossz);
    }

    private static Elojelzo CreateElojelzo(AltalanosAllomasiTopologiaAdat altalanosAdatok, BejaratiJelzoSzerep bejaratiJelzoSzerep)
    {
        ElojelzoSzerep elojelzoSzerep = bejaratiJelzoSzerep.Elojelzo!;
        string nev = GetNev(elojelzoSzerep, altalanosAdatok);
        return new(nev, bejaratiJelzoSzerep.JelzoIrany, altalanosAdatok.JelzoForma, altalanosAdatok.ElojelzoTipus,
            altalanosAdatok.Szelvenyszamok[elojelzoSzerep]);
    }

    private static Ismetlojelzo? CreateIsmetlojelzo(AltalanosAllomasiTopologiaAdat altalanosAdatok, FojelzoSzerep fojelzoSzerep)
    {
        IsmetlojelzoSzerep ismetlojelzoSzerep = fojelzoSzerep.Ismetlojelzo!;
        if (altalanosAdatok.Szelvenyszamok.ContainsKey(ismetlojelzoSzerep))
        {
            string nev = GetNev(ismetlojelzoSzerep, altalanosAdatok);
            return new(nev, fojelzoSzerep.JelzoIrany, altalanosAdatok.Szelvenyszamok[ismetlojelzoSzerep]);
        }
        else
        {
            return null;
        }
    }

    private static Fojelzo CreateBejaratiJelzo(AltalanosAllomasiTopologiaAdat altalanosAdatok, BejaratiJelzoSzerep bejaratiJelzoSzerep)
    {
        string nev = GetNev(bejaratiJelzoSzerep, altalanosAdatok);
        return new(nev, bejaratiJelzoSzerep.JelzoIrany, altalanosAdatok.JelzoForma,
            FojelzoRendeltetes.Bejarati, altalanosAdatok.Szelvenyszamok[bejaratiJelzoSzerep]);
    }

    private static Valto CreateValto(AltalanosAllomasiTopologiaAdat altalanosAdatok, AllomasOldalTopologiaAdat allomasOldalAdatok)
    {
        Irany allomasOldal = allomasOldalAdatok.AllomasOldal;
        ValtoSzerep valtoSzerep = ValtoSzerep.GetByOldal(allomasOldal);
        string nev = GetNev(valtoSzerep, altalanosAdatok);
        return new(nev, allomasOldal, allomasOldalAdatok.ValtoTajolas,
            altalanosAdatok.ValtoAllitasIdo, altalanosAdatok.Szelvenyszamok[valtoSzerep]);
    }

    private static Fojelzo? CreateKijaratiJelzo(AltalanosAllomasiTopologiaAdat altalanosAdatok,
        VaganySzerep vaganySzerep, Irany allomasOldal)
    {
        KijaratiJelzoSzerep kijaratiJelzoSzerep = KijaratiJelzoSzerep.GetByVaganyOldal(vaganySzerep, allomasOldal);
        if (altalanosAdatok.Szelvenyszamok.ContainsKey(kijaratiJelzoSzerep))
        {
            string nev = GetNev(kijaratiJelzoSzerep, altalanosAdatok);
            return new(nev, allomasOldal, altalanosAdatok.JelzoForma,
            FojelzoRendeltetes.Kijarati, altalanosAdatok.Szelvenyszamok[kijaratiJelzoSzerep]);
        }
        else
        {
            return null;
        }
    }

    private static Vagany CreateVagany(AltalanosAllomasiTopologiaAdat altalanosAdatok,
        VaganySzerep vaganySzerep)
    {
        string nev = GetNev(vaganySzerep, altalanosAdatok);
        if (altalanosAdatok.VaganyHosszok.ContainsKey(vaganySzerep))
        {
            return new(nev, altalanosAdatok.AllomasNev, altalanosAdatok.VaganyHosszok[vaganySzerep]);
        }
        else
        {
            return new(nev, altalanosAdatok.AllomasNev);
        }
    }

    private static Szakasz CreateSzakasz(IHelyhezKotottPalyaElem a, IHelyhezKotottPalyaElem b, Irany allomasOldal)
        => new(GetSzakaszNev(a.Nev, b.Nev, allomasOldal));

    private static Szakasz CreateSzakasz(string nev)
        => new(nev);

    private static string GetAllomaskozNev(AllomasNev kpBal, AllomasNev kpJobb, Irany allomasOldal)
        => $"{GetSzakaszNev(kpBal.Kod(), kpJobb.Kod(), allomasOldal)} állomásköz";

    private static string GetSzakaszNev(string kpBal, string kpJobb, Irany allomasOldal)
        => allomasOldal == Irany.KezdopontFele
        ? GetKoztesNev(kpBal, kpJobb)
        : GetKoztesNev(kpJobb, kpBal);

    private static string GetKoztesNev(string kpElem, string vpElem)
        => $"{kpElem}-{vpElem}";

    private static string GetNev(TopologiaiObjektumSzerep szerep, AltalanosAllomasiTopologiaAdat altalanosAdatok)
        => altalanosAdatok.NevFelulirasok.GetValueOrDefault(szerep, szerep.Nev);
}

record AllomasVegTopologiaElemek(Szakasz SzelsoSzakasz, Valto Valto, Szakasz ValtoKiteroSzar);

record ValtoTopologiaiElemek(Valto Valto, Szakasz CsucsSzar, Szakasz KiteroSzar);
