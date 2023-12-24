namespace Gyermekvasut.Modellek.Ido;

public class KozpontiOra
{
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
    public ITimer Timer { get; }

    public KozpontiOra(ITimer timer)
    {
        Timer = timer;
        Timer.Elapsed += Timer_Elapsed;
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
        Timer.Start();
    }

    public event EventHandler? KozpontiIdoChanged;

    private void OnKozpontiIdoChanged()
    {
        KozpontiIdoChanged?.Invoke(this, EventArgs.Empty);
    }
}