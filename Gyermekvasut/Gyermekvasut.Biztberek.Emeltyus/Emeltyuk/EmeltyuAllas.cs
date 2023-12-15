namespace Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;

public enum EmeltyuAllas
{
    Also = 0,
    Felso = 1
}

public static class EmeltyuAllasExtensions
{
    public static EmeltyuAllas Allit(this EmeltyuAllas allas)
    {
        return allas switch
        {
            EmeltyuAllas.Also => EmeltyuAllas.Felso,
            EmeltyuAllas.Felso => EmeltyuAllas.Also,
            _ => throw new NotImplementedException()
        };
    }
}
