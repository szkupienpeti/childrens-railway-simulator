using Gyermekvasut.Modellek.Palya;

namespace Gyermekvasut.Modellek.Topologia;

internal class LezarasiTablazatFactory
{
    public AllomasiTopologia Topologia { get; }

    public LezarasiTablazatFactory(AllomasiTopologia topologia)
    {
        Topologia = topologia;
    }

    public LezarasiTablazat Create()
    {
        LezarasiTablazat lezarasiTablazat = new();
        foreach (Valto valto in  Topologia.Valtok)
        {
            foreach (ValtoAllas valtoAllas in Enum.GetValues<ValtoAllas>())
            {
                // Egyszerűsítés: minden váltó állás pontosan egy vágányra terel
                Vagany vagany = valto.GetGyokFeloliVagany(valtoAllas);
                lezarasiTablazat.AddValtoAllas(vagany, valto, valtoAllas);
            }
        }
        return lezarasiTablazat;
    }
}
