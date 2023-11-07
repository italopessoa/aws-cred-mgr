// Copyright (c) 2023 Ellosoft Limited. All rights reserved.

using Ellosoft.AwsCredentialsManager.Services.Configuration.Models;

namespace Ellosoft.AwsCredentialsManager.Services.Configuration;

public interface IConfigManager
{
    AppConfig AppConfig { get; }

    /// <summary>
    ///     Persist the <see cref="AppConfig" /> state into a file
    /// </summary>
    void SaveConfig();
}

public class ConfigManager : IConfigManager
{
    private const string APP_CONFIG_FILE = "aws_cred_mgr.yml";

    private static readonly string AppConfigPath = Path.Combine(AppDataDirectory.UserProfileDirectory, APP_CONFIG_FILE);

    private readonly ConfigReader _configReader = new();
    private readonly ConfigWriter _configWriter = new();

    public ConfigManager() => AppConfig = GetConfiguration();

    public AppConfig AppConfig { get; }

    public void SaveConfig() => _configWriter.Write(AppConfigPath, AppConfig);

    private AppConfig GetConfiguration() => File.Exists(AppConfigPath) ? _configReader.Read(AppConfigPath) : new AppConfig();
}
