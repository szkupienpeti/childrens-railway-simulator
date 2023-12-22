using Gyermekvasut.Modellek.Palya;

namespace Gyermekvasut.Modellek.Topologia;

public static class SzakaszHosszSzamolo
{
    public static void HianyzoHosszokatKiszamol(ICollection<Szakasz> szakaszok)
    {
        foreach (var szakasz in szakaszok)
        {
            int hossz;
            if (TipusosSzomszedokKozott<IHelyhezKotottPalyaElem, IHelyhezKotottPalyaElem>(szakasz))
            {
                int kpSzelvenyMeter = GetSzomszedSzelvenyOsszMeter(szakasz, Irany.KezdopontFele);
                int vpSzelvenyMeter = GetSzomszedSzelvenyOsszMeter(szakasz, Irany.VegpontFele);
                hossz = vpSzelvenyMeter - kpSzelvenyMeter;
            }
            else if (HelyhezKotottEsVaganyKozott(szakasz))
            {
                Irany helyhezKotottSzomszedIrany = GetOnlyHelyhezKotottSzomszedIrany(szakasz);
                int szomszedSzelvenyMeter = GetSzomszedSzelvenyOsszMeter(szakasz, helyhezKotottSzomszedIrany);
                Irany vaganyIrany = helyhezKotottSzomszedIrany.Fordit();
                Vagany vagany = GetTipusosSzomszed<Vagany>(szakasz, vaganyIrany)!;
                int vaganyHossz = vagany.Hossz;
                int vaganyonTuliSzelvenyMeter = GetVaganyonTuliSzelvenyOsszMeter(szakasz, vaganyIrany);
                int osszTavolsag = Math.Abs(vaganyonTuliSzelvenyMeter - szomszedSzelvenyMeter);
                int hianyzoTavolsagokOssz = osszTavolsag - vaganyHossz;
                hossz = Convert.ToInt32((double)hianyzoTavolsagokOssz / 2);
            }
            else
            {
                throw new InvalidOperationException($"A {szakasz} szakasz hiányzó hossza nem számolható ki");
            }
            szakasz.SetHossz(hossz);
        }
    }

    private static bool HelyhezKotottEsVaganyKozott(Szakasz szakasz)
        => TipusosSzomszedokKozott<IHelyhezKotottPalyaElem, Vagany>(szakasz)
            || TipusosSzomszedokKozott<Vagany, IHelyhezKotottPalyaElem>(szakasz);

    private static bool TipusosSzomszedokKozott<TKp, TVp>(Szakasz szakasz)
            where TKp : class
            where TVp : class
        => GetTipusosSzomszed<TKp>(szakasz, Irany.KezdopontFele) != null
            && GetTipusosSzomszed<TVp>(szakasz, Irany.VegpontFele) != null;

    private static T? GetTipusosSzomszed<T>(Szakasz szakasz, Irany irany)
        where T : class
    {
        PalyaElem szomszed = szakasz.Kovetkezo(irany)!;
        return szomszed as T;
    }

    private static Irany GetOnlyHelyhezKotottSzomszedIrany(Szakasz szakasz)
        =>  Enum.GetValues<Irany>()
            .Single(irany => GetTipusosSzomszed<IHelyhezKotottPalyaElem>(szakasz, irany) != null);

    private static int GetSzomszedSzelvenyOsszMeter(Szakasz szakasz, Irany irany)
    {
        PalyaElem szomszed = szakasz.Kovetkezo(irany)!;
        if (szomszed is IHelyhezKotottPalyaElem helyhezKotottPalyaElem)
        {
            return helyhezKotottPalyaElem.Szelvenyszam.GetOsszMeter();
        }
        else
        {
            throw new ArgumentException($"A szakasz szomszédja ({szomszed}) nem helyhez kötött pályaelem");
        }
    }

    private static int GetVaganyonTuliSzelvenyOsszMeter(Szakasz szakasz, Irany irany)
    {
        Vagany vagany = GetTipusosSzomszed<Vagany>(szakasz, irany)!;
        Szakasz tavoliSzakasz = GetTipusosSzomszed<Szakasz>(vagany, irany)!;
        return GetSzomszedSzelvenyOsszMeter(tavoliSzakasz, irany);
    }
}
