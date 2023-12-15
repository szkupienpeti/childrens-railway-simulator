using Gyermekvasut.ValtokezeloNS;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Palya.Jelzok;
using Gyermekvasut.Modellek.Topologia;

namespace Gyermekvasut.Biztberek.Reteszes;

public class ReteszesValtokezeloFactory : ValtokezeloFactory<ReteszesValtokezelo>
{
    public ReteszesValtokezeloFactory(AllomasiTopologia topologia) : base(topologia) { }

    protected override ReteszesValtokezelo Create(Valto valto, Fojelzo bejaratiJelzo,
        Szakasz kijaratOldoSzakasz, Szakasz kijaratOldoSzakaszElottiSzakasz,
        Vagany egyenesBejaratOldoSzakasz, Szakasz egyenesBejaratOldoSzakaszElottiSzakasz,
        Vagany kiteroBejaratOldoSzakasz, Szakasz kiteroBejaratOldoSzakaszElottiSzakasz)
    {
        return new ReteszesValtokezelo(valto, Topologia.LezarasiTablazat, bejaratiJelzo,
            kijaratOldoSzakasz, kijaratOldoSzakaszElottiSzakasz,
            egyenesBejaratOldoSzakasz, egyenesBejaratOldoSzakaszElottiSzakasz,
            kiteroBejaratOldoSzakasz, kiteroBejaratOldoSzakaszElottiSzakasz);
    }
}
