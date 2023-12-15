using Gyermekvasut.ValtokezeloNS;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Palya.Jelzok;
using Gyermekvasut.Modellek.Topologia;

namespace Gyermekvasut.Biztberek.Reteszes;

public class ReteszesValtokezelo : Valtokezelo
{
    public ReteszesValtokezelo(Valto valto, LezarasiTablazat lezarasiTablazat, Fojelzo bejaratiJelzo,
        Szakasz kijaratOldoSzakasz, Szakasz kijaratOldoSzakaszElottiSzakasz,
        Szakasz egyenesBejaratOldoSzakasz, Szakasz egyenesBejaratOldoSzakaszElottiSzakasz,
        Szakasz kiteroBejaratOldoSzakasz, Szakasz kiteroBejaratOldoSzakaszElottiSzakasz)
        : base(valto, ValtoLezarasSzerep.Rendelkezo, lezarasiTablazat, bejaratiJelzo,
            kijaratOldoSzakasz, kijaratOldoSzakaszElottiSzakasz,
            egyenesBejaratOldoSzakasz, egyenesBejaratOldoSzakaszElottiSzakasz,
            kiteroBejaratOldoSzakasz, kiteroBejaratOldoSzakaszElottiSzakasz)
    { }

    protected override void VaganyutLezaras()
        => throw new InvalidOperationException("Reteszes váltó esetén a váltólezárás a rendelkező feladata.");

    protected override void VaganyutFeloldas()
        => throw new InvalidOperationException("Reteszes váltó esetén a váltófeloldás a rendelkező feladata.");
}
