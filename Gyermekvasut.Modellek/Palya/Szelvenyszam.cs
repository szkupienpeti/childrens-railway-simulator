namespace Gyermekvasut.Modellek.Palya;

public readonly struct Szelvenyszam
{
    private static readonly int METERS_IN_HECTOMETER = 100;

    public Szelvenyszam(int hektometer, int meter)
    {
        if (hektometer < 0)
        {
            throw new ArgumentException("A hektométer nem lehet negatív");
        }
        if (meter < -99 || meter > 99)
        {
            throw new ArgumentException("A méter -99 és 99 között kell, hogy legyen");
        }
        Hektometer = hektometer;
        Meter = meter;
    }

    public int Hektometer { get; }
    public int Meter { get; }

    public int GetOsszMeter()
        => METERS_IN_HECTOMETER * Hektometer + Meter;
}
