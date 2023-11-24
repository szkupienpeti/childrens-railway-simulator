using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Grpc.Client;
using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;
using Microsoft.Extensions.Configuration;
using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Halozat.Factory;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas : Allomas
{
    private static readonly string HALOZAT_CONFIG_FILE = "gyermekvasut.halozat.settings.json";
    private IConfiguration Configuration { get; }
    private GrpcAllomasServer AllomasServer { get; }
    private GrpcAllomasClient? KpAllomasClient { get; }
    private GrpcAllomasClient? VpAllomasClient { get; }

    public event EventHandler<CsengetesEventArgs>? CsengetesEvent;
    public event EventHandler<VisszaCsengetesEventArgs>? VisszaCsengetesEvent;
    public event EventHandler<EngedelyKeresEventArgs>? EngedelyKeresEvent;
    public event EventHandler<EngedelyAdasEventArgs>? EngedelyAdasEvent;
    public event EventHandler<EngedelyMegtagadasEventArgs>? EngedelyMegtagadasEvent;
    public event EventHandler<IndulasiIdoKozlesEventArgs>? IndulasiIdoKozlesEvent;
    public event EventHandler<IndulasiIdoKozlesVetelEventArgs>? IndulasiIdoKozlesVetelEvent;
    public event EventHandler<VisszajelentesEventArgs>? VisszajelentesEvent;
    public event EventHandler<VisszajelentesVetelEventArgs>? VisszajelentesVetelEvent;
    public event EventHandler<VonatAllomaskozbeBelepEventArgs>? VonatAllomaskozbeBelepEvent;
    public event EventHandler<VonatAllomaskozbolKilepEventArgs>? VonatAllomaskozbolKilepEvent;

    public HalozatiAllomas(AllomasNev nev) : base(nev)
    {
        Configuration = new ConfigurationBuilder()
            .AddJsonFile(HALOZAT_CONFIG_FILE)
            .Build();
        GrpcAllomasServerFactory serverFactory = new(Configuration);
        AllomasServer = serverFactory.CreateAndStart(AllomasNev);
        SubscribeToServerEvents();
        GrpcAllomasClientFactory clientFactory = new(Configuration);
        KpAllomasClient = clientFactory.CreateOptional(AllomasNev.KpSzomszed());
        VpAllomasClient = clientFactory.CreateOptional(AllomasNev.VpSzomszed());
    }

    private void SubscribeToServerEvents()
    {
        AllomasServer.GrpcCsengetesEvent += AllomasServer_GrpcCsengetesEvent;
        AllomasServer.GrpcVisszaCsengetesEvent += AllomasServer_GrpcVisszaCsengetesEvent;
        AllomasServer.GrpcEngedelyKeresEvent += AllomasServer_GrpcEngedelyKeresEvent;
        AllomasServer.GrpcEngedelyAdasEvent += AllomasServer_GrpcEngedelyAdasEvent;
        AllomasServer.GrpcEngedelyMegtagadasEvent += AllomasServer_GrpcEngedelyMegtagadasEvent;
        AllomasServer.GrpcIndulasiIdoKozlesEvent += AllomasServer_GrpcIndulasiIdoKozlesEvent;
        AllomasServer.GrpcIndulasiIdoKozlesVetelEvent += AllomasServer_GrpcIndulasiIdoKozlesVetelEvent;
        AllomasServer.GrpcVisszajelentesEvent += AllomasServer_GrpcVisszajelentesEvent;
        AllomasServer.GrpcVisszajelentesVetelEvent += AllomasServer_GrpcVisszajelentesVetelEvent;
        AllomasServer.GrpcVonatAllomaskozbeBelepEvent += AllomasServer_GrpcVonatAllomaskozbeBelepEvent;
        AllomasServer.GrpcVonatAllomaskozbolKilepEvent += AllomasServer_GrpcVonatAllomaskozbolKilepEvent;
    }

    private GrpcAllomasClient GetSzomszedClient(HivasIrany irany)
    {
        return irany switch
        {
            HivasIrany.KezdopontFele => KpAllomasClient!,
            HivasIrany.VegpontFele => VpAllomasClient!,
            _ => throw new NotImplementedException()
        };
    }
}