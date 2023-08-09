﻿using System.Text.Json;
using LBPUnion.PLRPC.Types.Configuration;
using Xunit;

namespace LBPUnion.PLRPC.Tests.Unit;

[Trait("Category", "Unit")]
public class ConfigurationTests
{
    private static readonly JsonSerializerOptions lenientJsonOptions = new()
    {
        AllowTrailingCommas = true,
        WriteIndented = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
    };

    [Fact]
    public async void CanGenerateAndParseConfiguration()
    {
        LocalConfiguration defaultConfig = new();
        await File.WriteAllTextAsync("./config.json", JsonSerializer.Serialize(defaultConfig, lenientJsonOptions));

        bool configurationExists = File.Exists("./config.json");

        Assert.True(configurationExists);

        string configurationJson = await File.ReadAllTextAsync("./config.json");
        LocalConfiguration? configuration = JsonSerializer.Deserialize<LocalConfiguration>(configurationJson, lenientJsonOptions);

        bool configurationIsValid = configuration is { ServerUrl: not null, Username: not null };

        Assert.True(configurationIsValid);
    }
}