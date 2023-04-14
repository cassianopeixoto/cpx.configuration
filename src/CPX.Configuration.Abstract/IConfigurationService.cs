namespace CPX.Configuration.Abstract;

public interface IConfigurationService
{
    string GetString(string key);
    int GetInteger(string key);
    decimal GetDecimal(string key);
    bool GetBoolean(string key);
    DateTime GetDateTime(string key);
    T GetObject<T>(string key) where T : class;
}