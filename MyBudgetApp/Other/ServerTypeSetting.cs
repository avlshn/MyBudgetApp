﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudgetApp.Other
{
    internal class ServerTypeSetting
    {
        public readonly string DisplayName;

        public Action<DbContextOptionsBuilder> UseDBMode;

        public bool isServerEnabled;
        public bool isWindowsAuthenticationEnabled;
        //public bool isServerEnabled;
        //public bool isServerEnabled;

        public void CreateDB(DbContextOptionsBuilder optionsBuilder)
        {
            UseDBMode(optionsBuilder);
        }

        public override string ToString()
        {
            return DisplayName;
        }


        public ServerTypeSetting(string name, Action<DbContextOptionsBuilder> connect)
        {
            DisplayName = name;
            UseDBMode = connect;
        }
    }
}
