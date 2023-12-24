using Gyermekvasut.Biztberek.Emeltyus;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Ido;

namespace Gyermekvasut.Biztberek.Reteszes;

public class ReteszesBiztberFactory
    : EmeltyusBiztberFactory<ReteszesBiztber, ReteszesEmeltyuCsoport, ReteszesValtokezelo>
{
    public ReteszesBiztberFactory(Allomas allomas) : this(allomas, new TimerWrapperFactory()) { }

    public ReteszesBiztberFactory(Allomas allomas, ITimerFactory timerFactory)
        : base(allomas, new ReteszesEmeltyuCsoportFactory(allomas.Topologia, new(allomas.Topologia, timerFactory)))
    { }

    public override ReteszesBiztber Create()
        => new(Allomas, EmeltyuCsoportFactory);
}
