using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Modellek.VonatNS;

public class Menetrend
{
    public string Vonatszam { get; }
    public VonatIrany Irany { get; }
    public List<AllomasiMenetrendSor> Sorok { get; } = new();

    public Menetrend(string vonatszam, VonatIrany irany, params AllomasiMenetrendSor[] sorok)
    {
        Vonatszam = vonatszam;
        Irany = irany;
        foreach (var sor in sorok)
        {
            Sorok.Add(sor);
        }
    }
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
}
