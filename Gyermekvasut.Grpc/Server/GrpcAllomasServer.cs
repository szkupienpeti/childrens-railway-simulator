using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Builder;

namespace Gyermekvasut.Grpc.Server;

public class GrpcAllomasServer : GrpcAllomas.GrpcAllomasBase, IGrpcAllomasServer
{
    private WebApplication? _app;
    private WebApplication App => _app!;

    // Events
    public event EventHandler<GrpcRequestEventArgs<CsengetesRequest>>? GrpcCsengetesEvent;
    public event EventHandler<GrpcRequestEventArgs<VisszaCsengetesRequest>>? GrpcVisszaCsengetesEvent;
    public event EventHandler<GrpcRequestEventArgs<EngedelyKeresRequest>>? GrpcEngedelyKeresEvent;
    public event EventHandler<GrpcRequestEventArgs<EngedelyAdasRequest>>? GrpcEngedelyAdasEvent;
    public event EventHandler<GrpcRequestEventArgs<EngedelyMegtagadasRequest>>? GrpcEngedelyMegtagadasEvent;
    public event EventHandler<GrpcRequestEventArgs<IndulasiIdoKozlesRequest>>? GrpcIndulasiIdoKozlesEvent;
    public event EventHandler<GrpcRequestEventArgs<IndulasiIdoKozlesVetelRequest>>? GrpcIndulasiIdoKozlesVetelEvent;
    public event EventHandler<GrpcRequestEventArgs<VisszajelentesRequest>>? GrpcVisszajelentesEvent;
    public event EventHandler<GrpcRequestEventArgs<VisszajelentesVetelRequest>>? GrpcVisszajelentesVetelEvent;
    public event EventHandler<GrpcRequestEventArgs<VonatAllomaskozbeBelepRequest>>? GrpcVonatAllomaskozbeBelepEvent;
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
        GrpcEngedelyKeresEvent?.Invoke(this, new GrpcRequestEventArgs<EngedelyKeresRequest>(request));
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> EngedelyAdas(EngedelyAdasRequest request, ServerCallContext context)
    {
        GrpcEngedelyAdasEvent?.Invoke(this, new GrpcRequestEventArgs<EngedelyAdasRequest>(request));
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> EngedelyMegtagadas(EngedelyMegtagadasRequest request, ServerCallContext context)
    {
        GrpcEngedelyMegtagadasEvent?.Invoke(this, new GrpcRequestEventArgs<EngedelyMegtagadasRequest>(request));
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> IndulasiIdoKozles(IndulasiIdoKozlesRequest request, ServerCallContext context)
    {
        GrpcIndulasiIdoKozlesEvent?.Invoke(this, new GrpcRequestEventArgs<IndulasiIdoKozlesRequest>(request));
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> IndulasiIdoKozlesVetel(IndulasiIdoKozlesVetelRequest request, ServerCallContext context)
    {
        GrpcIndulasiIdoKozlesVetelEvent?.Invoke(this, new GrpcRequestEventArgs<IndulasiIdoKozlesVetelRequest>(request));
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> Visszajelentes(VisszajelentesRequest request, ServerCallContext context)
    {
        GrpcVisszajelentesEvent?.Invoke(this, new GrpcRequestEventArgs<VisszajelentesRequest>(request));
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> VisszajelentesVetel(VisszajelentesVetelRequest request, ServerCallContext context)
    {
        GrpcVisszajelentesVetelEvent?.Invoke(this, new GrpcRequestEventArgs<VisszajelentesVetelRequest>(request));
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> VonatAllomaskozbeBelep(VonatAllomaskozbeBelepRequest request, ServerCallContext context)
    {
        GrpcVonatAllomaskozbeBelepEvent?.Invoke(this, new GrpcRequestEventArgs<VonatAllomaskozbeBelepRequest>(request));
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> VonatAllomaskozbolKilep(VonatAllomaskozbolKilepRequest request, ServerCallContext context)
    {
        GrpcVonatAllomaskozbolKilepEvent?.Invoke(this, new GrpcRequestEventArgs<VonatAllomaskozbolKilepRequest>(request));
        return Task.FromResult(new Empty());
    }
}

