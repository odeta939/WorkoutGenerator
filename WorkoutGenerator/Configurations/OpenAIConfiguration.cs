using Microsoft.Extensions.Configuration;

namespace WorkoutGenerator.Configurations;

public class OpenAIConfiguration
{
    private const string ConfigurationSectionName = "ChatGPT";
    public string? ApiToken { get; set; }
    public string? BaseAddress { get; set; }

    public OpenAIConfiguration(IConfiguration configuration)
    {
        var configurationSection = configuration.GetSection(ConfigurationSectionName);
        ApiToken = configurationSection[nameof(ApiToken)];
        BaseAddress = configurationSection[nameof(BaseAddress)];

    }
}