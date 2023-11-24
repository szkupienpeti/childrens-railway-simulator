using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Grpc.Client;
using Gyermekvasut.Halozat.EventArgs;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Topologia;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Gyermekvasut.Grpc;
using Microsoft.AspNetCore.Hosting;
using Gyermekvasut.Modellek.Telefon;

namespace Gyermekvasut.Halozat
{
    public class HalozatiAllomas : Allomas
    {
        private static readonly string HALOZAT_CONFIG_FILE = "gyermekvasut.halozat.settings.json";
        private IConfiguration Configuration { get; }
        public GrpcAllomasServer AllomasServer { get; }
        private GrpcAllomasClient? KpAllomasClient { get; }
        private GrpcAllomasClient? VpAllomasClient { get; }

        public HalozatiAllomas(AllomasNev nev) : base(nev)
        {
            // Load config
            Configuration = new ConfigurationBuilder()
                .AddJsonFile(HALOZAT_CONFIG_FILE)
                .Build();
            // Subscribe to gRPC server events
            // TODO Ugly singleton solution
            // GrpcAllomasServerEvents serverEvents = GrpcAllomasServerEvents.Instance;
            // serverEvents.GrpcCsengetesEvent += ServerEvents_GrpcCsengetesEvent;
            // Start server
            AllomasServer = CreateAndStartServer();
            // Subscribe to gRCP server events
            // TODO New solution, not tested
            AllomasServer.GrpcCsengetesEvent += ServerEvents_GrpcCsengetesEvent;
            // Create client(s)
            AllomasNev? kpSzomszed = nev.KpSzomszed();
            KpAllomasClient = CreateAllomasClient(kpSzomszed);

            AllomasNev? vpSzomszed = nev.VpSzomszed();
            VpAllomasClient = CreateAllomasClient(vpSzomszed);
        }

        private GrpcAllomasServer CreateAndStartServer()
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddGrpc();
            // TODO Using AddSingleton: can receive instance, but results in 404s
            builder.Services.AddSingleton(new GrpcAllomasServer());
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //app.MapGrpcService<GrpcAllomasServer>(); // Worked, but could not receive instance
            app.MapGet("/", () => "Gyermekvasut.Grpc.Server.GrpcAllomasServer is working.");

            string address = GetAllomasAddress(Nev);
            app.RunAsync(address);

            return (app.Services.GetRequiredService(typeof(GrpcAllomasServer)) as GrpcAllomasServer)!;
        }

        private GrpcAllomasClient? CreateAllomasClient(AllomasNev? allomasNevOpt)
        {
            if (allomasNevOpt == null)
            {
                return null;
            }
            AllomasNev allomasNev = (AllomasNev)allomasNevOpt;
            string address = GetAllomasAddress(allomasNev);
            return new GrpcAllomasClient(address);
        }

        private string GetAllomasAddress(AllomasNev allomasNev)
        {
            string allomasKod = allomasNev.Kod();
            string configKey = $"Halozat:{allomasKod}";
            string? address = Configuration[configKey];
            return address ?? throw new KeyNotFoundException($"Hiányzó config: {configKey}");
        }

        public event EventHandler<CsengetesEventArgs>? Csengetes;
        private void ServerEvents_GrpcCsengetesEvent(object? sender, Grpc.Server.EventArgsNS.GrpcCsengetesEventArgs grpcEventArgs)
        {
            CsengetesEventArgs e = CsengetesEventArgs.FromGrpcEventArgs(grpcEventArgs);
            Csengetes?.Invoke(this, e);
        }

        public void Csenget(HivasIrany irany, List<Csengetes> csengetesek)
        {
            CsengetesRequest request = new()
            {
                Kuldo = ModelToGrpcMapper.MapAllomasNev(Nev)
            };
            ModelToGrpcMapper.FillRepeated(request.Csengetesek, csengetesek, ModelToGrpcMapper.MapCsengetes);
            GetSzomszedClient(irany).CsengetesAsync(request);
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
}