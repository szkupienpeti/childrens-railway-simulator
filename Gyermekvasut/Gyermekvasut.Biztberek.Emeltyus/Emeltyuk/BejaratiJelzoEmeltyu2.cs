using Gyermekvasut.Modellek.Palya.Jelzok;

namespace Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;

public class BejaratiJelzoEmeltyu2<TAllitasiKiserletVisitor> : BejaratiJelzoEmeltyu<TAllitasiKiserletVisitor>
    where TAllitasiKiserletVisitor : EmeltyusAllitasiKiserletVisitor<TAllitasiKiserletVisitor>
{
    public BejaratiJelzoEmeltyu2(Fojelzo bejaratiJelzo)
        : base($"{bejaratiJelzo.Nev}2", bejaratiJelzo, EmeltyuAllas.Felso) { }

    protected override EmeltyuAllitasEredmeny BiztberAllitasiKiserlet(TAllitasiKiserletVisitor biztber)
        => biztber.BejaratiJelzoEmeltyu2AllitasKiserlet(this);

    protected override void KulsoteriObjektumAllit()
    {
        switch (Allas)
        {
            case EmeltyuAllas.Also:
                BejaratiJelzo.Jelzes = Sebessegjelzes.CsokkentettSebesseg;
                break;
            case EmeltyuAllas.Felso:
                BejaratiJelzo.Jelzes = Sebessegjelzes.Megallj;
                break;
        }
    }
}
