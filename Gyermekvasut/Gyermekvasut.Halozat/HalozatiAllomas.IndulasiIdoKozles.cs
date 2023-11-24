﻿using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Grpc.Server.EventArgsNS;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas : Allomas
{
    private void AllomasServer_GrpcIndulasiIdoKozlesEvent(object? sender, GrpcIndulasiIdoKozlesEventArgs grpcEventArgs)
    {
        IndulasiIdoKozlesEventArgs e = IndulasiIdoKozlesEventArgs.FromGrpcEventArgs(grpcEventArgs);
        IndulasiIdoKozlesEvent?.Invoke(this, e);
    }

    public void IndulasiIdotKozol(HivasIrany irany, string vonatszam, TimeOnly ido, string nev)
    {
        IndulasiIdoKozlesRequest request = new()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(AllomasNev),
            Vonatszam = vonatszam,
            Ido = ModelToGrpcMapper.MapTimeOnly(ido),
            Nev = nev
        };
        GetSzomszedClient(irany).IndulasiIdoKozlesAsync(request);
    }
}