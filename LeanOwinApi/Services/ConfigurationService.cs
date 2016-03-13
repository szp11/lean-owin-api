using System;
using System.Collections.Concurrent;
using System.Configuration;

namespace LeanOwinApi.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly ConcurrentDictionary<string, object> _map = new ConcurrentDictionary<string, object>();
        private readonly Func<string, object> _getAppSetting = key => ConfigurationManager.AppSettings[key];
        private readonly Func<string, string> _errorMessage = key => $"Required configuration missing: {key}";

        public string GetString(string key)
        {
            var value = _map.GetOrAdd(key, _getAppSetting(key)) as string;
            if (!string.IsNullOrEmpty(value))
                return value;
            throw new ConfigurationErrorsException(_errorMessage(key));
        }

        public bool GetBool(string key)
        {
            var temp = _map.GetOrAdd(key, _getAppSetting(key)) as string;
            if (!string.IsNullOrEmpty(temp))
            {
                bool result;
                if (bool.TryParse(temp, out result))
                    return result;
            }
            throw new ConfigurationErrorsException(_errorMessage(key));
        }

        public int GetInt(string key)
        {
            var temp = _map.GetOrAdd(key, _getAppSetting(key)) as string;
            if (!string.IsNullOrEmpty(temp))
            {
                int result;
                if (int.TryParse(temp, out result))
                    return result;
            }
            throw new ConfigurationErrorsException(_errorMessage(key));
        }
    }
}
