using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.VonatNS;

namespace Gyermekvasut.Modellek.AllomasNS;

public abstract class Koruljarato
{
    protected System.Timers.Timer timer = new();
    public Allomas Allomas { get; }

    protected Koruljarato(Allomas allomas)
    {
        Allomas = allomas;
        foreach (var vagany in Allomas.Topologia.Vaganyok)
        {
            vagany.VonatErkezett += Vagany_VonatErkezett;
        }
    }

    private void Vagany_VonatErkezett(object? sender, VonatErkezettEventArgs e)
    {
        Vonat vonat = e.Vonat;
        Jarmu? gep = vonat.Feloszlik();
        if (vonat.VeglegFeloszlott)
        {
            KihuzKitol(vonat);
        }
        else if (gep != null)
        {
            Koruljar(vonat, gep);
        }
    }

    protected abstract void KihuzKitol(Vonat vonat);
    protected abstract void Koruljar(Vonat vonat, Jarmu gep);
    /*
     * gépből új szerelvény, max sebesség tolatás
     * léptet
     * - közben O-ban tolatóvgútra vár
     * - A-ban csak váltót állít
     * szerelvény levesz
     * gép visszacsatol eredeti vonatra
     */
}
