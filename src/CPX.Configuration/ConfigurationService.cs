using CPX.Configuration.Abstract;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CPX.Configuration;

public sealed class ConfigurationService : IConfigurationService
{
    private readonly IConfiguration configuration;

    public ConfigurationService(IConfiguration? configuration)
    {
        if (configuration is null)
            throw new ArgumentNullException(nameof(configuration));

        this.configuration = configuration;
    }

    public bool GetBoolean(string key)
    {
        var stringValue = GetString(key);

        if (bool.TryParse(stringValue, out bool value))
            return value;

        throw ArgumentException();
    }

    public DateTime GetDateTime(string key)
    {
        var stringValue = GetString(key);

        if (DateTime.TryParse(stringValue, out DateTime value))
            return value;

        throw ArgumentException();
    }

    public decimal GetDecimal(string key)
    {
        var stringValue = GetString(key);

        if (decimal.TryParse(stringValue, out decimal value))
            return value;

        throw ArgumentException();
    }

    public int GetInteger(string key)
    {
        var stringValue = GetString(key);

        if (int.TryParse(stringValue, out int value))
            return value;

        throw ArgumentException();
    }

    public T GetObject<T>(string key) where T : class
    {
        var stringValue = GetString(key);

        try
        {
            var @object = JsonConvert.DeserializeObject<T>(stringValue);

            if (@object == null)
                throw ArgumentException();

            return @object;
        }
        catch
        {
            throw ArgumentException();
        }
    }

    public string GetString(string key)
    {
        var value = configuration.GetSection(key).Value;

        if (value == null)
            throw ArgumentException();

        return value;
    }

    private ArgumentException ArgumentException()
    {
        return new ArgumentException();
    }
}