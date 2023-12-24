using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.Ido;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Palya.Jelzok;
using Gyermekvasut.Modellek.Topologia;

namespace Gyermekvasut.ValtokezeloNS;

public abstract class ValtokezeloFactory<TValtokezelo>
    where TValtokezelo : Valtokezelo
{
    protected AllomasiTopologia Topologia { get; }
    protected abstract double VaganyutBeallitasMinutes { get; }
    public ITimerFactory TimerFactory { get; }

    protected ValtokezeloFactory(AllomasiTopologia topologia, ITimerFactory timerFactory)
    {
        Topologia = topologia;
        TimerFactory = timerFactory;
    }

    public TValtokezelo Create(Irany allomasvegIrany)
    {
        Valto valto = Topologia.GetOnlyValto(allomasvegIrany);
        Fojelzo bejaratiJelzo = Topologia.GetBejaratiJelzo(allomasvegIrany);
        Szakasz allomaskoz = Topologia.Allomaskozok[allomasvegIrany]!;
        Szakasz allomaskozElottiSzakasz = allomaskoz.GetKovetkezoFeltetelesPalyaElem<Szakasz>(allomasvegIrany.Fordit())!;
        Vagany egyenesVagany = valto.GetGyokFeloliVagany(ValtoAllas.Egyenes);
        Szakasz egyenesValtoSzar = egyenesVagany.GetKovetkezoFeltetelesPalyaElem<Szakasz>(allomasvegIrany)!;
        Vagany kiteroVagany = valto.GetGyokFeloliVagany(ValtoAllas.Kitero);
        Szakasz kiteroValtoSzar = kiteroVagany.GetKovetkezoFeltetelesPalyaElem<Szakasz>(allomasvegIrany)!;
        ITimer timer = TimerFactory.Create(false, IdoUtil.MinutesToTimerInterval(VaganyutBeallitasMinutes));
        return Create(valto, bejaratiJelzo,
            allomaskoz, allomaskozElottiSzakasz,
            egyenesVagany, egyenesValtoSzar,
            kiteroVagany, kiteroValtoSzar, timer);
    }

    protected abstract TValtokezelo Create(Valto valto, Fojelzo bejaratiJelzo,
        Szakasz kijaratOldoSzakasz, Szakasz kijaratOldoSzakaszElottiSzakasz,
        Vagany egyenesBejaratOldoSzakasz, Szakasz egyenesBejaratOldoSzakaszElottiSzakasz,
        Vagany kiteroBejaratOldoSzakasz, Szakasz kiteroBejaratOldoSzakaszElottiSzakasz, ITimer timer);
}
