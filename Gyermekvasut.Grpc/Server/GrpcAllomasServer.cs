using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Microsoft.AspNetCore.Builder;

namespace Gyermekvasut.Grpc.Server;

public class GrpcAllomasServer : GrpcAllomas.GrpcAllomasBase, IGrpcAllomasServer
{
    private WebApplication? _app;
    private WebApplication App => _app!;

    // Events
    public event EventHandler<GrpcRequestEventArgs<CsengetesRequest>>? GrpcCsengetesEvent;
    public event EventHandler<GrpcRequestEventArgs<VisszaCsengetesRequest>>? GrpcVisszaCsengetesEvent;
    public event EventHandler<GrpcEngedelyKeresEventArgs>? GrpcEngedelyKeresEvent;
    public event EventHandler<GrpcEngedelyAdasEventArgs>? GrpcEngedelyAdasEvent;
    public event EventHandler<GrpcEngedelyMegtagadasEventArgs>? GrpcEngedelyMegtagadasEvent;
    public event EventHandler<GrpcRequestEventArgs<IndulasiIdoKozlesRequest>>? GrpcIndulasiIdoKozlesEvent;
    public event EventHandler<GrpcIndulasiIdoKozlesVetelEventArgs>? GrpcIndulasiIdoKozlesVetelEvent;
    public event EventHandler<GrpcVisszajelentesEventArgs>? GrpcVisszajelentesEvent;
    public event EventHandler<GrpcVisszajelentesVetelEventArgs>? GrpcVisszajelentesVetelEvent;
    public event EventHandler<GrpcVonatAllomaskozbeBelepEventArgs>? GrpcVonatAllomaskozbeBelepEvent;
    public event EventHandler<GrpcRequestEventArgs<VonatAllomaskozbolKilepRequest>>? GrpcVonatAllomaskozbolKilepEvent;

    public void SetApp(WebApplication app)
    {
        if (_app != null)
        {
            throw new InvalidOperationException("A WebApplication példány már be lett állítva");
        }
        _app = app;
    }
    public void Stop()
    {
        App.StopAsync().Wait();
    }

    // Override gRPC methods: invoke events
    public override Task<Empty> Csengetes(CsengetesRequest request, ServerCallContext context)
    {
        GrpcCsengetesEvent?.Invoke(this, new GrpcRequestEventArgs<CsengetesRequest>(request));
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> VisszaCsengetes(VisszaCsengetesRequest request, ServerCallContext context)
    {
        GrpcVisszaCsengetesEvent?.Invoke(this, new GrpcRequestEventArgs<VisszaCsengetesRequest>(request));
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> EngedelyKeres(EngedelyKeresRequest request, ServerCallContext context)
    {
        GrpcEngedelyKeresEvent?.Invoke(this, new GrpcEngedelyKeresEventArgs(request));
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> EngedelyAdas(EngedelyAdasRequest request, ServerCallContext context)
    {
        GrpcEngedelyAdasEvent?.Invoke(this, new GrpcEngedelyAdasEventArgs(request));
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> EngedelyMegtagadas(EngedelyMegtagadasRequest request, ServerCallContext context)
    {
        GrpcEngedelyMegtagadasEvent?.Invoke(this, new GrpcEngedelyMegtagadasEventArgs(request));
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> IndulasiIdoKozles(IndulasiIdoKozlesRequest request, ServerCallContext context)
    {
        GrpcIndulasiIdoKozlesEvent?.Invoke(this, new GrpcRequestEventArgs<IndulasiIdoKozlesRequest>(request));
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> IndulasiIdoKozlesVetel(IndulasiIdoKozlesVetelRequest request, ServerCallContext context)
    {
        GrpcIndulasiIdoKozlesVetelEvent?.Invoke(this, new GrpcIndulasiIdoKozlesVetelEventArgs(request));
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> Visszajelentes(VisszajelentesRequest request, ServerCallContext context)
    {
        GrpcVisszajelentesEvent?.Invoke(this, new GrpcVisszajelentesEventArgs(request));
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> VisszajelentesVetel(VisszajelentesVetelRequest request, ServerCallContext context)
    {
        GrpcVisszajelentesVetelEvent?.Invoke(this, new GrpcVisszajelentesVetelEventArgs(request));
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> VonatAllomaskozbeBelep(VonatAllomaskozbeBelepRequest request, ServerCallContext context)
    {
        GrpcVonatAllomaskozbeBelepEvent?.Invoke(this, new GrpcVonatAllomaskozbeBelepEventArgs(request));
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> VonatAllomaskozbolKilep(VonatAllomaskozbolKilepRequest request, ServerCallContext context)
    {
        GrpcVonatAllomaskozbolKilepEvent?.Invoke(this, new GrpcRequestEventArgs<VonatAllomaskozbolKilepRequest>(request));
        return Task.FromResult(new Empty());
    }
}

