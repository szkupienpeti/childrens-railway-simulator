namespace Gyermekvasut.Modellek.Ido;

public static class IdoUtil
{
    private static readonly int SECONDS_IN_MINUTE = 60;
    private static readonly int MILLISECONDS_IN_SECOND = 1000;

    public static double MinutesToTimerInterval(double minutes)
        => MinutesToTimerInterval(minutes, Szimulacio.Instance.SebessegSzorzo);

    public static double MinutesToTimerInterval(double minutes, double sebessegSzorzo)
        => minutes * SECONDS_IN_MINUTE * SecondsToTimerInterval(1, sebessegSzorzo);

    public static double SecondsToTimerInterval(double seconds)
        => SecondsToTimerInterval(seconds, Szimulacio.Instance.SebessegSzorzo);

    public static double SecondsToTimerInterval(double seconds, double sebessegSzorzo)
    {
        if (seconds <= 0)
        {
            throw new ArgumentException("Az időintervallum pozitív kell, hogy legyen");
        }
        if (sebessegSzorzo <= 0)
        {
            throw new ArgumentException("A sebességszorzó pozitív kell, hogy legyen");
        }
        return seconds * MILLISECONDS_IN_SECOND / sebessegSzorzo;
    }
}
