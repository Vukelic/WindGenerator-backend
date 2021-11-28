using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace DtoModel.DtoModels.Abstractions.Common
{
    public class ADtoBaseEntity : INotifyPropertyChanged
    {

        #region -Id- property
        private long _Id;
        public long Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion  

        #region -TimeCreated- property
        private DateTime? _TimeCreated;
        public DateTime? TimeCreated
        {
            get { return _TimeCreated; }
            set
            {
                if (_TimeCreated != value)
                {
                    _TimeCreated = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -CreatedUserId- property
        private string _CreatedUserId;
        public string CreatedUserId
        {
            get { return _CreatedUserId; }
            set
            {
                if (_CreatedUserId != value)
                {
                    _CreatedUserId = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -CreatedUserName- property
        private String _CreatedUserName;
        public String CreatedUserName
        {
            get { return _CreatedUserName; }
            set
            {
                if (_CreatedUserName != value)
                {
                    _CreatedUserName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -TimeModified- property
        private DateTime? _TimeModified;
        public DateTime? TimeModified
        {
            get { return _TimeModified; }
            set
            {
                if (_TimeModified != value)
                {
                    _TimeModified = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -ModifiedUserId- property
        private String _ModifiedUserId;
        public String ModifiedUserId
        {
            get { return _ModifiedUserId; }
            set
            {
                if (_ModifiedUserId != value)
                {
                    _ModifiedUserId = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -ModifiedUserName- property
        private String _ModifiedUserName;
        public String ModifiedUserName
        {
            get { return _ModifiedUserName; }
            set
            {
                if (_ModifiedUserName != value)
                {
                    _ModifiedUserName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -SoftDeleted- property
        private bool _SoftDeleted;
        public bool SoftDeleted
        {
            get { return _SoftDeleted; }
            set
            {
                if (_SoftDeleted != value)
                {
                    _SoftDeleted = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -SystemString- property
        private String _SystemString;
        public String SystemString
        {
            get { return _SystemString; }
            set
            {
                if (_SystemString != value)
                {
                    _SystemString = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -AdditionalJsonData- property
        private String _AdditionalJsonData;
        public String AdditionalJsonData
        {
            get { return _AdditionalJsonData; }
            set
            {
                if (_AdditionalJsonData != value)
                {
                    _AdditionalJsonData = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -IsVirtual- property
        private bool _IsVirtual;
        public bool IsVirtual
        {
            get { return _IsVirtual; }
            set
            {
                if (_IsVirtual != value)
                {
                    _IsVirtual = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -SoftDeleteReasonJson- property
        private String _SoftDeleteReasonJson;
        public String SoftDeleteReasonJson
        {
            get { return _SoftDeleteReasonJson; }
            set
            {
                if (_SoftDeleteReasonJson != value)
                {
                    _SoftDeleteReasonJson = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -SoftDeleteReasonInt- property
        private int _SoftDeleteReasonInt;
        public int SoftDeleteReasonInt
        {
            get { return _SoftDeleteReasonInt; }
            set
            {
                if (_SoftDeleteReasonInt != value)
                {
                    _SoftDeleteReasonInt = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region INotifyPropertyChange implementation
        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
