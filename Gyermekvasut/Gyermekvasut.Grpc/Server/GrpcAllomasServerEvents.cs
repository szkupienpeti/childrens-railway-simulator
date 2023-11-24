using Gyermekvasut.Grpc.Server.EventArgsNS;

namespace Gyermekvasut.Grpc.Server;

public class GrpcAllomasServerEvents
{
    private static readonly Lazy<GrpcAllomasServerEvents> lazy = new(() => new());

    public static GrpcAllomasServerEvents Instance => lazy.Value;

    /*
     * Mivel több server is futhat egy gépen, nem elég egy statikus singleton event kezelés
     * legalább állomásnév specifikusan kell
     * de a legszebb nyilván az lenne, ha a serveren lennének az eventek
     * ehhez viszont a halozatiallomas objektumnak kellene a server object? (addsingleton nem akar működni)
     */

    /*private static readonly Lazy<GrpcAllomasServerEventsHolder> lazy = new(() => new());

    public static GrpcAllomasServerEvents Get(GrpcAllomasNev grpcAllomasNev) => lazy.Value.Get(grpcAllomasNev);*/

    private GrpcAllomasServerEvents() { }

    /*public event EventHandler<GrpcCsengetesEventArgs>? GrpcCsengetesEvent;
    public void OnGrpcCsengetesEvent(object? sender, GrpcCsengetesEventArgs e)
    {
        GrpcCsengetesEvent?.Invoke(sender, e);
    }*/
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
}

/*class GrpcAllomasServerEventsHolder
{
    private Dictionary<GrpcAllomasNev, GrpcAllomasServerEvents> Events { get; } = new();

    internal GrpcAllomasServerEventsHolder()
    {
        Enum.GetValues<GrpcAllomasNev>.
    }

    public GrpcAllomasServerEvents Get(GrpcAllomasNev grpcAllomasNev) => Events[grpcAllomasNev];
}*/