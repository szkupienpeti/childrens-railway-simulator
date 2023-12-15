using Gyermekvasut.Biztberek.Emeltyus;
using Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;

namespace Gyermekvasut.Biztberek.Reteszes;

public interface IReteszesAllitasiKiserletVisitor<TAllitasiKiserletVisitor> : EmeltyusAllitasiKiserletVisitor<TAllitasiKiserletVisitor>
    where TAllitasiKiserletVisitor : IReteszesAllitasiKiserletVisitor<TAllitasiKiserletVisitor>
{
    EmeltyuAllitasEredmeny ReteszEmeltyuAllitasKiserlet(ReteszEmeltyu<TAllitasiKiserletVisitor> reteszEmeltyu);
    EmeltyuAllitasEredmeny ReteszEmeltyuUresbenAllitasKiserlet(ReteszEmeltyu<TAllitasiKiserletVisitor> reteszEmeltyu);
}
