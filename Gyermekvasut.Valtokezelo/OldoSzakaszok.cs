using Gyermekvasut.Modellek.Palya;

namespace Gyermekvasut.Valtokezelo;

internal class OldoSzakaszok
{
    public Szakasz OldoSzakasz { get; }
    public Szakasz OldoSzakaszElottiSzakasz { get; }

    public OldoSzakaszok(Szakasz oldoSzakasz, Szakasz oldoSzakaszElottiSzakasz)
    {
        OldoSzakasz = oldoSzakasz;
        OldoSzakaszElottiSzakasz = oldoSzakaszElottiSzakasz;
    }
}
