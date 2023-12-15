namespace Gyermekvasut.Modellek.VonatNS;

public enum JarmuTipus
{
    Mk45,
    Nagykilato
}

public static class JarmuTipusExtensions
{
    public static bool Vontato(this JarmuTipus jarmu)
    {
        return jarmu switch
        {
            JarmuTipus.Mk45 => true,
            JarmuTipus.Nagykilato => false,
            _ => throw new NotImplementedException()
        };
    }

    public static int Hossz(this JarmuTipus jarmu)
    {
        return jarmu switch
        {
            JarmuTipus.Mk45 => 10,
            JarmuTipus.Nagykilato => 20,
            _ => throw new NotImplementedException()
        };
    }
}