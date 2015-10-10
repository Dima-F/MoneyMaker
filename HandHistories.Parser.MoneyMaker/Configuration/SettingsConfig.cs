﻿using System.Configuration;

namespace HandHistories.Parser.MoneyMaker.Configuration
{
    public static class SettingsConfig
    {
        public static  string GetConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static void SetConfig(string key, string value)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
