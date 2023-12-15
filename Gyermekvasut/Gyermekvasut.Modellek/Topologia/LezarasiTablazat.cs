using Gyermekvasut.Modellek.Palya;

namespace Gyermekvasut.Modellek.Topologia;

public class LezarasiTablazat
{
    public LezarasiTablazat() { }

    private Dictionary<Vagany, Dictionary<Valto, ValtoAllas?>> ValtoAllasok { get; } = new();

    public void AddValtoAllas(Vagany vagany, Valto valto, ValtoAllas valtoAllas)
    {
        if (!ValtoAllasok.ContainsKey(vagany))
        {
            ValtoAllasok[vagany] = new();
        }
        Dictionary<Valto, ValtoAllas?> vaganyValtoAllasok = ValtoAllasok[vagany];
        if (vaganyValtoAllasok.ContainsKey(valto))
        {
            throw new ArgumentException($"A(z) {vagany.Nev} vágányra a(z) {valto.Nev} váltónak már meg van adva a szükséges lezárása: {vaganyValtoAllasok[valto]}");
        }
        vaganyValtoAllasok[valto] = valtoAllas;
    }

    public ValtoAllas? GetValtoAllas(Vagany vagany, Valto valto)
    {
        if (!ValtoAllasok.ContainsKey(vagany))
        {
            throw new ArgumentException($"A(z) {vagany.Nev} vágány nem ta");
        }
        return ValtoAllasok[vagany][valto];
    }
}
