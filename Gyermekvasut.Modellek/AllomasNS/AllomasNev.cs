using Gyermekvasut.Modellek.Topologia;
using System.Collections.Immutable;

namespace Gyermekvasut.Modellek.AllomasNS;

public enum AllomasNev
{
    Szechenyihegy = 1,
    Csilleberc = 2,
    Viragvolgy = 3,
    Janoshegy = 4,
    Szepjuhaszne = 5,
    Harshegy = 6,
    Huvosvolgy = 7
}

public static class AllomasNevExtensions
{
    public static readonly ImmutableList<AllomasNev> VONAL = ImmutableList.Create
    (
        AllomasNev.Szechenyihegy,
        AllomasNev.Csilleberc,
        AllomasNev.Viragvolgy,
        AllomasNev.Janoshegy,
        AllomasNev.Szepjuhaszne,
        AllomasNev.Harshegy,
        AllomasNev.Huvosvolgy
    );

    public static string Nev(this AllomasNev allomasNev)
        => allomasNev switch
        {
            AllomasNev.Szechenyihegy => "Széchenyihegy",
            AllomasNev.Csilleberc => "Csillebérc",
            AllomasNev.Viragvolgy => "Virágvölgy",
            AllomasNev.Janoshegy => "Jánoshegy",
            AllomasNev.Szepjuhaszne => "Szépjuhászné",
            AllomasNev.Harshegy => "Hárshegy",
            AllomasNev.Huvosvolgy => "Hűvösvölgy",
            _ => throw new NotImplementedException()
        };

    public static string Kod(this AllomasNev allomasNev)
        => allomasNev switch
        {
            AllomasNev.Szechenyihegy => "A",
            AllomasNev.Csilleberc => "U",
            AllomasNev.Viragvolgy => "L",
            AllomasNev.Janoshegy => "I",
            AllomasNev.Szepjuhaszne => "S",
            AllomasNev.Harshegy => "H",
            AllomasNev.Huvosvolgy => "O",
            _ => throw new NotImplementedException()
        };

    public static AllomasNev? Szomszed(this AllomasNev allomasNev, Irany irany)
        => irany switch
        {
            Irany.KezdopontFele => allomasNev.KpSzomszed(),
            Irany.VegpontFele => allomasNev.VpSzomszed(),
            _ => throw new NotImplementedException()
        };

    public static AllomasNev? KpSzomszed(this AllomasNev allomasNev)
    {
        if (allomasNev == AllomasNev.Szechenyihegy)
        {
            return null;
        }
        int index = VONAL.IndexOf(allomasNev);
        return VONAL[index - 1];
    }

    public static AllomasNev? VpSzomszed(this AllomasNev allomasNev)
    {
        if (allomasNev == AllomasNev.Huvosvolgy)
        {
            return null;
        }
        int index = VONAL.IndexOf(allomasNev);
        return VONAL[index + 1];
    }

    public static IEnumerable<Irany> SzomszedIranyok(this AllomasNev allomasNev)
    {
        foreach (var irany in Enum.GetValues<Irany>())
        {
            if (allomasNev.Szomszed(irany).HasValue)
            {
                yield return irany;
            }
        }
    }

    public static Irany? GetSzomszedIrany(this AllomasNev allomasNev, AllomasNev szomszed)
    {
        foreach (var irany in Enum.GetValues<Irany>())
        {
            if (allomasNev.Szomszed(irany) == szomszed)
            {
                return irany;
            }
        }
        return null;
    }

    public static bool Vegallomas(this AllomasNev allomasNev)
        => allomasNev is AllomasNev.Szechenyihegy or AllomasNev.Huvosvolgy;
}
