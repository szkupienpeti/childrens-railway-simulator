namespace Gyermekvasut.Modellek.Ido;

public interface ITimerFactory
{
    ITimer Create(bool autoReset, double interval);
}
