using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Palya.Jelzok;

namespace Gyermekvasut.Modellek.Topologia;

public class AllomasiTopologia
{
    public HashSet<Valto> Valtok { get; } = new();
    public HashSet<Jelzo> Jelzok { get; } = new();
    public HashSet<Vagany> Vaganyok { get; } = new();
    public Dictionary<Irany, Szakasz?> Allomaskozok { get; } = new();
    private LezarasiTablazat? _lezarasiTablazat;
    public LezarasiTablazat LezarasiTablazat
    {
        get => _lezarasiTablazat!;
        set => _lezarasiTablazat = value!;
    }

    internal AllomasiTopologia(Szakasz? kpAllomaskoz, Szakasz? vpAllomaskoz)
    {
        Allomaskozok[Irany.KezdopontFele] = kpAllomaskoz;
        Allomaskozok[Irany.VegpontFele] = vpAllomaskoz;
    }

    public Fojelzo GetBejaratiJelzo(Irany allomasvegIrany)
    {
        return Jelzok.OfType<Fojelzo>()
            // A KP állomásvég felöli jelző a VP fele néz
            .Single(fojelzo => fojelzo.Rendeltetes == FojelzoRendeltetes.Bejarati
                && fojelzo.Irany == allomasvegIrany.Fordit());
    }

    public Elojelzo GetElojelzo(Irany allomasvegIrany)
    {
        return Jelzok.OfType<Elojelzo>()
            // A KP állomásvég felöli jelző a VP fele néz
            .Single(elojelzo => elojelzo.Irany == allomasvegIrany.Fordit());
    }

    public Valto GetOnlyValto(Irany allomasvegIrany)
    {
        return Valtok
            .Single(valto => valto.CsucsIrany == allomasvegIrany);
    }

    public void EgyenesFeltolt(params PalyaElem[] elemek)
    {
        for (int i = 1; i < elemek.Length; i++)
        {
            PalyaElem elozo = elemek[i - 1];
            PalyaElem elem = elemek[i];
            elozo.Szomszedolas(Irany.VegpontFele, elem);
            elem.Szomszedolas(Irany.KezdopontFele, elozo);

            if (elem is Valto)
            {
                Valtok.Add((elem as Valto)!);
            }
            if (elem is Jelzo)
            {
                Jelzok.Add((elem as Jelzo)!);
            }
            if (elem is Vagany)
            {
                Vaganyok.Add((elem as Vagany)!);
            }
        }
    }

    public void ValtoKiteroSzomszedol(Valto valto, PalyaElem kiteroSzar)
    {
        Valtok.Add(valto);
        valto.KiteroSzomszedolas(kiteroSzar);
        kiteroSzar.Szomszedolas(valto.CsucsIrany, valto);
        // TODO Lezárási táblázathoz hozzáad: gyök felé elindul mindkét száron, ha vágányt talál, add
    }

    public Irany GetAllomaskozIrany(Szakasz allomaskoz)
        => Allomaskozok.Single(pair => pair.Value == allomaskoz).Key;
}
