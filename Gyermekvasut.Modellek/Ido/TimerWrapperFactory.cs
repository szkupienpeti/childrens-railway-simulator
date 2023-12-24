namespace Gyermekvasut.Modellek.Ido;

public class TimerWrapperFactory : ITimerFactory
{
    public ITimer Create(bool autoReset, double interval)
        => new TimerWrapper(autoReset, interval);
}
