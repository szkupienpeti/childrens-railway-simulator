using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Grpc.Client;
using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;
using Microsoft.Extensions.Configuration;
using Gyermekvasut.Halozat.Factory;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.VonatNS;

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
        SubscribeToAllomaskozEvents();
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

    private void SubscribeToAllomaskozEvents()
    {
        foreach (var allomaskoz in Topologia.Allomaskozok.Values)
        {
            if (allomaskoz != null)
            {
                allomaskoz.SzerelvenyChanged += Allomaskoz_SzerelvenyChanged;
            }
        }
    }

    private void Allomaskoz_SzerelvenyChanged(object? sender, SzakaszSzerelvenyChangedEventArgs e)
    {
        Szakasz allomaskoz = (sender as Szakasz)!;
        Irany allomaskozIrany = Topologia.GetAllomaskozIrany(allomaskoz);
        if (e.ElozoSzerelveny == null && allomaskoz.Szerelveny!.Irany == allomaskozIrany)
        {
            // Induló vonat
            VonatotAllomaskozbeBeleptet(allomaskozIrany, (allomaskoz.Szerelveny as Vonat)!);
        }
        else if (allomaskoz.Szerelveny == null && e.ElozoSzerelveny!.Irany != allomaskozIrany)
        {
            // Érkező vonat
            VonatotAllomaskozolKileptet(allomaskozIrany, (e.ElozoSzerelveny as Vonat)!.Nev);
        }
    }

    private GrpcAllomasClient GetSzomszedClient(Irany irany)
    {
        return irany switch
        {
            Irany.KezdopontFele => KpAllomasClient!,
            Irany.VegpontFele => VpAllomasClient!,
            _ => throw new NotImplementedException()
        };
    }
}