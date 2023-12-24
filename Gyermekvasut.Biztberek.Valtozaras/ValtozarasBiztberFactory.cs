using Gyermekvasut.Biztberek.Emeltyus;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Ido;

namespace Gyermekvasut.Biztberek.Valtozaras;

public class ValtozarasBiztberFactory
    : EmeltyusBiztberFactory<ValtozarasBiztber, ValtozarasEmeltyuCsoport, ValtozarasValtokezelo>
{
    public ValtozarasBiztberFactory(Allomas allomas) : this(allomas, new TimerWrapperFactory()) { }

    public ValtozarasBiztberFactory(Allomas allomas, ITimerFactory timerFactory) :
        base(allomas, new ValtozarasEmeltyuCsoportFactory(allomas.Topologia, new(allomas.Topologia, timerFactory)))
    { }

    public override ValtozarasBiztber Create()
        =>  new(Allomas, EmeltyuCsoportFactory);
}
