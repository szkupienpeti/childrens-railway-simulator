using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.Topologia;
using Gyermekvasut.ValtokezeloNS;

namespace Gyermekvasut.Biztberek.Emeltyus;

public abstract class EmeltyuCsoportFactory<TAllitasiKiserletVisitor, TEmeltyuCsoport, TValtokezelo>
    where TAllitasiKiserletVisitor : IEmeltyusAllitasiKiserletVisitor<TAllitasiKiserletVisitor>
    where TEmeltyuCsoport : EmeltyuCsoport<TAllitasiKiserletVisitor, TValtokezelo>
    where TValtokezelo : Valtokezelo
{
    protected AllomasiTopologia Topologia { get; }
    protected ValtokezeloFactory<TValtokezelo> ValtokezeloFactory { get; }

    protected EmeltyuCsoportFactory(AllomasiTopologia topologia,
        ValtokezeloFactory<TValtokezelo> valtokezeloFactory)
    {
        Topologia = topologia;
        ValtokezeloFactory = valtokezeloFactory;
    }

    public abstract TEmeltyuCsoport Create(Irany allomasvegIrany);
}
