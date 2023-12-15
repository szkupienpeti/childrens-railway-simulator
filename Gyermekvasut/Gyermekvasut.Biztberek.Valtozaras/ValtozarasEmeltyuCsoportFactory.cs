using Gyermekvasut.Biztberek.Emeltyus;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.Topologia;

namespace Gyermekvasut.Biztberek.Valtozaras;

public class ValtozarasEmeltyuCsoportFactory
    : EmeltyuCsoportFactory<ValtozarasBiztber, ValtozarasEmeltyuCsoport, ValtozarasValtokezelo>
{
    public ValtozarasEmeltyuCsoportFactory(AllomasiTopologia topologia,
        ValtozarasValtokezeloFactory valtokezeloFactory)
        : base(topologia, valtokezeloFactory) { }

    public override ValtozarasEmeltyuCsoport Create(Irany allomasvegIrany)
    {
        ValtozarasValtokezelo valtokezelo = ValtokezeloFactory.Create(allomasvegIrany);
        ValtozarasEmeltyuCsoport emeltyuCsoport = new(allomasvegIrany,
            Topologia.GetElojelzo(allomasvegIrany),
            Topologia.GetBejaratiJelzo(allomasvegIrany),
            Topologia.GetOnlyValto(allomasvegIrany));
        emeltyuCsoport.ValtokezeloHozzarendel(valtokezelo);
        return emeltyuCsoport;
    }
}
