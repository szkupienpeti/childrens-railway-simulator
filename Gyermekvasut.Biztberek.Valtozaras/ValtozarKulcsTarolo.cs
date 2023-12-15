using Gyermekvasut.Modellek.Palya;

namespace Gyermekvasut.Biztberek.Valtozaras;

public class ValtozarKulcsTarolo
{
    public Valto Valto { get; }
    public ValtoAllas? ValtozarKulcs { get; private set; }

    public ValtozarKulcsTarolo(Valto valto, ValtoAllas? valtozarKulcs)
    {
        Valto = valto;
        ValtozarKulcs = valtozarKulcs;
    }

    public void ValtozarKulcsBetesz(ValtoAllas valtozarKulcs)
    {
        if (ValtozarKulcs != null)
        {
            throw new ArgumentException($"A(z) {valtozarKulcs} nem tehető be, mert már bent van a(z) {ValtozarKulcs}");
        }
        ValtozarKulcs = valtozarKulcs;
        Valto.Lezar(valtozarKulcs);
    }

    public ValtoAllas ValtozarKulcsKivesz()
    {
        if (ValtozarKulcs == null)
        {
            throw new InvalidOperationException("Nincs bent váltózárkulcs");
        }
        ValtoAllas valtozarKulcs = ValtozarKulcs!.Value;
        Valto.Felold(valtozarKulcs);
        ValtozarKulcs = null;
        return valtozarKulcs;
    }
}
