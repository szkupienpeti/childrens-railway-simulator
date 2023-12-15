using Gyermekvasut.Biztberek.Emeltyus;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Palya;

namespace Gyermekvasut.Biztberek.Valtozaras;

public class ValtozarasBiztber : EmeltyusBiztber<ValtozarasBiztber, ValtozarasEmeltyuCsoport, ValtozarasValtokezelo>
{
    public ValtozarasBiztber(Allomas allomas, EmeltyuCsoportFactory<ValtozarasBiztber, ValtozarasEmeltyuCsoport, ValtozarasValtokezelo> emeltyuCsoportFactory)
        : base(allomas, emeltyuCsoportFactory) { }

    protected override bool ValtoSzerkezetilegLezarva(ValtozarasEmeltyuCsoport emeltyuCsoport, ValtoAllas valtoAllas)
        => emeltyuCsoport.ValtozarKulcsTarolo.ValtozarKulcs == valtoAllas;
}
