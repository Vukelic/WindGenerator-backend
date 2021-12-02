using RepositoryModel.RepoModels.Abstractions.Common;
using RepositoryModel.RepoModels.Implementations.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryModel.RepoModels.Implementations.Role
{
    public class RepoRole: ARepoBaseEntity
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
        private List<RepoUser> _ListOfUsers;
        public virtual List<RepoUser> ListOfUsers
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
