using Gyermekvasut.Modellek.Palya.Jelzok;

namespace Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;

public class BejaratiJelzoEmeltyu1<TAllitasiKiserletVisitor> : BejaratiJelzoEmeltyu<TAllitasiKiserletVisitor>
    where TAllitasiKiserletVisitor : EmeltyusAllitasiKiserletVisitor<TAllitasiKiserletVisitor>
{
    public BejaratiJelzoEmeltyu1(Fojelzo bejaratiJelzo)
        : base($"{bejaratiJelzo.Nev}1", bejaratiJelzo, EmeltyuAllas.Also) { }

    protected override EmeltyuAllitasEredmeny BiztberAllitasiKiserlet(TAllitasiKiserletVisitor biztber)
        => biztber.BejaratiJelzoEmeltyu1AllitasKiserlet(this);

    protected override void KulsoteriObjektumAllit()
    {
        switch (Allas)
        {
            case EmeltyuAllas.Also:
                BejaratiJelzo.Jelzes = Sebessegjelzes.Megallj;
                break;
            case EmeltyuAllas.Felso:
                BejaratiJelzo.Jelzes = Sebessegjelzes.SebessegCsokkentesNelkuli;
                break;
        }
    }
}
