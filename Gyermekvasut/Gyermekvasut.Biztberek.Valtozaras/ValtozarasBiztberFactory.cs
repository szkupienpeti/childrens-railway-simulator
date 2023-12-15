using Gyermekvasut.Biztberek.Emeltyus;
using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Biztberek.Valtozaras;

public class ValtozarasBiztberFactory
    : EmeltyusBiztberFactory<ValtozarasBiztber, ValtozarasEmeltyuCsoport, ValtozarasValtokezelo>
{
    public ValtozarasBiztberFactory(Allomas allomas) :
        base(allomas, new ValtozarasEmeltyuCsoportFactory(allomas.Topologia, new(allomas.Topologia)))
    { }

    public override ValtozarasBiztber Create()
        =>  new(Allomas, EmeltyuCsoportFactory);
}
