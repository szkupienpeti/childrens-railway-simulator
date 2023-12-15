namespace Gyermekvasut.Modellek;

public sealed class Szimulacio
{
    private static readonly int SECONDS_IN_MINUTE = 60;
    private static readonly int MILLISECONDS_IN_SECOND = 1000;

    private static readonly Lazy<Szimulacio> lazy = new(() => new());

    public static Szimulacio Instance => lazy.Value;

    private Szimulacio() { }

    public void Start(int ora, int perc)
    {
        KozpontiIdo = new TimeOnly(ora, perc);
        KozpontiOra.Interval = (double)SECONDS_IN_MINUTE * MILLISECONDS_IN_SECOND / SebessegSzorzo;
        KozpontiOra.Elapsed += KozpontiOra_Elapsed;
        KozpontiOra.Start();
    }

    private void KozpontiOra_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        KozpontiIdo = KozpontiIdo.AddMinutes(1);
    }

    private System.Timers.Timer KozpontiOra { get; } = new();
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
    public event EventHandler? KozpontiIdoChanged;

    private void OnKozpontiIdoChanged()
    {
        KozpontiIdoChanged?.Invoke(this, EventArgs.Empty);
    }
    public int SebessegSzorzo { get; set; } = 2;
}
