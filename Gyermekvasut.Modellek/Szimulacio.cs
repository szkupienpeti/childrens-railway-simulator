namespace Gyermekvasut.Modellek;

public sealed class Szimulacio
{
    private static readonly Lazy<Szimulacio> lazy = new(() => new());

    public static Szimulacio Instance => lazy.Value;

    private Szimulacio() { }

    public void Start(int ora, int perc)
    {
        KozpontiIdo = new TimeOnly(ora, perc);
        KozpontiOra.Interval = 60 * 1000 / SebessegSzorzo;
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
        get => KozpontiIdo;
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
