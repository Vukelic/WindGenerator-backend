using DtoModel.DtoModels.Abstractions.Common;
using DtoModel.DtoModels.Implementations.User;
using DtoModel.DtoModels.Interfaces.Role;
using System;
using System.Collections.Generic;
using System.Text;

namespace DtoModel.DtoModels.Implementations.Role
{
    public class DtoRole : ADtoBaseEntity, IDtoRole
    {
        #region -Name- property
        private String _Name;
        public String Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -Description- property
        private String _Description;
        public String Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -Active- property
        private bool _Active;
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (_Active != value)
                {
                    _Active = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        //COMPLEX PROPERTIES

        #region -ListOfUsers- property
        private List<DtoUser> _ListOfUsers;
        public List<DtoUser> ListOfUsers
        {
            get { return _ListOfUsers; }
            set
            {
                if (_ListOfUsers != value)
                {
                    _ListOfUsers = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion
    }
}
