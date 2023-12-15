using Gyermekvasut.Biztberek.Emeltyus;
using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Biztberek.Reteszes;

public class ReteszesBiztberFactory
    : EmeltyusBiztberFactory<ReteszesBiztber, ReteszesEmeltyuCsoport, ReteszesValtokezelo>
{
    public ReteszesBiztberFactory(Allomas allomas)
        : base(allomas, new ReteszesEmeltyuCsoportFactory(allomas.Topologia, new(allomas.Topologia)))
    { }

    public override ReteszesBiztber Create()
        => new(Allomas, EmeltyuCsoportFactory);
}
