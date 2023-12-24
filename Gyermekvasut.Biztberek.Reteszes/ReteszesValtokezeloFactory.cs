using Gyermekvasut.ValtokezeloNS;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Palya.Jelzok;
using Gyermekvasut.Modellek.Topologia;
using Gyermekvasut.Modellek.Ido;

namespace Gyermekvasut.Biztberek.Reteszes;

public class ReteszesValtokezeloFactory : ValtokezeloFactory<ReteszesValtokezelo>
{
    protected override double VaganyutBeallitasMinutes => 2;

    public ReteszesValtokezeloFactory(AllomasiTopologia topologia, ITimerFactory timerFactory)
        : base(topologia, timerFactory)
    { }

    protected override ReteszesValtokezelo Create(Valto valto, Fojelzo bejaratiJelzo,
        Szakasz kijaratOldoSzakasz, Szakasz kijaratOldoSzakaszElottiSzakasz,
        Vagany egyenesBejaratOldoSzakasz, Szakasz egyenesBejaratOldoSzakaszElottiSzakasz,
        Vagany kiteroBejaratOldoSzakasz, Szakasz kiteroBejaratOldoSzakaszElottiSzakasz, ITimer timer)
    {
        return new ReteszesValtokezelo(valto, Topologia.LezarasiTablazat, bejaratiJelzo,
            kijaratOldoSzakasz, kijaratOldoSzakaszElottiSzakasz,
            egyenesBejaratOldoSzakasz, egyenesBejaratOldoSzakaszElottiSzakasz,
            kiteroBejaratOldoSzakasz, kiteroBejaratOldoSzakaszElottiSzakasz, timer);
    }
}
