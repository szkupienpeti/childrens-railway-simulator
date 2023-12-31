﻿using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek.VonatNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.Palya;
using System.Diagnostics;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas : Allomas
{
    private void AllomasServer_GrpcVonatAllomaskozbeBelepEvent(object? sender, GrpcVonatAllomaskozbeBelepEventArgs grpcEventArgs)
    {
        VonatAllomaskozbeBelepEventArgs e = VonatAllomaskozbeBelepEventArgs.FromGrpcEventArgs(grpcEventArgs);
        BelepoVonatotAllomaskozbeLehelyez(e.Vonat);
        VonatAllomaskozbeBelepEvent?.Invoke(this, e);
    }

    private void BelepoVonatotAllomaskozbeLehelyez(Vonat vonat)
    {
        Irany allomasOldalIrany = vonat.Irany.Fordit();
        Szakasz allomaskoz = Topologia.Allomaskozok[allomasOldalIrany]!;
        Trace.WriteLine($"{AllomasNev}: Belépő {vonat} vonat lehelyezése {allomaskoz} állomásközbe");
        vonat.Lehelyez(allomaskoz);
    }

    public void VonatotAllomaskozbeBeleptet(Irany irany, Vonat vonat)
    {
        VonatAllomaskozbeBelepRequest request = new()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(AllomasNev),
            Vonat = ModelToGrpcMapper.MapVonat(vonat)
        };
        GetSzomszedClient(irany).VonatAllomaskozbeBelep(request);
    }
}