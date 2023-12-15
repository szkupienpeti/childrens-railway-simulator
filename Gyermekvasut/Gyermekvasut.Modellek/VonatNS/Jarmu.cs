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
}

