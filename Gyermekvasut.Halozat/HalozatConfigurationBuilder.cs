using Microsoft.Extensions.Configuration;

namespace Gyermekvasut.Halozat;

public class HalozatConfigurationBuilder
{
    private ConfigurationBuilder ConfigurationBuilder { get; }
    public HalozatConfigurationBuilder()
    {
        ConfigurationBuilder = new ConfigurationBuilder();
    }

    public HalozatConfigurationBuilder AddJsonFile(string path)
    {
        ConfigurationBuilder.AddJsonFile(path);
        return this;
    }

    public IConfiguration Build()
        => ConfigurationBuilder.Build();
}
