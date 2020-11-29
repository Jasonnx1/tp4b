using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Text;

namespace WeatherApp
{
    public static class AppConfiguration
    {

        private static IConfiguration Configuration;
        

        public static string GetValue(string key)
        {
            if (Configuration == null)
            {
                initConfig();
            }

            return Configuration[key];
        }

        public static void SetValue(string key, string value)
        {
            if (Configuration == null)
            {
                initConfig();
            }

            Configuration[key] = value;

        }

        private static void initConfig()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json",
                optional: true,
                reloadOnChange: true);

            var devEnvVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

            var isDevelopment = string.IsNullOrEmpty(devEnvVariable) ||
                                    devEnvVariable.ToLower() == "development";

            if (isDevelopment)
            {
                builder.AddUserSecrets<App>();
            }

            Configuration = builder.Build();
        }
    }
}