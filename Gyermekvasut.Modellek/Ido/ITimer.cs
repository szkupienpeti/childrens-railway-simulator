﻿using System.Timers;

namespace Gyermekvasut.Modellek.Ido;

public interface ITimer
{
    double Interval { get; set; }
    void Start();
    event EventHandler? Elapsed;
}
