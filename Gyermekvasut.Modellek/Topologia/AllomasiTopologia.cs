using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Palya.Jelzok;

namespace Gyermekvasut.Modellek.Topologia;

public class AllomasiTopologia
{
    public HashSet<Valto> Valtok { get; } = new();
    public HashSet<Jelzo> Jelzok { get; } = new();
    public HashSet<Vagany> Vaganyok { get; } = new();
    public HashSet<Szakasz> Szakaszok { get; } = new();
    public Dictionary<Irany, Szakasz?> Allomaskozok { get; } = new();
    private LezarasiTablazat? _lezarasiTablazat;
    public LezarasiTablazat LezarasiTablazat
    {
        get => _lezarasiTablazat!;
        set => _lezarasiTablazat = value;
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

    public void EgyenesFeltolt(List<PalyaElem> elemek)
    {
        for (int i = 1; i < elemek.Count; i++)
        {
            PalyaElem elozo = elemek[i - 1];
            PalyaElem elem = elemek[i];
            elozo.Szomszedolas(Irany.VegpontFele, elem);
            elem.Szomszedolas(Irany.KezdopontFele, elozo);
            
            if (elem is Valto valto)
            {
                Valtok.Add(valto);
            }
            if (elem is Jelzo jelzo)
            {
                Jelzok.Add(jelzo);
            }
            if (elem is Vagany vagany)
            {
                Vaganyok.Add(vagany);
            }
            if (elem is Szakasz szakasz)
            {
                Szakaszok.Add(szakasz);
            }
        }
    }

    public void ValtoKiteroSzomszedol(Valto valto, PalyaElem kiteroSzar)
    {
        Valtok.Add(valto);
        valto.KiteroSzomszedolas(kiteroSzar);
        kiteroSzar.Szomszedolas(valto.CsucsIrany, valto);
    }

    public HashSet<Szakasz> GetHianyzoHosszuSzakaszok()
        =>Szakaszok.Where(sz => sz.HosszHianyzik).ToHashSet();

    public Irany GetAllomaskozIrany(Szakasz allomaskoz)
        => Allomaskozok.Single(pair => pair.Value == allomaskoz).Key;
}
