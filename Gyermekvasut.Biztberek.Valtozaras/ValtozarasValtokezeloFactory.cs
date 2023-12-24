using Gyermekvasut.ValtokezeloNS;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Palya.Jelzok;
using Gyermekvasut.Modellek.Topologia;
using Gyermekvasut.Modellek.Ido;

namespace Gyermekvasut.Biztberek.Valtozaras;

internal class ValtozarasValtokezeloFactory : ValtokezeloFactory<ValtozarasValtokezelo>
{
    protected override double VaganyutBeallitasMinutes => 3;

    public ValtozarasValtokezeloFactory(AllomasiTopologia topologia, ITimerFactory timerFactory)
        : base(topologia, timerFactory)
    { }

    protected override ValtozarasValtokezelo Create(Valto valto, Fojelzo bejaratiJelzo,
        Szakasz kijaratOldoSzakasz, Szakasz kijaratOldoSzakaszElottiSzakasz,
        Vagany egyenesBejaratOldoSzakasz, Szakasz egyenesBejaratOldoSzakaszElottiSzakasz,
        Vagany kiteroBejaratOldoSzakasz, Szakasz kiteroBejaratOldoSzakaszElottiSzakasz, ITimer timer)
    {
        return new ValtozarasValtokezelo(valto, Topologia.LezarasiTablazat, bejaratiJelzo,
            kijaratOldoSzakasz, kijaratOldoSzakaszElottiSzakasz,
            egyenesBejaratOldoSzakasz, egyenesBejaratOldoSzakaszElottiSzakasz,
            kiteroBejaratOldoSzakasz, kiteroBejaratOldoSzakaszElottiSzakasz, timer);
    }
}
