namespace LeanOwinApi.Services
{
    public interface IConfigurationService
    {
        string GetString(string key);
        bool GetBool(string key);
        int GetInt(string key);
    }
}
