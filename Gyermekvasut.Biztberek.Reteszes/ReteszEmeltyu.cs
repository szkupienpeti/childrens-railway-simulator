using Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;
using Gyermekvasut.Modellek.Palya;

namespace Gyermekvasut.Biztberek.Reteszes;

public class ReteszEmeltyu<TAllitasiKiserletVisitor> : Emeltyu<TAllitasiKiserletVisitor>
    where TAllitasiKiserletVisitor : ReteszesAllitasiKiserletVisitor<TAllitasiKiserletVisitor>
{
    public bool UresbenMozog { get; private set; }
    public Valto Valto { get; }

    public ReteszEmeltyu(Valto valto)
        : base(valto.Nev, GetAlapEmeltyuAllas(valto))
    {
        Valto = valto;
        UresbenMozog = (valto.AlapLezaras == ValtoLezaras.Feloldva);
    }

    /// <summary>
    /// Csak kicsappantva állítás esetén hívódik meg, üresben állítás esetén nem.
    /// Vagyis az UresbenMozog érték azt adja meg, hogy a kicsappantás előtt üresben mozgott-e az emeltyű.
    /// </summary>
    protected override void KulsoteriObjektumAllit()
    {
        switch (Allas)
        {
            case EmeltyuAllas.Also:
                if (UresbenMozog)
                {
                    Valto.Lezar(ValtoAllas.Egyenes);
                }
                else
                {
                    Valto.Felold(ValtoAllas.Kitero);
                }
                break;
            case EmeltyuAllas.Felso:
                if (UresbenMozog)
                {
                    Valto.Lezar(ValtoAllas.Kitero);
                }
                else
                {
                    Valto.Felold(ValtoAllas.Egyenes);
                }
                break;
        }
        UresbenMozog = !UresbenMozog;
    }

    protected override EmeltyuAllitasEredmeny BiztberAllitasiKiserlet(TAllitasiKiserletVisitor biztber)
        => biztber.ReteszEmeltyuAllitasKiserlet(this);

    public EmeltyuAllitasEredmeny UresbenAllitasiKiserlet(TAllitasiKiserletVisitor biztber)
    {
        EmeltyuAllitasEredmeny biztberEredmeny = biztber.ReteszEmeltyuUresbenAllitasKiserlet(this);
        if (biztberEredmeny == EmeltyuAllitasEredmeny.Allithato)
        {
            UresbenAllit();
            return EmeltyuAllitasEredmeny.Atallitva;
        }
        else
        {
            return biztberEredmeny;
        }
    }

    private void UresbenAllit()
    {
        Allas = Allas.Allit();
    }

    private static EmeltyuAllas GetAlapEmeltyuAllas(Valto valto)
    {
        if (valto.AlapLezaras == ValtoLezaras.Lezarva && valto.AlapAllas == ValtoAllas.Kitero)
        {
            return EmeltyuAllas.Felso;
        }
        else
        {
            return EmeltyuAllas.Also;
        }
    }
}
