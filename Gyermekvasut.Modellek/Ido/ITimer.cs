namespace Gyermekvasut.Modellek.Ido;

public interface ITimer
{
    bool Enabled { get; }
    double Interval { get; set; }
    void Start();
    event EventHandler? Elapsed;
}
