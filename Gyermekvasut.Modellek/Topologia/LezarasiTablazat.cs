using Gyermekvasut.Modellek.Palya;

namespace Gyermekvasut.Modellek.Topologia;

public class LezarasiTablazat
{
    private Dictionary<Vagany, Dictionary<Valto, ValtoAllas?>> ValtoAllasok { get; } = new();

    public void AddValtoAllas(Vagany vagany, Valto valto, ValtoAllas valtoAllas)
    {
        ValtoAllasok.TryAdd(vagany, new());
        Dictionary<Valto, ValtoAllas?> vaganyValtoAllasok = ValtoAllasok[vagany];
        if (vaganyValtoAllasok.ContainsKey(valto))
        {
            throw new ArgumentException($"A(z) {vagany.Nev} vágányra a(z) {valto.Nev} váltónak már meg van adva a szükséges lezárása: {vaganyValtoAllasok[valto]}");
        }
        vaganyValtoAllasok.Add(valto, valtoAllas);
    }

    public ValtoAllas? GetValtoAllas(Vagany vagany, Valto valto)
    {
        if (!ValtoAllasok.TryGetValue(vagany, out Dictionary<Valto, ValtoAllas?>? vaganyValtoAllasok))
        {
            throw new ArgumentException($"A(z) {vagany.Nev} vágány nem található a lezárási táblázatban");
        }
        return vaganyValtoAllasok[valto];
    }

    public Vagany? GetVagany(Valto valto, ValtoAllas valtoAllas)
        => ValtoAllasok
            .Where(item => item.Value.TryGetValue(valto, out ValtoAllas? vaganyValtoAllas) && vaganyValtoAllas == valtoAllas)
            .Select(item => item.Key)
            .SingleOrDefault();
}
