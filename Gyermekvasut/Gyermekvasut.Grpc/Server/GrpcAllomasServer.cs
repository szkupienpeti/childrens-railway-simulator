using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Gyermekvasut.Grpc.Server.EventArgsNS;

namespace Gyermekvasut.Grpc.Server;

public sealed class GrpcAllomasServer : GrpcAllomas.GrpcAllomasBase
{
    public event EventHandler<GrpcCsengetesEventArgs>? GrpcCsengetesEvent;
    private void OnGrpcCsengetesEvent(object? sender, GrpcCsengetesEventArgs e)
    {
        GrpcCsengetesEvent?.Invoke(sender, e);
    }

    public sealed override Task<Empty> Csengetes(CsengetesRequest request, ServerCallContext context)
    {
        // GrpcAllomasServerEvents.Instance.OnGrpcCsengetesEvent(this, new GrpcCsengetesEventArgs(request));
        OnGrpcCsengetesEvent(this, new GrpcCsengetesEventArgs(request));
        return Task.FromResult(new Empty());
    }

    /*public sealed override Task<Empty> VisszaCsengetes(VisszaCsengetesRequest request, ServerCallContext context)
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
    }*/
}

