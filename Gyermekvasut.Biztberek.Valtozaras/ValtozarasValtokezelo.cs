using Gyermekvasut.ValtokezeloNS;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Palya.Jelzok;
using Gyermekvasut.Modellek.Topologia;
using Gyermekvasut.Modellek.Ido;

namespace Gyermekvasut.Biztberek.Valtozaras;

public class ValtozarasValtokezelo : Valtokezelo
{
    private ValtozarKulcsTarolo? _valtozarKulcsTarolo;
    public ValtozarKulcsTarolo ValtozarKulcsTarolo
    {
        get => _valtozarKulcsTarolo!;
        private set
        {
            if (_valtozarKulcsTarolo != null)
            {
                throw new InvalidOperationException("A váltózárkulcs-tároló már be lett állítva");
            }
            _valtozarKulcsTarolo = value;
        }
    }

    public ValtozarasValtokezelo(Valto valto, LezarasiTablazat lezarasiTablazat, Fojelzo bejaratiJelzo,
        Szakasz kijaratOldoSzakasz, Szakasz kijaratOldoSzakaszElottiSzakasz,
        Szakasz egyenesBejaratOldoSzakasz, Szakasz egyenesBejaratOldoSzakaszElottiSzakasz,
        Szakasz kiteroBejaratOldoSzakasz, Szakasz kiteroBejaratOldoSzakaszElottiSzakasz, ITimer timer)
        : base(valto, ValtoLezarasSzerep.Valtokezelo, lezarasiTablazat, bejaratiJelzo,
            kijaratOldoSzakasz, kijaratOldoSzakaszElottiSzakasz,
            egyenesBejaratOldoSzakasz, egyenesBejaratOldoSzakaszElottiSzakasz,
            kiteroBejaratOldoSzakasz, kiteroBejaratOldoSzakaszElottiSzakasz, timer)
    { }

    public void ValtozarKulcsTaroloHozzarendel(ValtozarKulcsTarolo valtozarKulcsTarolo)
    {
        ValtozarKulcsTarolo = valtozarKulcsTarolo;
    }

    protected override void VaganyutLezaras()
    {
        ValtozarKulcsTarolo.ValtozarKulcsBetesz(Valto.Vegallas!.Value);
    }

    protected override void VaganyutFeloldas()
    {
        ValtoAllas valtozarKulcs = ValtozarKulcsTarolo.ValtozarKulcsKivesz();
        if (valtozarKulcs != Valto.Vegallas)
        {
            throw new InvalidOperationException($"A kivett váltózárkulcs({valtozarKulcs}) nem egyezik meg a váltó végállásával ({Valto.Vegallas})");
        }
    }
}
