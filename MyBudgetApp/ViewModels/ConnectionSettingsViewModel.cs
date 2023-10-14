using MyBudgetApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudgetApp.ViewModels
{
    internal class ConnectionSettingsViewModel : ViewModel
    {
        public enum ServerType
        {
            SQLite,
            MsSQL
        }

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
            set { Set(ref _isTrustedConnection, value); }

        }

        public bool IsReadOnly 
        {
            get => !IsTrustedConnection; 
        }


        private ServerType _selectedServerType;

        public ServerType SelectedServerType
        {
            get { return _selectedServerType; }
            set { Set(ref _selectedServerType, value); }
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
        #endregion


        public ConnectionSettingsViewModel()
        {
            
        }
    }
}
