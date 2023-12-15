using Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.Palya.Jelzok;
using Gyermekvasut.ValtokezeloNS;

namespace Gyermekvasut.Biztberek.Emeltyus;

public abstract class EmeltyuCsoport<TAllitasiKiserletVisitor, TValtokezelo>
    where TAllitasiKiserletVisitor : EmeltyusAllitasiKiserletVisitor<TAllitasiKiserletVisitor>
    where TValtokezelo : Valtokezelo
{
    public Irany AllomasvegIrany { get; }
    public KetfogalmuElojelzoEmeltyu<TAllitasiKiserletVisitor> ElojelzoEmeltyu { get; }
    public BejaratiJelzoEmeltyu1<TAllitasiKiserletVisitor> BejaratiJelzoEmeltyu1 { get; }
    public BejaratiJelzoEmeltyu2<TAllitasiKiserletVisitor> BejaratiJelzoEmeltyu2 { get; }
    private TValtokezelo? _valtokezelo;
    public TValtokezelo Valtokezelo
    {
        get => _valtokezelo!;
        private set
        {
            if (_valtokezelo != null)
            {
                throw new InvalidOperationException("A váltókezelő már be lett állítva");
            }
            _valtokezelo = value;
        }
    }

    protected EmeltyuCsoport(Irany allomasvegIrany, Elojelzo elojelzo, Fojelzo bejaratiJelzo)
    {
        AllomasvegIrany = allomasvegIrany;
        ElojelzoEmeltyu = new(elojelzo);
        BejaratiJelzoEmeltyu1 = new(bejaratiJelzo);
        BejaratiJelzoEmeltyu2 = new(bejaratiJelzo);
    }

    public virtual void ValtokezeloHozzarendel(TValtokezelo valtokezelo)
    {
        Valtokezelo = valtokezelo;
    }

    public virtual bool Tartalmaz(Emeltyu<TAllitasiKiserletVisitor> emeltyu)
        => emeltyu == ElojelzoEmeltyu
            || emeltyu == BejaratiJelzoEmeltyu1
            || emeltyu == BejaratiJelzoEmeltyu2;
}
