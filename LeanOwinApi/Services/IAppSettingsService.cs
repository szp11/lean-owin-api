namespace LeanOwinApi.Services
{
    public interface IAppSettingsService
    {
        string GetString(string key);
        bool GetBool(string key);
        int GetInt(string key);
    }
}
