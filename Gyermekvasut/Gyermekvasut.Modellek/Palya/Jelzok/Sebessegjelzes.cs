using Gyermekvasut.Modellek.VonatNS;

namespace Gyermekvasut.Modellek.Palya.Jelzok;

public enum Sebessegjelzes
{
    Megallj = 0,
    SebessegCsokkentesNelkuli = 1,
    CsokkentettSebesseg = 2
}

static class SebessegjelzesExtensions
{
    private static readonly int MEGALLJ_SEBESSEG = 0;
    public static int Sebesseg(this Sebessegjelzes jelzes)
    {
        return jelzes switch
        {
            Sebessegjelzes.Megallj => MEGALLJ_SEBESSEG,
            Sebessegjelzes.SebessegCsokkentesNelkuli => Szerelveny.PALYASEBESSEG,
            Sebessegjelzes.CsokkentettSebesseg => Szerelveny.CSOKKENTETT_SEBESSEG,
            _ => throw new NotImplementedException()
        };
    }
}
