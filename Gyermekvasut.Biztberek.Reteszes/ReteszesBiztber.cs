using Gyermekvasut.Biztberek.Emeltyus;
using Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Palya;

namespace Gyermekvasut.Biztberek.Reteszes;

public class ReteszesBiztber : EmeltyusBiztber<ReteszesBiztber, ReteszesEmeltyuCsoport, ReteszesValtokezelo>, IReteszesAllitasiKiserletVisitor<ReteszesBiztber>
{
    public ReteszesBiztber(Allomas allomas, EmeltyuCsoportFactory<ReteszesBiztber, ReteszesEmeltyuCsoport, ReteszesValtokezelo> emeltyuCsoportFactory)
        : base(allomas, emeltyuCsoportFactory) { }

    protected override bool ValtoSzerkezetilegLezarva(ReteszesEmeltyuCsoport emeltyuCsoport, ValtoAllas valtoAllas)
        => !emeltyuCsoport.ReteszEmeltyu.UresbenMozog && emeltyuCsoport.ReteszEmeltyu.Allas == GetReteszEmeltyuAllas(valtoAllas);

    private static EmeltyuAllas GetReteszEmeltyuAllas(ValtoAllas valtoAllas)
        => valtoAllas switch
        {
            ValtoAllas.Egyenes => EmeltyuAllas.Also,
            ValtoAllas.Kitero => EmeltyuAllas.Felso,
            _ => throw new NotImplementedException()
        };

    // Állítási kísérletek
    public EmeltyuAllitasEredmeny ReteszEmeltyuAllitasKiserlet(ReteszEmeltyu<ReteszesBiztber> reteszEmeltyu)
    {
        if (reteszEmeltyu.UresbenMozog)
        {
            return reteszEmeltyu.Allas switch
            {
                EmeltyuAllas.Also => ReteszLezarasKiserlet(reteszEmeltyu, ValtoAllas.Kitero),
                EmeltyuAllas.Felso => ReteszLezarasKiserlet(reteszEmeltyu, ValtoAllas.Egyenes),
                _ => throw new NotImplementedException()
            };
        }
        else
        {
            return ReteszFeloldasKiserlet(reteszEmeltyu);
        }
    }

    public EmeltyuAllitasEredmeny ReteszEmeltyuUresbenAllitasKiserlet(ReteszEmeltyu<ReteszesBiztber> reteszEmeltyu)
    {
        if (!reteszEmeltyu.UresbenMozog)
        {
            return EmeltyuAllitasEredmeny.NemAllithatoSzerkezetileg;
        }
        return EmeltyuAllitasEredmeny.Allithato;
    }

    // Retesz
    private EmeltyuAllitasEredmeny ReteszLezarasKiserlet(ReteszEmeltyu<ReteszesBiztber> reteszEmeltyu, ValtoAllas valtoAllas)
    {
        if (!ValtoMegfeleloAllasban(reteszEmeltyu, valtoAllas))
        {
            return EmeltyuAllitasEredmeny.NemAllithatoSzerkezetileg;
        }
        if (!VaganyutElrendelve(reteszEmeltyu))
        {
            return EmeltyuAllitasEredmeny.AllitasMegtagadvaIndokolatlan;
        }
        return EmeltyuAllitasEredmeny.Allithato;
    }

    private EmeltyuAllitasEredmeny ReteszFeloldasKiserlet(ReteszEmeltyu<ReteszesBiztber> reteszEmeltyu)
    {
        if (!BejaratiJelzoMegalljban(reteszEmeltyu))
        {
            return EmeltyuAllitasEredmeny.NemAllithatoSzerkezetileg;
        }
        if (!VaganyutFelhasznalva(reteszEmeltyu))
        {
            return EmeltyuAllitasEredmeny.AllitasMegtagadvaKoraiVisszavetel;
        }
        return EmeltyuAllitasEredmeny.Allithato;
    }

    // Szerkezeti függések
    private static bool ValtoMegfeleloAllasban(ReteszEmeltyu<ReteszesBiztber> reteszEmeltyu, ValtoAllas valtoAllas)
        => reteszEmeltyu.Valto.Vegallas!.Value == valtoAllas;

    private bool VaganyutElrendelve(ReteszEmeltyu<ReteszesBiztber> reteszEmeltyu)
        => GetValtokezelo(reteszEmeltyu).AktualisVaganyutElrendeles != null;
}
