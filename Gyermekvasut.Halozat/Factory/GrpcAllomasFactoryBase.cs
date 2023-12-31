using Gyermekvasut.Modellek.AllomasNS;
using Microsoft.Extensions.Configuration;

namespace Gyermekvasut.Halozat.Factory;

public abstract class GrpcAllomasFactoryBase
{
    private IConfiguration Configuration { get; }

    protected GrpcAllomasFactoryBase(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected string GetAllomasAddress(AllomasNev allomasNev)
    {
        string allomasKod = allomasNev.Kod();
        string configKey = $"Halozat:{allomasKod}";
        string? address = Configuration[configKey];
        return address ?? throw new KeyNotFoundException($"Hiányzó config: {configKey}");
    }
}
