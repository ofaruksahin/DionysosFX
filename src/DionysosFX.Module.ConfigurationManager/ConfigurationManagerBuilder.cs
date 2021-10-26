using Autofac;
using DionysosFX.Swan;
using DionysosFX.Swan.Exceptions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DionysosFX.Module.ConfigurationManager
{
    public static class ConfigurationManagerBuilder
    {
        private static List<string> configurationFiles;

        public static List<string> ConfigurationFiles
        {
            get => configurationFiles ?? (configurationFiles = new List<string>());
        }

        private static IConfiguration configuration;

        public static IConfiguration Configuration
        {
            get => configuration;
            set => configuration = value;
        }

        public static void Build()
        {
            if (configuration != null)
                throw new Exception("Configuration Manager Already Defined.");
            IConfigurationBuilder builder = new ConfigurationBuilder();
            ConfigurationFiles.ForEach((file) =>
            {
                builder.AddJsonFile(file);
            });
            configuration = builder.Build();            
        }

    }

    public static class Configuration
    {
        public static IConfigurationSection GetSection(string key) => ConfigurationManagerBuilder.Configuration.GetSection(key);
    }
}
