using Gyermekvasut.Modellek.BiztberNS;
using Gyermekvasut.Modellek.Palya;

namespace Gyermekvasut.ValtokezeloNS;

public class VaganyutElrendeles
{
    public VaganyutIrany Irany { get; }
    public string Vonatszam { get; }
    public Vagany Vagany { get; }
    public TimeOnly Ido { get; }

    public VaganyutElrendeles(VaganyutIrany irany,
        string vonatszam, Vagany vagany, TimeOnly ido)
    {
        Irany = irany;
        Vonatszam = vonatszam;
        Vagany = vagany;
        Ido = ido;
    }
}
