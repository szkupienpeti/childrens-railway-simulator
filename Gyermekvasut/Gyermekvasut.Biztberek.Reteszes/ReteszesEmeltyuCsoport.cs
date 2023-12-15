using Gyermekvasut.Biztberek.Emeltyus;
using Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Palya.Jelzok;

namespace Gyermekvasut.Biztberek.Reteszes;

public sealed class ReteszesEmeltyuCsoport : EmeltyuCsoport<ReteszesBiztber, ReteszesValtokezelo>
{
    public ReteszEmeltyu<ReteszesBiztber> ReteszEmeltyu { get; }

    public ReteszesEmeltyuCsoport(Irany allomasvegIrany, Elojelzo elojelzo,
        Fojelzo bejaratiJelzo, Valto valto)
        : base(allomasvegIrany, elojelzo, bejaratiJelzo)
    {
        ReteszEmeltyu = new(valto);
    }

    public override bool Tartalmaz(Emeltyu<ReteszesBiztber> emeltyu)
        => emeltyu == ReteszEmeltyu || base.Tartalmaz(emeltyu);
}
