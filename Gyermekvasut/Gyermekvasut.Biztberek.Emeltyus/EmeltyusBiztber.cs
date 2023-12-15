using Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;
using Gyermekvasut.ValtokezeloNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.BiztberNS;
using Gyermekvasut.Modellek.Palya;

namespace Gyermekvasut.Biztberek.Emeltyus;

public abstract class EmeltyusBiztber<TAllitasiKiserletVisitor, TEmeltyuCsoport, TValtokezelo> : Biztber, EmeltyusAllitasiKiserletVisitor<TAllitasiKiserletVisitor>
    where TAllitasiKiserletVisitor : EmeltyusAllitasiKiserletVisitor<TAllitasiKiserletVisitor>
    where TEmeltyuCsoport : EmeltyuCsoport<TAllitasiKiserletVisitor, TValtokezelo>
    where TValtokezelo : Valtokezelo
{
    public Dictionary<Irany, TEmeltyuCsoport> EmeltyuCsoportok { get; } = new();

    public EmeltyusBiztber(Allomas allomas, EmeltyuCsoportFactory<TAllitasiKiserletVisitor, TEmeltyuCsoport, TValtokezelo> emeltyuCsoportFactory)
        : base(allomas)
    {
        foreach (Irany irany in Enum.GetValues<Irany>())
        {
            TEmeltyuCsoport emeltyuCsoport = emeltyuCsoportFactory.Create(irany);
            EmeltyuCsoportok[irany] = emeltyuCsoport;
        }
    }

    // Allítás kísérletek
    public EmeltyuAllitasEredmeny ElojelzoEmeltyuAllitasKiserlet(KetfogalmuElojelzoEmeltyu<TAllitasiKiserletVisitor> elojelzoEmeltyu)
    {
        return elojelzoEmeltyu.Allas switch
        {
            EmeltyuAllas.Also => ElojelzoSzabadElojelzesreAllitasKiserlet(elojelzoEmeltyu),
            EmeltyuAllas.Felso => ElojelzoMegalljElojelzesreAllitasKiserlet(elojelzoEmeltyu),
            _ => throw new NotImplementedException()
        };
    }

    public EmeltyuAllitasEredmeny BejaratiJelzoEmeltyu1AllitasKiserlet(BejaratiJelzoEmeltyu1<TAllitasiKiserletVisitor> bejaratiJelzoEmeltyu1)
    {
        return bejaratiJelzoEmeltyu1.Allas switch
        {
            EmeltyuAllas.Also => BejaratiJelzoSzabadraAllitasKiserlet(bejaratiJelzoEmeltyu1, ValtoAllas.Egyenes),
            EmeltyuAllas.Felso => BejaratiJelzoMegalljbaAllitasKiserlet(bejaratiJelzoEmeltyu1),
            _ => throw new NotImplementedException()
        };
    }

    public EmeltyuAllitasEredmeny BejaratiJelzoEmeltyu2AllitasKiserlet(BejaratiJelzoEmeltyu2<TAllitasiKiserletVisitor> bejaratiJelzoEmeltyu2)
    {
        return bejaratiJelzoEmeltyu2.Allas switch
        {
            EmeltyuAllas.Also => BejaratiJelzoMegalljbaAllitasKiserlet(bejaratiJelzoEmeltyu2),
            EmeltyuAllas.Felso => BejaratiJelzoSzabadraAllitasKiserlet(bejaratiJelzoEmeltyu2, ValtoAllas.Kitero),
            _ => throw new NotImplementedException()
        };
    }

    // Előjelző
    private EmeltyuAllitasEredmeny ElojelzoSzabadElojelzesreAllitasKiserlet(KetfogalmuElojelzoEmeltyu<TAllitasiKiserletVisitor> elojelzoEmeltyu)
    {
        if (!BejaratiJelzoEgyKarralSzabadban(elojelzoEmeltyu))
        {
            return EmeltyuAllitasEredmeny.NemAllithatoSzerkezetileg;
        }
        if (VaganyutFelhasznalva(elojelzoEmeltyu))
        {
            return EmeltyuAllitasEredmeny.AllitasMegtagadvaFelhasznaltVaganyut;
        }
        return EmeltyuAllitasEredmeny.Allithato;
    }

    private EmeltyuAllitasEredmeny ElojelzoMegalljElojelzesreAllitasKiserlet(KetfogalmuElojelzoEmeltyu<TAllitasiKiserletVisitor> elojelzoEmeltyu)
    {
        if (!VonatMegerkezett(elojelzoEmeltyu))
        {
            return EmeltyuAllitasEredmeny.AllitasMegtagadvaKoraiVisszavetel;
        }
        return EmeltyuAllitasEredmeny.Allithato;
    }

    // Bejárati jelző
    private EmeltyuAllitasEredmeny BejaratiJelzoSzabadraAllitasKiserlet(BejaratiJelzoEmeltyu<TAllitasiKiserletVisitor> bejaratiJelzoEmeltyu, ValtoAllas valtoAllas)
    {
        if (!(TulvegiBejaratiJelzoMegalljban(bejaratiJelzoEmeltyu) && ValtoSzerkezetilegLezarva(bejaratiJelzoEmeltyu, valtoAllas)))
        {
            return EmeltyuAllitasEredmeny.NemAllithatoSzerkezetileg;
        }
        if (!BejaratElrendelve(bejaratiJelzoEmeltyu))
        {
            return EmeltyuAllitasEredmeny.AllitasMegtagadvaIndokolatlan;
        }
        return EmeltyuAllitasEredmeny.Allithato;
    }

    private EmeltyuAllitasEredmeny BejaratiJelzoMegalljbaAllitasKiserlet(BejaratiJelzoEmeltyu<TAllitasiKiserletVisitor> bejaratiJelzoEmeltyu)
    {
        if (!ElojelzoMegalljElojelzesben(bejaratiJelzoEmeltyu))
        {
            return EmeltyuAllitasEredmeny.NemAllithatoSzerkezetileg;
        }
        if (!VonatMegerkezett(bejaratiJelzoEmeltyu))
        {
            return EmeltyuAllitasEredmeny.AllitasMegtagadvaKoraiVisszavetel;
        }
        return EmeltyuAllitasEredmeny.Allithato;
    }

    // Szerkezeti függések
    protected bool BejaratiJelzoEgyKarralSzabadban(Emeltyu<TAllitasiKiserletVisitor> emeltyu)
    {
        TEmeltyuCsoport emeltyuCsoport = GetEmeltyuCsoport(emeltyu);
        return emeltyuCsoport.BejaratiJelzoEmeltyu1.Allas == EmeltyuAllas.Felso
            && emeltyuCsoport.BejaratiJelzoEmeltyu2.Allas == EmeltyuAllas.Felso;
    }

    protected bool TulvegiBejaratiJelzoMegalljban(Emeltyu<TAllitasiKiserletVisitor> emeltyu)
        => BejaratiJelzoMegalljban(GetTulvegiEmeltyuCsoport(emeltyu));

    protected bool BejaratiJelzoMegalljban(Emeltyu<TAllitasiKiserletVisitor> emeltyu)
        => BejaratiJelzoMegalljban(GetEmeltyuCsoport(emeltyu));

    protected bool BejaratiJelzoMegalljban(TEmeltyuCsoport emeltyuCsoport)
        => emeltyuCsoport.BejaratiJelzoEmeltyu1.Allas == EmeltyuAllas.Also
            && emeltyuCsoport.BejaratiJelzoEmeltyu2.Allas == EmeltyuAllas.Felso;

    private bool ElojelzoMegalljElojelzesben(Emeltyu<TAllitasiKiserletVisitor> emeltyu)
        => ElojelzoMegalljElojelzesben(GetEmeltyuCsoport(emeltyu));

    private bool ElojelzoMegalljElojelzesben(TEmeltyuCsoport emeltyuCsoport)
        => emeltyuCsoport.ElojelzoEmeltyu.Allas == EmeltyuAllas.Also;

    protected bool ValtoSzerkezetilegLezarva(Emeltyu<TAllitasiKiserletVisitor> emeltyu, ValtoAllas valtoAllas)
    {
        TEmeltyuCsoport emeltyuCsoport = GetEmeltyuCsoport(emeltyu);
        return ValtoSzerkezetilegLezarva(emeltyuCsoport, valtoAllas);
    }

    protected abstract bool ValtoSzerkezetilegLezarva(TEmeltyuCsoport emeltyuCsoport, ValtoAllas valtoAllas);

    // Nem-szerkezeti függések
    protected bool VonatMegerkezett(Emeltyu<TAllitasiKiserletVisitor> emeltyu)
        => GetValtokezelo(emeltyu).AktualisVaganyutFelhasznalva;

    protected bool BejaratElrendelve(Emeltyu<TAllitasiKiserletVisitor> emeltyu)
        => GetValtokezelo(emeltyu).AktualisVaganyutElrendeles?.Irany == VaganyutIrany.Bejarat;

    protected bool VaganyutFelhasznalva(Emeltyu<TAllitasiKiserletVisitor> emeltyu)
        => GetValtokezelo(emeltyu).AktualisVaganyutFelhasznalva;

    // Segédfüggvények
    protected TEmeltyuCsoport GetEmeltyuCsoport(Emeltyu<TAllitasiKiserletVisitor> emeltyu)
        => EmeltyuCsoportok.Values
            .Where(emeltyuCsoport => emeltyuCsoport.Tartalmaz(emeltyu))
            .Single();

    protected Valtokezelo GetValtokezelo(Emeltyu<TAllitasiKiserletVisitor> emeltyu)
        => GetEmeltyuCsoport(emeltyu).Valtokezelo;

    protected TEmeltyuCsoport GetTulvegiEmeltyuCsoport(Emeltyu<TAllitasiKiserletVisitor> emeltyu)
        => EmeltyuCsoportok.Values
            .Where(emeltyuCsoport => !emeltyuCsoport.Tartalmaz(emeltyu))
            .Single();
}
