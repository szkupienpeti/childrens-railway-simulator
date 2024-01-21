using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Microsoft.AspNetCore.Builder;

namespace Gyermekvasut.Grpc.Server;

public interface IGrpcAllomasServer
{
    event EventHandler<GrpcRequestEventArgs<CsengetesRequest>>? GrpcCsengetesEvent;
    event EventHandler<GrpcVisszaCsengetesEventArgs>? GrpcVisszaCsengetesEvent;
    event EventHandler<GrpcEngedelyKeresEventArgs>? GrpcEngedelyKeresEvent;
    event EventHandler<GrpcEngedelyAdasEventArgs>? GrpcEngedelyAdasEvent;
    event EventHandler<GrpcEngedelyMegtagadasEventArgs>? GrpcEngedelyMegtagadasEvent;
    event EventHandler<GrpcIndulasiIdoKozlesEventArgs>? GrpcIndulasiIdoKozlesEvent;
    event EventHandler<GrpcIndulasiIdoKozlesVetelEventArgs>? GrpcIndulasiIdoKozlesVetelEvent;
    event EventHandler<GrpcVisszajelentesEventArgs>? GrpcVisszajelentesEvent;
    event EventHandler<GrpcVisszajelentesVetelEventArgs>? GrpcVisszajelentesVetelEvent;
    event EventHandler<GrpcVonatAllomaskozbeBelepEventArgs>? GrpcVonatAllomaskozbeBelepEvent;
    event EventHandler<GrpcVonatAllomaskozbolKilepEventArgs>? GrpcVonatAllomaskozbolKilepEvent;

    void SetApp(WebApplication app);
    void Stop();

    Task<Empty> Csengetes(CsengetesRequest request, ServerCallContext context);
}
