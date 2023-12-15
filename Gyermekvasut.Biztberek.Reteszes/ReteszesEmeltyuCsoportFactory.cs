using Gyermekvasut.Biztberek.Emeltyus;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.Topologia;

namespace Gyermekvasut.Biztberek.Reteszes;

public class ReteszesEmeltyuCsoportFactory
    : EmeltyuCsoportFactory<ReteszesBiztber, ReteszesEmeltyuCsoport, ReteszesValtokezelo>
{
    public ReteszesEmeltyuCsoportFactory(AllomasiTopologia topologia,
        ReteszesValtokezeloFactory valtokezeloFactory)
        : base(topologia, valtokezeloFactory) { }

    public override ReteszesEmeltyuCsoport Create(Irany allomasvegIrany)
    {
        ReteszesValtokezelo valtokezelo = ValtokezeloFactory.Create(allomasvegIrany);
        ReteszesEmeltyuCsoport emeltyuCsoport = new(allomasvegIrany,
            Topologia.GetElojelzo(allomasvegIrany),
            Topologia.GetBejaratiJelzo(allomasvegIrany),
            Topologia.GetOnlyValto(allomasvegIrany));
        emeltyuCsoport.ValtokezeloHozzarendel(valtokezelo);
        return emeltyuCsoport;
    }
}
