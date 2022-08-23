using DtoServiceDAL.Interfaces.Role;
using DtoServiceDAL.Interfaces.User;
using DtoServiceDAL.Interfaces.WindGeneratorDevice;
using DtoServiceDAL.Interfaces.WindGeneratorDevice_History;
using DtoServiceDAL.Interfaces.WindGeneratorType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace DtoServiceDAL.Abstractions
{
    public abstract class ADtoDAL : IDtoDAL, INotifyPropertyChanged
    {

        protected ADtoDAL()
        {
        }

        public IDtoRoleDAL RoleDALService { get; set; }
        public virtual IDtoRoleDAL GetRoleDAL()
        {
            return RoleDALService;
        }

        public IDtoUserDAL UserDALService { get; set; }
        public virtual IDtoUserDAL GetUserDAL()
        {
            return UserDALService;
        }
        
        public IDtoWindGeneratorDeviceDAL WindGeneratorDeviceDALService { get; set; }
        public virtual IDtoWindGeneratorDeviceDAL GetWindGeneratorDeviceDAL()
        {
            return WindGeneratorDeviceDALService;
        }

        public IDtoWindGeneratorDevice_HistoryDAL WindGeneratorDevice_HistoryDALService { get; set; }
        public virtual IDtoWindGeneratorDevice_HistoryDAL GetWindGeneratorDevice_HistoryDAL()
        {
            return WindGeneratorDevice_HistoryDALService;
        }

        public IDtoWindGeneratorTypeDAL WindGeneratorTypeDALService { get; set; }
        public virtual IDtoWindGeneratorTypeDAL GetWindGeneratorTypeDAL()
        {
            return WindGeneratorTypeDALService;
        }

        #region INotifyPropertyChange implementation
        public event PropertyChangedEventHandler PropertyChanged;
        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
