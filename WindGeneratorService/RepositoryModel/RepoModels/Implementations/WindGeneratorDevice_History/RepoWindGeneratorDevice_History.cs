using RepositoryModel.RepoModels.Abstractions.Common;
using RepositoryModel.RepoModels.Implementations.WindGeneratorDevice;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryModel.RepoModels.Implementations.WindGeneratorDevice_History
{
    public class RepoWindGeneratorDevice_History : ARepoBaseEntity
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

        #region -ValueStr- property
        private String _ValueStr;
        public String ValueStr
        {
            get { return _ValueStr; }
            set
            {
                if (_ValueStr != value)
                {
                    _ValueStr = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -ValueDec- property
        private Decimal _ValueDec;
        public Decimal ValueDec
        {
            get { return _ValueDec; }
            set
            {
                if (_ValueDec != value)
                {
                    _ValueDec = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -GeographicalLatitude- property
        private double _GeographicalLatitude;
        public double GeographicalLatitude
        {
            get { return _GeographicalLatitude; }
            set
            {
                if (_GeographicalLatitude != value)
                {
                    _GeographicalLatitude = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -GeographicalLongitude- property
        private double _GeographicalLongitude;
        public double GeographicalLongitude
        {
            get { return _GeographicalLongitude; }
            set
            {
                if (_GeographicalLongitude != value)
                {
                    _GeographicalLongitude = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -GeographicalLatitudeStr- property
        private String _GeographicalLatitudeStr;
        public String GeographicalLatitudeStr
        {
            get { return _GeographicalLatitudeStr; }
            set
            {
                if (_GeographicalLatitudeStr != value)
                {
                    _GeographicalLatitudeStr = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -GeographicalLongitudeStr- property
        private String _GeographicalLongitudeStr;
        public String GeographicalLongitudeStr
        {
            get { return _GeographicalLongitudeStr; }
            set
            {
                if (_GeographicalLongitudeStr != value)
                {
                    _GeographicalLongitudeStr = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -Country- property
        private String _Country;
        public String Country
        {
            get { return _Country; }
            set
            {
                if (_Country != value)
                {
                    _Country = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -City- property
        private String _City;
        public String City
        {
            get { return _City; }
            set
            {
                if (_City != value)
                {
                    _City = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        //COMPLEX PROPERTIES    

        #region -ParentWindGeneratorDevice- property
        private RepoWindGeneratorDevice _ParentWindGeneratorDevice;
        public virtual RepoWindGeneratorDevice ParentWindGeneratorDevice
        {
            get { return _ParentWindGeneratorDevice; }
            set
            {
                if (_ParentWindGeneratorDevice != value)
                {
                    _ParentWindGeneratorDevice = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -ParentWindGeneratorDeviceId- property
        private long _ParentWindGeneratorDeviceId;
        public long ParentWindGeneratorDeviceId
        {
            get { return _ParentWindGeneratorDeviceId; }
            set
            {
                if (_ParentWindGeneratorDeviceId != value)
                {
                    _ParentWindGeneratorDeviceId = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion
    }
}
