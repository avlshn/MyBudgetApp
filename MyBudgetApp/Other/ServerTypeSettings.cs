using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudgetApp.Other
{
    internal class ServerTypeSettings
    {
        Dictionary<string, ServerTypeSetting> Connections = new Dictionary<string, ServerTypeSetting>();

        public ServerTypeSettings()
        {
            Connections.Add("SQLite", new ServerTypeSetting("SQLite",(optionsBuilder) => optionsBuilder.UseSqlite("Data Source=helloapp.db").UseLazyLoadingProxies()));
            Connections.Add("MSSQL", new ServerTypeSetting("MSSQL", (optionsBuilder) => optionsBuilder.Use ("Data Source=helloapp.db").UseLazyLoadingProxies()));
        }
    }
}
