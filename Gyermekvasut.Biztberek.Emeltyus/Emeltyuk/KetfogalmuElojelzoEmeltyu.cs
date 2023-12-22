using Gyermekvasut.Modellek.Palya.Jelzok;

namespace Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;

public class KetfogalmuElojelzoEmeltyu<TAllitasiKiserletVisitor> : Emeltyu<TAllitasiKiserletVisitor>
    where TAllitasiKiserletVisitor : IEmeltyusAllitasiKiserletVisitor<TAllitasiKiserletVisitor>
{
    public Elojelzo Elojelzo { get; }

    public KetfogalmuElojelzoEmeltyu(Elojelzo elojelzo)
        : base(elojelzo.Nev, EmeltyuAllas.Also)
    {
        Elojelzo = elojelzo;
    }

    protected override void KulsoteriObjektumAllit()
    {
        switch (Allas)
        {
            case EmeltyuAllas.Also:
                Elojelzo.Elojelzes = Sebessegjelzes.Megallj;
                break;
            case EmeltyuAllas.Felso:
                Elojelzo.Elojelzes = Sebessegjelzes.SebessegCsokkentesNelkuli;
                break;
        }
    }

    protected override EmeltyuAllitasEredmeny BiztberAllitasiKiserlet(TAllitasiKiserletVisitor biztber)
        => biztber.ElojelzoEmeltyuAllitasKiserlet(this);
}
