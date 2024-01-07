using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Palya.Jelzok;
using Gyermekvasut.Modellek.Topologia;
using Gyermekvasut.Modellek.Ido;
using Gyermekvasut.Valtokezelo;

namespace Gyermekvasut.Biztberek.Reteszes;

public class ReteszesValtokezelo : Valtokezelo
{
    public ReteszesValtokezelo(Valto valto, LezarasiTablazat lezarasiTablazat, Fojelzo bejaratiJelzo,
        Szakasz kijaratOldoSzakasz, Szakasz kijaratOldoSzakaszElottiSzakasz,
        Szakasz egyenesBejaratOldoSzakasz, Szakasz egyenesBejaratOldoSzakaszElottiSzakasz,
        Szakasz kiteroBejaratOldoSzakasz, Szakasz kiteroBejaratOldoSzakaszElottiSzakasz, ITimer timer)
        : base(valto, ValtoLezarasSzerep.Rendelkezo, lezarasiTablazat, bejaratiJelzo,
            kijaratOldoSzakasz, kijaratOldoSzakaszElottiSzakasz,
            egyenesBejaratOldoSzakasz, egyenesBejaratOldoSzakaszElottiSzakasz,
            kiteroBejaratOldoSzakasz, kiteroBejaratOldoSzakaszElottiSzakasz, timer)
    { }

    protected override void VaganyutLezaras()
        => throw new InvalidOperationException("Reteszes váltó esetén a váltólezárás a rendelkező feladata.");

    protected override void VaganyutFeloldas()
        => throw new InvalidOperationException("Reteszes váltó esetén a váltófeloldás a rendelkező feladata.");
}
