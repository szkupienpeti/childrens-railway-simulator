using Gyermekvasut.Modellek.BiztberNS;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Palya.Jelzok;
using Gyermekvasut.Modellek.Topologia;

namespace Gyermekvasut.ValtokezeloNS;

public abstract class Valtokezelo
{
    public Valto Valto { get; }
    public ValtoLezarasSzerep ValtoLezarasSzerep { get; }
    public LezarasiTablazat LezarasiTablazat { get; }
    private Fojelzo BejaratiJelzo { get; }
    private OldoSzakaszok KijaratOldoSzakaszok { get; }
    private Dictionary<ValtoAllas, OldoSzakaszok> BejaratOldoSzakaszok { get; } = new();
    // Vágányút-beállítás
    public VaganyutElrendeles? AktualisVaganyutElrendeles { get; private set; }
    /// <summary>
    /// Vágányút felhasználva, ha az oldó szakasz foglalt lett, majd az azt megelőző szakasz felszabadult,
    /// a váltókezelő pedig a bejárati jelző visszavételére/váltó feloldására vár.
    /// </summary>
    public bool AktualisVaganyutFelhasznalva { get; private set; }
    public VaganyutElrendeles? KovetkezoKijaratiVaganyutElrendeles { get; private set; }
    private System.Timers.Timer VaganyutBeallitasTimer { get; } = new() { AutoReset = false };

    public event EventHandler<BejelentesEventArgs>? Bejelentes;
    
    public Valtokezelo(Valto valto, ValtoLezarasSzerep valtoLezarasSzerep,
        LezarasiTablazat lezarasiTablazat, Fojelzo bejaratiJelzo,
        Szakasz kijaratOldoSzakasz, Szakasz kijaratOldoSzakaszElottiSzakasz,
        Szakasz egyenesBejaratOldoSzakasz, Szakasz egyenesBejaratOldoSzakaszElottiSzakasz,
        Szakasz kiteroBejaratOldoSzakasz, Szakasz kiteroBejaratOldoSzakaszElottiSzakasz)
    {
        Valto = valto;
        ValtoLezarasSzerep = valtoLezarasSzerep;
        LezarasiTablazat = lezarasiTablazat;
        if (bejaratiJelzo.Rendeltetes != FojelzoRendeltetes.Bejarati)
        {
            throw new ArgumentException($"A megadott főjelző nem bejárati jelző: {bejaratiJelzo}");
        }
        BejaratiJelzo = bejaratiJelzo;
        KijaratOldoSzakaszok = new(kijaratOldoSzakasz, kijaratOldoSzakaszElottiSzakasz);
        BejaratOldoSzakaszok[ValtoAllas.Egyenes] = new(egyenesBejaratOldoSzakasz, egyenesBejaratOldoSzakaszElottiSzakasz);
        BejaratOldoSzakaszok[ValtoAllas.Kitero] = new(kiteroBejaratOldoSzakasz, kiteroBejaratOldoSzakaszElottiSzakasz);
        VaganyutBeallitasTimer.Elapsed += VaganyutBeallitasTimer_Elapsed;
    }

    public ValtokezeloElrendelhetoseg GetElrendelhetoseg()
    {
        if (AktualisVaganyutElrendeles == null)
        {
            return ValtokezeloElrendelhetoseg.BarmireElrendelheto;
        }
        else if (AktualisVaganyutElrendeles.Irany == VaganyutIrany.Bejarat
            && KovetkezoKijaratiVaganyutElrendeles == null)
        {
            return ValtokezeloElrendelhetoseg.CsakBejaratUtaniKijaratraRendelhetoEl;
        }
        else
        {
            return ValtokezeloElrendelhetoseg.NemRendelhetoEl;
        }
    }

    public void BejaratElrendel(VaganyutElrendeles elrendeles)
    {
        if (elrendeles.Irany != VaganyutIrany.Bejarat)
        {
            throw new ArgumentException($"Bejáratként csak bejárati vágányút rendelhető el, {elrendeles.Irany} nem");
        }
        if (AktualisVaganyutElrendeles != null)
        {
            throw new ArgumentException("Már van elrendelt vágányút, nem rendelhető el új bejárat");
        }
        AktualisVaganyutElrendeles = elrendeles;
        VaganyutBeallitasTimer.Start();
    }

    public void KijaratElrendel(VaganyutElrendeles elrendeles)
    {
        if (elrendeles.Irany != VaganyutIrany.Kijarat)
        {
            throw new ArgumentException($"Kijáratként csak kijárati vágányút rendelhető el, {elrendeles.Irany} nem");
        }
        if (AktualisVaganyutElrendeles != null)
        {
            throw new ArgumentException("Már van elrendelt vágányút, nem rendelhető el új (nem behaladás utáni) kijárat");
        }
        AktualisVaganyutElrendeles = elrendeles;
        VaganyutBeallitasTimer.Start();
    }

    public void BejaratUtaniKijaratElrendel(VaganyutElrendeles elrendeles)
    {
        if (elrendeles.Irany != VaganyutIrany.Kijarat)
        {
            throw new ArgumentException($"Bejárat utáni kijáratként csak kijárati vágányút rendelhető el, {elrendeles.Irany} nem");
        }
        if (AktualisVaganyutElrendeles?.Irany != VaganyutIrany.Bejarat)
        {
            throw new ArgumentException($"Az elrendelt vágányút ({AktualisVaganyutElrendeles?.Irany}) nem bejárati vágányút");
        }
        if (KovetkezoKijaratiVaganyutElrendeles != null)
        {
            throw new ArgumentException($"Már van bejárat utáni kijárati vágányút elrendelve" +
                $" a(z) {KovetkezoKijaratiVaganyutElrendeles.Vonatszam} vonatnak");
        }
        KovetkezoKijaratiVaganyutElrendeles = elrendeles;
    }

