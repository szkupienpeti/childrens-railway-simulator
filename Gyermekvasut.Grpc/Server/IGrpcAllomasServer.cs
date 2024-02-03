using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Builder;

namespace Gyermekvasut.Grpc.Server;

public interface IGrpcAllomasServer
{
    event EventHandler<GrpcRequestEventArgs<CsengetesRequest>>? GrpcCsengetesEvent;
    event EventHandler<GrpcRequestEventArgs<VisszaCsengetesRequest>>? GrpcVisszaCsengetesEvent;
    event EventHandler<GrpcRequestEventArgs<EngedelyKeresRequest>>? GrpcEngedelyKeresEvent;
    event EventHandler<GrpcRequestEventArgs<EngedelyAdasRequest>>? GrpcEngedelyAdasEvent;
    event EventHandler<GrpcRequestEventArgs<EngedelyMegtagadasRequest>>? GrpcEngedelyMegtagadasEvent;
    event EventHandler<GrpcRequestEventArgs<IndulasiIdoKozlesRequest>>? GrpcIndulasiIdoKozlesEvent;
    event EventHandler<GrpcRequestEventArgs<IndulasiIdoKozlesVetelRequest>>? GrpcIndulasiIdoKozlesVetelEvent;
    event EventHandler<GrpcRequestEventArgs<VisszajelentesRequest>>? GrpcVisszajelentesEvent;
    event EventHandler<GrpcRequestEventArgs<VisszajelentesVetelRequest>>? GrpcVisszajelentesVetelEvent;
    event EventHandler<GrpcRequestEventArgs<VonatAllomaskozbeBelepRequest>>? GrpcVonatAllomaskozbeBelepEvent;
    event EventHandler<GrpcRequestEventArgs<VonatAllomaskozbolKilepRequest>>? GrpcVonatAllomaskozbolKilepEvent;

    void SetApp(WebApplication app);
    void Stop();

    Task<Empty> Csengetes(CsengetesRequest request, ServerCallContext context);
}
