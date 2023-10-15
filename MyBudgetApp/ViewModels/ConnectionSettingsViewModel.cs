using MyBudgetApp.Commands;
using MyBudgetApp.Other;
using MyBudgetApp.Properties;
using MyBudgetApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyBudgetApp.ViewModels
{
    internal class ConnectionSettingsViewModel : ViewModel
    {
        #region Properties

        private string? _serverSetting;

		public string ServerSetting
        {
            get => _serverSetting;
            set { Set(ref _serverSetting, value); }
        }


        private string? _dataBaseSetting;

        public string? DataBaseSetting
        {
            get { return _dataBaseSetting; }
            set { Set(ref _dataBaseSetting, value); }
        }


        private bool _isTrustedConnection;

        public bool IsTrustedConnection
        {
            get { return _isTrustedConnection; }
            set 
            { 
                if (!value && IsControlEnabledAuthentication) IsControlEnabledCredentials = true;
                else IsControlEnabledCredentials = false;
                Set(ref _isTrustedConnection, value); 
            }

        }


        private bool _isEncryptedConnection;

        public bool IsEncryptedConnection
        {
            get { return _isEncryptedConnection; }
            set { Set(ref _isEncryptedConnection, value); }
        }


        private ServerTypeSetting _selectedServerType;

        public ServerTypeSetting SelectedServerType
        {
            get { return _selectedServerType; }
            set 
            {
                
                if (value.DisplayName == "MSSQL")
                {
                    IsControlEnabledServer = true;
                    IsControlEnabledAuthentication = true;
                    IsControlEnabledEncryption = true;
                }
                if (value.DisplayName == "SQLite")
                {
                    IsControlEnabledServer = false;
                    IsControlEnabledAuthentication = false;
                    IsControlEnabledEncryption = false;
                    IsControlEnabledCredentials = false;
                }
                if (!_isTrustedConnection && IsControlEnabledAuthentication) IsControlEnabledCredentials = true;

                Set(ref _selectedServerType, value);
            }
        }


        private ObservableCollection<ServerTypeSetting> _serverTypes;

        public ObservableCollection<ServerTypeSetting> ServerTypes
        {
            get { return _serverTypes; }
        }


        private string? _loginSetting;

        public string LoginSetting
        {
            get => _loginSetting;
            set { Set(ref _loginSetting, value); }
        }


        private string? _passwordSetting;

        public string PasswordSetting
        {
            get => _passwordSetting;
            set { Set(ref _passwordSetting, value); }
        }


        #region Controls Disabling
        private bool _isControlEnabledServer;

        public bool IsControlEnabledServer
        {
            get { return _isControlEnabledServer; }
            set { Set(ref _isControlEnabledServer, value); }
        }


        private bool _isControlEnabledAuthentication;

        public bool IsControlEnabledAuthentication
        {
            get { return _isControlEnabledAuthentication; }
            set { Set(ref _isControlEnabledAuthentication, value); }
        }


        private bool _isControlEnabledCredentials;

        public bool IsControlEnabledCredentials
        {
            get { return _isControlEnabledCredentials; }
            set { Set(ref _isControlEnabledCredentials, value); }
        }


        private bool _isControlEnabledEncryption;

        public bool IsControlEnabledEncryption
        {
            get { return _isControlEnabledEncryption; }
            set { Set(ref _isControlEnabledEncryption, value); }
        }


        #endregion


        #endregion


        #region Commands

        #region ConnectionSettingsOK

        public ICommand ConnectionSettingsOKCommand { get; }

        private bool CanConnectionSettingsOKCommand(object o) => true;

        private void OnConnectionSettingsOKCommand(object o)
        {
            Settings.Default.Server = _serverSetting;
            Settings.Default.DataBase = _dataBaseSetting;
            Settings.Default.ConnectionServerType = _selectedServerType.DisplayName;
            Settings.Default.IsTrustedConnection = _isTrustedConnection;
            Settings.Default.Save();

            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        #endregion

        #endregion


        public ConnectionSettingsViewModel()
        {
            _serverSetting = Settings.Default.Server;
            _dataBaseSetting = Settings.Default.DataBase;
            _isTrustedConnection = Settings.Default.IsTrustedConnection;

            _serverTypes = new ObservableCollection<ServerTypeSetting>();
            foreach (var n in ServerTypeSettings.Connections) _serverTypes.Add(n.Value);

            _selectedServerType = ServerTypeSettings.Connections[Settings.Default.ConnectionServerType];


            #region Commands CTOR

            ConnectionSettingsOKCommand = new RelayCommand(OnConnectionSettingsOKCommand, CanConnectionSettingsOKCommand);

            #endregion
        }
    }
}
