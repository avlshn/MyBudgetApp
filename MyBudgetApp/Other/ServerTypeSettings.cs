using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyBudgetApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudgetApp.Other
{
    internal static class ServerTypeSettings
    {
        public static Dictionary<string, ServerTypeSetting> Connections = new Dictionary<string, ServerTypeSetting>();

        static ServerTypeSettings()
        {
            string dbName = Settings.Default.DataBase;
            string serverName = Settings.Default.Server;
            bool isTrusted = Settings.Default.IsTrustedConnection;
            bool isEncrypted = Settings.Default.IsEncrypted;
            Connections.Add("SQLite", new ServerTypeSetting("SQLite",(optionsBuilder) => optionsBuilder.UseSqlite($"Data Source={dbName}").UseLazyLoadingProxies()));
            Connections.Add("MSSQL", new ServerTypeSetting("MSSQL", (optionsBuilder) => optionsBuilder.UseSqlServer($"Server={serverName};Database={dbName};Trusted_Connection={isTrusted};Encrypt={isEncrypted};").UseLazyLoadingProxies()));
        }
    }
}
