
using Microsoft.Extensions.Configuration;

public static class ConfigReader
{
    public static TestConfig Load()
    {
        var root = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)  // = bin/Debug/
            .AddJsonFile("Config/appsettings.json").Build();
        return root.Get<TestConfig>()!;
    }
}