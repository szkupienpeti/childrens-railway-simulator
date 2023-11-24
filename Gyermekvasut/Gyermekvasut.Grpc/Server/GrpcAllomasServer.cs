using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Gyermekvasut.Grpc.Server.EventArgsNS;

namespace Gyermekvasut.Grpc.Server;

public sealed class GrpcAllomasServer : GrpcAllomas.GrpcAllomasBase
{
    // Events
    public event EventHandler<GrpcCsengetesEventArgs>? GrpcCsengetesEvent;
    public event EventHandler<GrpcVisszaCsengetesEventArgs>? GrpcVisszaCsengetesEvent;
    public event EventHandler<GrpcEngedelyKeresEventArgs>? GrpcEngedelyKeresEvent;
    public event EventHandler<GrpcEngedelyAdasEventArgs>? GrpcEngedelyAdasEvent;
    public event EventHandler<GrpcEngedelyMegtagadasEventArgs>? GrpcEngedelyMegtagadasEvent;
    public event EventHandler<GrpcIndulasiIdoKozlesEventArgs>? GrpcIndulasiIdoKozlesEvent;
    public event EventHandler<GrpcIndulasiIdoKozlesVetelEventArgs>? GrpcIndulasiIdoKozlesVetelEvent;
    public event EventHandler<GrpcVisszajelentesEventArgs>? GrpcVisszajelentesEvent;
    public event EventHandler<GrpcVisszajelentesVetelEventArgs>? GrpcVisszajelentesVetelEvent;
    public event EventHandler<GrpcVonatAllomaskozbeBelepEventArgs>? GrpcVonatAllomaskozbeBelepEvent;
    public event EventHandler<GrpcVonatAllomaskozbolKilepEventArgs>? GrpcVonatAllomaskozbolKilepEvent;

    // Override gRPC methods: invoke events
    public sealed override Task<Empty> Csengetes(CsengetesRequest request, ServerCallContext context)
    {
        GrpcCsengetesEvent?.Invoke(this, new GrpcCsengetesEventArgs(request));
        return Task.FromResult(new Empty());
    }

    public sealed override Task<Empty> VisszaCsengetes(VisszaCsengetesRequest request, ServerCallContext context)
    {
        GrpcVisszaCsengetesEvent?.Invoke(this, new GrpcVisszaCsengetesEventArgs(request));
        return Task.FromResult(new Empty());
    }

    public sealed override Task<Empty> EngedelyKeres(EngedelyKeresRequest request, ServerCallContext context)
    {
        GrpcEngedelyKeresEvent?.Invoke(this, new GrpcEngedelyKeresEventArgs(request));
        return Task.FromResult(new Empty());
    }

    public sealed override Task<Empty> EngedelyAdas(EngedelyAdasRequest request, ServerCallContext context)
    {
        GrpcEngedelyAdasEvent?.Invoke(this, new GrpcEngedelyAdasEventArgs(request));
        return Task.FromResult(new Empty());
    }

    public sealed override Task<Empty> EngedelyMegtagadas(EngedelyMegtagadasRequest request, ServerCallContext context)
    {
        GrpcEngedelyMegtagadasEvent?.Invoke(this, new GrpcEngedelyMegtagadasEventArgs(request));
        return Task.FromResult(new Empty());
    }

    public sealed override Task<Empty> IndulasiIdoKozles(IndulasiIdoKozlesRequest request, ServerCallContext context)
    {
        GrpcIndulasiIdoKozlesEvent?.Invoke(this, new GrpcIndulasiIdoKozlesEventArgs(request));
        return Task.FromResult(new Empty());
    }

    public sealed override Task<Empty> IndulasiIdoKozlesVetel(IndulasiIdoKozlesVetelRequest request, ServerCallContext context)
    {
        GrpcIndulasiIdoKozlesVetelEvent?.Invoke(this, new GrpcIndulasiIdoKozlesVetelEventArgs(request));
        return Task.FromResult(new Empty());
    }

    public sealed override Task<Empty> Visszajelentes(VisszajelentesRequest request, ServerCallContext context)
    {
        GrpcVisszajelentesEvent?.Invoke(this, new GrpcVisszajelentesEventArgs(request));
        return Task.FromResult(new Empty());
    }

    public sealed override Task<Empty> VisszajelentesVetel(VisszajelentesVetelRequest request, ServerCallContext context)
    {
        GrpcVisszajelentesVetelEvent?.Invoke(this, new GrpcVisszajelentesVetelEventArgs(request));
        return Task.FromResult(new Empty());
    }

    public sealed override Task<Empty> VonatAllomaskozbeBelep(VonatAllomaskozbeBelepRequest request, ServerCallContext context)
    {
        GrpcVonatAllomaskozbeBelepEvent?.Invoke(this, new GrpcVonatAllomaskozbeBelepEventArgs(request));
        return Task.FromResult(new Empty());
    }

    public sealed override Task<Empty> VonatAllomaskozbolKilep(VonatAllomaskozbolKilepRequest request, ServerCallContext context)
    {
        GrpcVonatAllomaskozbolKilepEvent?.Invoke(this, new GrpcVonatAllomaskozbolKilepEventArgs(request));
        return Task.FromResult(new Empty());
    }
}

