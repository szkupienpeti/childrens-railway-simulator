namespace Gyermekvasut.Modellek.VonatNS;

public enum Irany
{
    Paros = 0,
    Paratlan = 1,
}

public static class IranyExtensions
{
    public static Irany Fordit(this Irany irany)
    {
        return irany switch
        {
            Irany.Paros => Irany.Paratlan,
            Irany.Paratlan => Irany.Paros,
            _ => throw new NotImplementedException()
        };
    }
}
