﻿using Gyermekvasut.Modellek.VonatNS;
using System.Diagnostics;

namespace Gyermekvasut.Modellek.Palya;

public class Szakasz : EgyenesPalyaElem
{
    public int Hossz { get; }
    private Szerelveny? _szerelveny = null;
    public Szerelveny? Szerelveny
    {
        get => _szerelveny;
        private set
        {
            if (value != _szerelveny)
            {
                Szerelveny? elozoSzerelveny = _szerelveny;
                _szerelveny = value;
                OnSzerelvenyChanged(elozoSzerelveny);
            }
        }
    }
    public event EventHandler<SzakaszSzerelvenyChangedEventArgs>? SzerelvenyChanged;

    public Szakasz(string nev, int hossz) : base(nev)
    {
        Hossz = hossz;
    }

    public void Elfoglal(Szerelveny szerelveny)
    {
        if (Szerelveny != null)
        {
            throw new ArgumentException("Foglalt szakaszt próbál elfoglalni egy szerelvény");
        }
        Szerelveny = szerelveny;
        Trace.WriteLine($"{szerelveny} elfoglalta {this} szakaszt");
    }

    public virtual void Felszabadit(Szerelveny szerelveny)
    {
        if (Szerelveny != szerelveny)
        {
            throw new ArgumentException("Nem a szakasz szerelvénye próbálja felszabadítani a szakaszt");
        }
        Szerelveny = null;
        Trace.WriteLine($"{szerelveny} elhagyta {this} szakaszt");
    }

    protected virtual void OnSzerelvenyChanged(Szerelveny? elozoSzerelveny)
    {
        SzerelvenyChanged?.Invoke(this, new(elozoSzerelveny));
    }
}

public class SzakaszSzerelvenyChangedEventArgs : EventArgs
{
    public Szerelveny? ElozoSzerelveny { get; }
    public SzakaszSzerelvenyChangedEventArgs(Szerelveny? elozoSzerelveny)
    {
        ElozoSzerelveny = elozoSzerelveny;
    }
}