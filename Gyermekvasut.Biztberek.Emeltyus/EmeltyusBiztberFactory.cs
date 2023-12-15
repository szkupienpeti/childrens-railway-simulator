using Gyermekvasut.ValtokezeloNS;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.BiztberNS;

namespace Gyermekvasut.Biztberek.Emeltyus;

public abstract class EmeltyusBiztberFactory<TBiztber, TEmeltyuCsoport, TValtokezelo> : BiztberFactory<TBiztber>
    where TBiztber : EmeltyusBiztber<TBiztber, TEmeltyuCsoport, TValtokezelo>
    where TEmeltyuCsoport : EmeltyuCsoport<TBiztber, TValtokezelo>
    where TValtokezelo : Valtokezelo
{
    protected EmeltyuCsoportFactory<TBiztber, TEmeltyuCsoport, TValtokezelo> EmeltyuCsoportFactory { get; }

    protected EmeltyusBiztberFactory(Allomas allomas, EmeltyuCsoportFactory<TBiztber, TEmeltyuCsoport, TValtokezelo> emeltyuCsoportFactory)
        : base(allomas)
    {
        EmeltyuCsoportFactory = emeltyuCsoportFactory;
    }
}
