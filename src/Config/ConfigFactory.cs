using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace dottr.Config;

public static class ConfigFactory
{
    private const string ConfigFileName = ".dottr";

    public static Config GetOrCreateConfig()
    {
        var configFilePath = GetConfigFilePath();

        if (File.Exists(configFilePath))
        {
            MigrateIfNecessary(configFilePath);
            return LoadConfig(configFilePath);
        }

        var config = CreateDefault();
        PersistConfig(config, configFilePath);
        return config;
    }

    private static string GetConfigFilePath()
    {
        var userProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var configFilePath = Path.Combine(userProfilePath, ConfigFileName);
        return configFilePath;
    }

    private static void MigrateIfNecessary(string path)
    {
        var versionString = File.ReadLines(path).First();
        var serializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        var version = serializer.Deserialize<ConfigVersion>(versionString);
        switch (version.Version)
        {
            case Versions.V10:
                break;
        }
    }

    private static Config LoadConfig(string path)
    {
        var yaml = File.ReadAllText(path);
        var serializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        return serializer.Deserialize<Config>(yaml);
    }

    private static Config CreateDefault() => new()
    {
    };

    private static void PersistConfig(Config config, string path)
    {
        var serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        var yaml = serializer.Serialize(config);
        File.WriteAllText(path, yaml);
    }

    public class ConfigVersion : IConfigVersion
    {
        public string Version { get; set; } = null!;
    }
}