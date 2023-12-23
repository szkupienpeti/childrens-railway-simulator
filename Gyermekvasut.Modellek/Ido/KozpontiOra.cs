namespace Gyermekvasut.Modellek.Ido;

public class KozpontiOra
{
    private static readonly int SECONDS_IN_MINUTE = 60;
    private static readonly int MILLISECONDS_IN_SECOND = 1000;

    private TimeOnly _kozpontiIdo;
    public TimeOnly KozpontiIdo
    {
        get => _kozpontiIdo;
        private set
        {
            if (value != _kozpontiIdo)
            {
                _kozpontiIdo = value;
                OnKozpontiIdoChanged();
            }
        }
    }
    public bool Enabled { get; private set; }
    public int SebessegSzorzo { get; }
    public ITimer Timer { get; }

    public KozpontiOra(int sebessegSzorzo, ITimer timer)
    {
        SebessegSzorzo = sebessegSzorzo;
        Timer = timer;
    }

    private void Timer_Elapsed(object? sender, EventArgs e)
    {
        KozpontiIdo = KozpontiIdo.AddMinutes(1);
    }

    public void Start(TimeOnly kezdoIdo)
    {
        if (Enabled)
        {
            throw new InvalidOperationException("A központi órát már elindították");
        }
        Enabled = true;
        KozpontiIdo = kezdoIdo;
        Timer.Interval = (double)SECONDS_IN_MINUTE * MILLISECONDS_IN_SECOND / SebessegSzorzo;
        Timer.Elapsed += Timer_Elapsed;
        Timer.Start();
    }

    public event EventHandler? KozpontiIdoChanged;

    private void OnKozpontiIdoChanged()
    {
        KozpontiIdoChanged?.Invoke(this, EventArgs.Empty);
    }
}