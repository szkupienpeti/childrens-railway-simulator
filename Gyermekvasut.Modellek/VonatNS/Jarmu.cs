
namespace Gyermekvasut.Modellek.VonatNS;

public class Jarmu
{
    public string Nev { get; }
    public JarmuTipus Tipus { get; }
    public Jarmu(string nev, JarmuTipus tipus)
    {
        Nev = nev;
        Tipus = tipus;
    }

    public override string ToString() => Nev;

    public override bool Equals(object? obj)
        => obj is Jarmu jarmu
            && Nev == jarmu.Nev
            && Tipus == jarmu.Tipus;
    
    public override int GetHashCode()
        => HashCode.Combine(Nev, Tipus);
}

