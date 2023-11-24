using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Palya.Jelzok;

namespace Gyermekvasut.Modellek.Topologia;

public class AllomasiTopologia
{
    public HashSet<Valto> Valtok { get; } = new();
    public HashSet<Jelzo> Jelzok { get; } = new();
    public HashSet<Vagany> Vaganyok { get; } = new();
    public Szakasz KpAllomaskoz { get; }
    public Szakasz VpAllomaskoz { get; }

    internal AllomasiTopologia(Szakasz kpAllomaskoz, Szakasz vpAllomaskoz)
    {
        KpAllomaskoz = kpAllomaskoz;
        VpAllomaskoz = vpAllomaskoz;
    }
    public void EgyenesFeltolt(params PalyaElem[] elemek)
    {
        for (int i = 1; i < elemek.Length; i++)
        {
            PalyaElem elozo = elemek[i - 1];
            PalyaElem elem = elemek[i];
            elozo.VpSzomszedolas(elem);
            elem.KpSzomszedolas(elozo);

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
        switch (valto.CsucsFelolIrany)
        {
            case VonatNS.Irany.Paros:
                kiteroSzar.KpSzomszedolas(valto);
                break;
            case VonatNS.Irany.Paratlan:
                kiteroSzar.VpSzomszedolas(valto);
                break;
        }
    }
}
