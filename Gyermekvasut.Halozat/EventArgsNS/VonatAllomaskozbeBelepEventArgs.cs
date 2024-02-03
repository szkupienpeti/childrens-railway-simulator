﻿using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.VonatNS;

namespace Gyermekvasut.Halozat.EventArgsNS;

public class VonatAllomaskozbeBelepEventArgs : HalozatiAllomasEventArgs
{
    public Vonat Vonat { get; }
    public VonatAllomaskozbeBelepEventArgs(AllomasNev kuldo, Vonat vonat) : base(kuldo)
    {
        Vonat = vonat;
    }

    public static VonatAllomaskozbeBelepEventArgs FromGrpcEventArgs(GrpcRequestEventArgs<VonatAllomaskozbeBelepRequest> grpcEventArgs)
    {
        VonatAllomaskozbeBelepRequest request = grpcEventArgs.Request;
        AllomasNev kuldo = GrpcToModelMapper.MapAllomasNev(request.Kuldo);
        Vonat vonat = GrpcToModelMapper.MapVonat(request.Vonat);
        return new(kuldo, vonat);
    }
}
