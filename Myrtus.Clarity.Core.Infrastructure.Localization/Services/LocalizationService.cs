using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Myrtus.Clarity.Core.Application.Abstractions.Localization.Services;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Myrtus.Clarity.Core.Infrastructure.Localization.Services
{
    public class LocalizationService : ILocalizationService
    {
        private readonly Dictionary<string, Dictionary<string, string>> _localizedMessages;
        private readonly ILogger<LocalizationService> _logger;

        public LocalizationService(ILogger<LocalizationService> logger)
        {
            _logger = logger;
            _localizedMessages = LoadLocalizedMessages();
        }

        public string GetLocalizedString(string key, string language)
        {
            if (_localizedMessages.TryGetValue(language, out var messages) && messages.TryGetValue(key, out var message))
            {
                return message;
            }

            var baseLanguage = language.Split('-')[0];
            if (_localizedMessages.TryGetValue(baseLanguage, out var baseMessages) && baseMessages.TryGetValue(key, out var baseMessage))
            {
                return baseMessage;
            }

            if (_localizedMessages.TryGetValue("en-US", out var defaultMessages) && defaultMessages.TryGetValue(key, out var defaultMessage))
            {
                return defaultMessage;
            }

            return key;
        }

        private Dictionary<string, Dictionary<string, string>> LoadLocalizedMessages()
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();

            var localizedMessages = new Dictionary<string, Dictionary<string, string>>();
            var resourceDirectory = Path.Combine(AppContext.BaseDirectory, "Resources");

            if (Directory.Exists(resourceDirectory))
            {
                var yamlFiles = Directory.GetFiles(resourceDirectory, "*.yaml");

                foreach (var filePath in yamlFiles)
                {
                    try
                    {
                        var language = Path.GetFileNameWithoutExtension(filePath);
                        var yamlContent = File.ReadAllText(filePath, Encoding.UTF8);

                        var messages = deserializer.Deserialize<Dictionary<string, object>>(yamlContent);
                        localizedMessages[language] = FlattenDictionary(messages);

                        _logger.LogInformation("Successfully loaded {Count} keys for language {Language}", localizedMessages[language].Count, language);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error deserializing YAML file: {FilePath}", filePath);
                    }
                }
            }

            return localizedMessages;
        }

        private Dictionary<string, string> FlattenDictionary(Dictionary<string, object> nestedDict, string parentKey = "")
        {
            var flatDict = new Dictionary<string, string>();

            foreach (var pair in nestedDict)
            {
                var key = string.IsNullOrEmpty(parentKey) ? pair.Key : $"{parentKey}.{pair.Key}";

                if (pair.Value is Dictionary<object, object> nested)
                {
                    var nestedDictTyped = nested.ToDictionary(k => k.Key.ToString()!, v => v.Value);
                    foreach (var innerPair in FlattenDictionary(nestedDictTyped, key))
                    {
                        flatDict[innerPair.Key] = innerPair.Value;
                    }
                }
                else
                {
                    flatDict[key] = pair.Value?.ToString() ?? string.Empty;
                }
            }

            return flatDict;
        }
    }
}
