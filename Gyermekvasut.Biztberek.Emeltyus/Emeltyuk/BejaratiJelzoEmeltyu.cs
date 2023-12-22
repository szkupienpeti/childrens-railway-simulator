﻿using Gyermekvasut.Modellek.Palya.Jelzok;

namespace Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;

public abstract class BejaratiJelzoEmeltyu<TAllitasiKiserletVisitor> : Emeltyu<TAllitasiKiserletVisitor>
    where TAllitasiKiserletVisitor : IEmeltyusAllitasiKiserletVisitor<TAllitasiKiserletVisitor>
{
    public Fojelzo BejaratiJelzo { get; }

    protected BejaratiJelzoEmeltyu(string nev, Fojelzo bejaratiJelzo, EmeltyuAllas alapAllas)
        : base(nev, alapAllas)
    {
        if (bejaratiJelzo.Rendeltetes != FojelzoRendeltetes.Bejarati)
        {
            throw new ArgumentException($"A főjelző nem bejárati jelző, hanem: {bejaratiJelzo.Rendeltetes}");
        }
        BejaratiJelzo = bejaratiJelzo;
    }
}
