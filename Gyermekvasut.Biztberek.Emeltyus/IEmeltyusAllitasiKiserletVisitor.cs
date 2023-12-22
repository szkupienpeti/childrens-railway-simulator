using Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;

namespace Gyermekvasut.Biztberek.Emeltyus;

public interface IEmeltyusAllitasiKiserletVisitor<TAllitasiKiserletVisitor>
    where TAllitasiKiserletVisitor : IEmeltyusAllitasiKiserletVisitor<TAllitasiKiserletVisitor>
{
    EmeltyuAllitasEredmeny ElojelzoEmeltyuAllitasKiserlet(KetfogalmuElojelzoEmeltyu<TAllitasiKiserletVisitor> elojelzoEmeltyu);

    EmeltyuAllitasEredmeny BejaratiJelzoEmeltyu1AllitasKiserlet(BejaratiJelzoEmeltyu1<TAllitasiKiserletVisitor> bejaratiJelzoEmeltyu1);

    EmeltyuAllitasEredmeny BejaratiJelzoEmeltyu2AllitasKiserlet(BejaratiJelzoEmeltyu2<TAllitasiKiserletVisitor> bejaratiJelzoEmeltyu2);
}
