using Gyermekvasut.Biztberek.Emeltyus;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Palya.Jelzok;

namespace Gyermekvasut.Biztberek.Valtozaras;

public sealed class ValtozarasEmeltyuCsoport : EmeltyuCsoport<ValtozarasBiztber, ValtozarasValtokezelo>
{
    public ValtozarKulcsTarolo ValtozarKulcsTarolo { get; }

    public ValtozarasEmeltyuCsoport(Irany allomasvegIrany, Elojelzo elojelzo,
        Fojelzo bejaratiJelzo, Valto valto)
        : base(allomasvegIrany, elojelzo, bejaratiJelzo)
    {
        ValtoAllas? valtozarKulcs;
        if (valto.Lezaras == ValtoLezaras.Lezarva)
        {
            if (!valto.Vegallas.HasValue)
            {
                throw new ArgumentNullException($"A(z) {valto.Nev} váltónak nincs végállása, de le van zárva");
            }
            valtozarKulcs = valto.Vegallas.Value;
        }
        else
        {
            valtozarKulcs = null;
        }
        ValtozarKulcsTarolo = new(valto, valtozarKulcs);
    }

    public override void ValtokezeloHozzarendel(ValtozarasValtokezelo valtokezelo)
    {
        base.ValtokezeloHozzarendel(valtokezelo);
        valtokezelo.ValtozarKulcsTaroloHozzarendel(ValtozarKulcsTarolo);
    }
}
