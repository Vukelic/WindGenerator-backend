using RepositoryModel.RepoModels.Abstractions.Common;
using RepositoryModel.RepoModels.Implementations.Role;
using RepositoryModel.RepoModels.Implementations.WindGeneratorDevice;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryModel.RepoModels.Implementations.User
{
    public class RepoUser : ARepoBaseEntity
    {
        #region -UserName- property
        private String _UserName;
        public String UserName
        {
            get { return _UserName; }
            set
            {
                if (_UserName != value)
                {
                    _UserName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

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

        #region -Surname- property
        private String _Surname;
        public String Surname
        {
            get { return _Surname; }
            set
            {
                if (_Surname != value)
                {
                    _Surname = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -Phone- property
        private String _Phone;
        public String Phone
        {
            get { return _Phone; }
            set
            {
                if (_Phone != value)
                {
                    _Phone = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -UserToken- property
        private String _UserToken;
        public String UserToken
        {
            get { return _UserToken; }
            set
            {
                if (_UserToken != value)
                {
                    _UserToken = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -ExpireTokenDateTime- property
        private DateTime? _ExpireTokenDateTime;
        public DateTime? ExpireTokenDateTime
        {
            get { return _ExpireTokenDateTime; }
            set
            {
                if (_ExpireTokenDateTime != value)
                {
                    _ExpireTokenDateTime = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -Workplace- property
        private String _Workplace;
        public String Workplace
        {
            get { return _Workplace; }
            set
            {
                if (_Workplace != value)
                {
                    _Workplace = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -PasswordHash- property
        private byte[] _PasswordHash;
        public byte[] PasswordHash
        {
            get { return _PasswordHash; }
            set
            {
                if (_PasswordHash != value)
                {
                    _PasswordHash = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -PasswordSalt- property
        private byte[] _PasswordSalt;
        public byte[] PasswordSalt
        {
            get { return _PasswordSalt; }
            set
            {
                if (_PasswordSalt != value)
                {
                    _PasswordSalt = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -Susspend- property
        private bool _Susspend;
        public bool Susspend
        {
            get { return _Susspend; }
            set
            {
                if (_Susspend != value)
                {
                    _Susspend = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -EmailConfirmed- property
        private bool _EmailConfirmed;
        public bool EmailConfirmed
        {
            get { return _EmailConfirmed; }
            set
            {
                if (_EmailConfirmed != value)
                {
                    _EmailConfirmed = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -FailedAttempt- property
        private int? _FailedAttempt;
        public int? FailedAttempt
        {
            get { return _FailedAttempt; }
            set
            {
                if (_FailedAttempt != value)
                {
                    _FailedAttempt = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -StartTrackingInterval- property
        private DateTime? _StartTrackingInterval;
        public DateTime? StartTrackingInterval
        {
            get { return _StartTrackingInterval; }
            set
            {
                if (_StartTrackingInterval != value)
                {
                    _StartTrackingInterval = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -LastLoginTime- property
        private DateTime? _LastLoginTime;
        public DateTime? LastLoginTime
        {
            get { return _LastLoginTime; }
            set
            {
                if (_LastLoginTime != value)
                {
                    _LastLoginTime = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -AppFlag- property
        private String _AppFlag;
        public String AppFlag
        {
            get { return _AppFlag; }
            set
            {
                if (_AppFlag != value)
                {
                    _AppFlag = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -RssId- property
        private String _RssId;
        public String RssId
        {
            get { return _RssId; }
            set
            {
                if (_RssId != value)
                {
                    _RssId = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        //COMPLEX PROPERTIES

        #region -AssignRole- property
        private RepoRole _AssignRole;
        public virtual RepoRole AssignRole
        {
            get { return _AssignRole; }
            set
            {
                if (_AssignRole != value)
                {
                    _AssignRole = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -AssignRoleId- property
        private long? _AssignRoleId;
        public long? AssignRoleId
        {
            get { return _AssignRoleId; }
            set
            {
                if (_AssignRoleId != value)
                {
                    _AssignRoleId = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -ListOfWindGeneratorDevice- property
        private List<RepoWindGeneratorDevice> _ListOfWindGeneratorDevice;
        public virtual List<RepoWindGeneratorDevice> ListOfWindGeneratorDevice
        {
            get { return _ListOfWindGeneratorDevice; }
            set
            {
                if (_ListOfWindGeneratorDevice != value)
                {
                    _ListOfWindGeneratorDevice = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion
    }
}
