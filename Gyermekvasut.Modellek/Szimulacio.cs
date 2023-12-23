using Gyermekvasut.Modellek.Ido;

namespace Gyermekvasut.Modellek;

public sealed class Szimulacio
{
    private static Szimulacio? _instance;
    public static Szimulacio Instance => _instance!;

    public int SebessegSzorzo { get; }
    public KozpontiOra Ora { get; }

    private Szimulacio(int sebessegSzorzo)
    {
        SebessegSzorzo = sebessegSzorzo;
        Ora = new(sebessegSzorzo, new TimerWrapper());
    }

    public void Start(TimeOnly kezdoIdo)
    {
        Ora.Start(kezdoIdo);
    }

    public static void Build(int sebessegSzorzo)
    {
        if (Instance != null)
        {
            throw new InvalidOperationException("A szimuláció már felépült");
        }
        _instance = new Szimulacio(sebessegSzorzo);
    }
}