    private void VaganyutBeallitasTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        VaganyutBeallitas();
        if (ValtoLezarasSzerep == ValtoLezarasSzerep.Valtokezelo)
        {
            VaganyutLezaras();
        }
        Bejelentes?.Invoke(this, new(AktualisVaganyutElrendeles!));
        Szakasz oldoSzakasz = GetOldoSzakasz();
        oldoSzakasz.SzerelvenyChanged += OldoSzakasz_SzerelvenyChanged;
    }

    private void VaganyutBeallitas()
    {
        ValtoAllas? szuksegesAllas = LezarasiTablazat.GetValtoAllas(AktualisVaganyutElrendeles!.Vagany, Valto);
        if (szuksegesAllas != null)
        {
            Valto.Allit(szuksegesAllas.Value);
        }
    }
    protected abstract void VaganyutLezaras();

    private void OldoSzakasz_SzerelvenyChanged(object? sender, SzakaszSzerelvenyChangedEventArgs e)
    {
       if (e.ElozoSzerelveny == null && e.UjSzerelveny != null && e.UjSzerelveny.Nev == AktualisVaganyutElrendeles!.Vonatszam)
       {
            // Vonat az oldási szakaszra lépett
            Szakasz oldoSzakasz = (sender as Szakasz)!;
            oldoSzakasz.SzerelvenyChanged -= OldoSzakasz_SzerelvenyChanged;
            // Feliratkozás az előző szakaszról lehaladásra
            Szakasz oldoSzakaszElottiSzakasz = GetOldoSzakaszElottiSzakasz();
            oldoSzakaszElottiSzakasz.SzerelvenyChanged += OldoSzakaszElottiSzakasz_SzerelvenyChanged;
       }
    }

    private void OldoSzakaszElottiSzakasz_SzerelvenyChanged(object? sender, SzakaszSzerelvenyChangedEventArgs e)
    {
        if (e.ElozoSzerelveny != null && e.ElozoSzerelveny.Nev == AktualisVaganyutElrendeles!.Vonatszam && e.UjSzerelveny == null)
        {
            // Vonat lehaladt az oldási szakasz előtti szakaszról
            Szakasz oldoSzakaszElottiSzakasz = (sender as Szakasz)!;
            oldoSzakaszElottiSzakasz.SzerelvenyChanged -= OldoSzakaszElottiSzakasz_SzerelvenyChanged;
            AktualisVaganyutFelhasznalva = true;
            switch (AktualisVaganyutElrendeles.Irany)
            {
                case VaganyutIrany.Bejarat:
                    BejaratiJelzo.JelzesChanged += BejaratiJelzo_JelzesChanged;
                    break;
                case VaganyutIrany.Kijarat:
                    VaganyutVisszavetel();
                    break;
            }
        }
    }

    private void BejaratiJelzo_JelzesChanged(object? sender, EventArgs e)
    {
        if (!(AktualisVaganyutElrendeles!.Irany == VaganyutIrany.Bejarat && AktualisVaganyutFelhasznalva))
        {
            throw new InvalidOperationException("A váltókezelő csak felhasznált bejárati vágányút esetén várhat" +
                " a bejárati jelző visszavételére.");
        }
        if (AktualisVaganyutFelhasznalva && BejaratiJelzo.Jelzes == Sebessegjelzes.Megallj)
        {
            VaganyutVisszavetel();
        }
    }

    private void VaganyutVisszavetel()
    {
        if (ValtoLezarasSzerep == ValtoLezarasSzerep.Valtokezelo)
        {
            VaganyutFeloldas();
            ValtoVisszaallitasEsAlaphelyzet();
        }
        else
        {
            Valto.LezarasChanged += Valto_LezarasChanged;
        }
    }

    protected abstract void VaganyutFeloldas();

    private void ValtoVisszaallitasEsAlaphelyzet()
    {
        // Szabványos állásba visszaállít
        Valto.Allit(Valto.AlapAllas);
        // Alaphelyzet
        AktualisVaganyutElrendeles = null;
        AktualisVaganyutFelhasznalva = false;
        // Következő elrendelt vágányút beállítása (ha van)
        if (KovetkezoKijaratiVaganyutElrendeles != null)
        {
            AktualisVaganyutElrendeles = KovetkezoKijaratiVaganyutElrendeles;
            KovetkezoKijaratiVaganyutElrendeles = null;
            VaganyutBeallitasTimer.Start();
        }
    }

    private void Valto_LezarasChanged(object? sender, EventArgs e)
    {
        if (Valto.Lezaras == ValtoLezaras.Feloldva)
        {
            ValtoVisszaallitasEsAlaphelyzet();
        }
    }

    // Segédfüggvények
    private Szakasz GetOldoSzakasz()
        => GetOldoSzakaszok().OldoSzakasz;

    private Szakasz GetOldoSzakaszElottiSzakasz()
        => GetOldoSzakaszok().OldoSzakaszElottiSzakasz;

    private OldoSzakaszok GetOldoSzakaszok()
    {
        return AktualisVaganyutElrendeles!.Irany switch
        {
            VaganyutIrany.Bejarat => BejaratOldoSzakaszok[Valto.Vegallas!.Value],
            VaganyutIrany.Kijarat => KijaratOldoSzakaszok,
            _ => throw new ArgumentNullException("Nincs aktuális vágányút")
        };
    }
}
