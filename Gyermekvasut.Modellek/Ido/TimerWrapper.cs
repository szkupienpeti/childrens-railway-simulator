using System.Timers;

namespace Gyermekvasut.Modellek.Ido;
internal class TimerWrapper : ITimer
{
    private System.Timers.Timer Timer { get; }

    public TimerWrapper()
    {
        Timer = new System.Timers.Timer();
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

    public event EventHandler? Elapsed;

    public void Start() => Timer.Start();
}
