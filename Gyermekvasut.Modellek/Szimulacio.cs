using Gyermekvasut.Modellek.Ido;

namespace Gyermekvasut.Modellek;

public sealed class Szimulacio
{


    private static Szimulacio? _instance;
    public static Szimulacio Instance => _instance!;

    public int SebessegSzorzo { get; }
    public KozpontiOra Ora { get; }
    public bool Enabled { get; private set; }

    private Szimulacio(int sebessegSzorzo)
    {
        SebessegSzorzo = sebessegSzorzo;
        double interval = IdoUtil.MinutesToTimerInterval(1, SebessegSzorzo);
        ITimer timer = new TimerWrapper(true, interval);
        Ora = new(timer);
    }

    public void Start(TimeOnly kezdoIdo)
    {
        if (Enabled)
        {
            throw new InvalidOperationException("A szimulációt már elindították");
        }
        Enabled = true;
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

    public static void Cleanup()
    {
        _instance = null;
    }
}
