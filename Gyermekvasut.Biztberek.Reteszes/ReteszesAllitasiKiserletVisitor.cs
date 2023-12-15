using Gyermekvasut.Biztberek.Emeltyus;
using Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;

namespace Gyermekvasut.Biztberek.Reteszes;

public interface ReteszesAllitasiKiserletVisitor<TAllitasiKiserletVisitor> : EmeltyusAllitasiKiserletVisitor<TAllitasiKiserletVisitor>
    where TAllitasiKiserletVisitor : ReteszesAllitasiKiserletVisitor<TAllitasiKiserletVisitor>
{
    EmeltyuAllitasEredmeny ReteszEmeltyuAllitasKiserlet(ReteszEmeltyu<TAllitasiKiserletVisitor> reteszEmeltyu);
    EmeltyuAllitasEredmeny ReteszEmeltyuUresbenAllitasKiserlet(ReteszEmeltyu<TAllitasiKiserletVisitor> reteszEmeltyu);
}
