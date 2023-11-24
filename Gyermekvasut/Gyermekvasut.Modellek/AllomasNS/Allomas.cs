using Gyermekvasut.Modellek.Topologia;

namespace Gyermekvasut.Modellek.AllomasNS;

public class Allomas
{
    public AllomasNev Nev { get; }
    public AllomasiTopologia Topologia { get; }

    public Allomas(AllomasNev nev)
    {
        Nev = nev;
        Topologia = Topologiak.Felepit(nev);
    }
}
