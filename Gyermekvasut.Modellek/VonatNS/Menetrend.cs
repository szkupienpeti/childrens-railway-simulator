using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Modellek.VonatNS;

public class Menetrend
{
    public string Vonatszam { get; }
    public VonatIrany Irany { get; }
    public bool Koruljar { get; }
    public List<AllomasiMenetrendSor> Sorok { get; } = new();

    public Menetrend(string vonatszam, VonatIrany irany, 
        bool koruljar, params AllomasiMenetrendSor[] sorok)
    {
        Vonatszam = vonatszam;
        Irany = irany;
        Koruljar = koruljar;
        foreach (var sor in sorok)
        {
            Sorok.Add(sor);
        }
    }

    public override bool Equals(object? obj)
        => obj is Menetrend menetrend
            && Vonatszam == menetrend.Vonatszam
            && Irany == menetrend.Irany
            && Koruljar == menetrend.Koruljar
            && Enumerable.SequenceEqual(Sorok, menetrend.Sorok);

    public override int GetHashCode()
        => HashCode.Combine(Vonatszam, Irany, Koruljar, Sorok);
}

public class AllomasiMenetrendSor
{
    public AllomasNev Allomas { get; }
    public TimeOnly? Erkezes { get; }
    public TimeOnly Indulas { get; }
    public bool Athalad => Erkezes == null;

    public AllomasiMenetrendSor(AllomasNev allomas, TimeOnly? erkezes, TimeOnly indulas)
    {
        Allomas = allomas;
        Erkezes = erkezes;
        Indulas = indulas;
    }

    public override bool Equals(object? obj)
        => obj is AllomasiMenetrendSor sor
            && Allomas == sor.Allomas
            && EqualityComparer<TimeOnly?>.Default.Equals(Erkezes, sor.Erkezes)
            && Indulas.Equals(sor.Indulas) && Athalad == sor.Athalad;

    public override int GetHashCode()
        => HashCode.Combine(Allomas, Erkezes, Indulas, Athalad);
}
