using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Topologia;

namespace Gyermekvasut.Modellek.AllomasNS;

public class Allomas
{
    public AllomasNev AllomasNev { get; }
    public AllomasiTopologia Topologia { get; }

    public Allomas(AllomasNev nev)
    {
        AllomasNev = nev;
        Topologia = Topologiak.Felepit(nev);
    }    
}
