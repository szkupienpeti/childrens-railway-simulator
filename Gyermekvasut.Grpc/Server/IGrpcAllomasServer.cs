using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Microsoft.AspNetCore.Builder;

namespace Gyermekvasut.Grpc.Server;

public interface IGrpcAllomasServer
{
    event EventHandler<GrpcRequestEventArgs<CsengetesRequest>>? GrpcCsengetesEvent;
    event EventHandler<GrpcRequestEventArgs<VisszaCsengetesRequest>>? GrpcVisszaCsengetesEvent;
    event EventHandler<GrpcEngedelyKeresEventArgs>? GrpcEngedelyKeresEvent;
    event EventHandler<GrpcEngedelyAdasEventArgs>? GrpcEngedelyAdasEvent;
    event EventHandler<GrpcEngedelyMegtagadasEventArgs>? GrpcEngedelyMegtagadasEvent;
    event EventHandler<GrpcRequestEventArgs<IndulasiIdoKozlesRequest>>? GrpcIndulasiIdoKozlesEvent;
    event EventHandler<GrpcIndulasiIdoKozlesVetelEventArgs>? GrpcIndulasiIdoKozlesVetelEvent;
    event EventHandler<GrpcVisszajelentesEventArgs>? GrpcVisszajelentesEvent;
    event EventHandler<GrpcVisszajelentesVetelEventArgs>? GrpcVisszajelentesVetelEvent;
    event EventHandler<GrpcRequestEventArgs<VonatAllomaskozbeBelepRequest>>? GrpcVonatAllomaskozbeBelepEvent;
    event EventHandler<GrpcRequestEventArgs<VonatAllomaskozbolKilepRequest>>? GrpcVonatAllomaskozbolKilepEvent;

    void SetApp(WebApplication app);
    void Stop();

    Task<Empty> Csengetes(CsengetesRequest request, ServerCallContext context);
}
