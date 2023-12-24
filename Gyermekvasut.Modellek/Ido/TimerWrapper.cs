using System.Timers;

namespace Gyermekvasut.Modellek.Ido;
internal class TimerWrapper : ITimer
{
    private System.Timers.Timer Timer { get; }

    public TimerWrapper(bool autoReset, double interval)
    {
        Timer = new()
        {
            AutoReset = autoReset,
            Interval = interval
        };
        Timer.Elapsed += Timer_Elapsed;
    }

    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        Elapsed?.Invoke(this, e);
    }

    public double Interval
    {
        get => Timer.Interval;
        set => Timer.Interval = value;
    }
    public bool Enabled => Timer.Enabled;

    public event EventHandler? Elapsed;

    public void Start() => Timer.Start();
}
