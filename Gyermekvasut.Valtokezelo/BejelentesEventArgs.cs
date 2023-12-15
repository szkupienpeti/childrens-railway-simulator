﻿namespace Gyermekvasut.ValtokezeloNS;

public class BejelentesEventArgs : EventArgs
{
    public VaganyutElrendeles Elrendeles { get; }
    public BejelentesEventArgs(VaganyutElrendeles elrendeles)
    {
        Elrendeles = elrendeles;
    }
}