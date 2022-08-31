using DtoModel.DtoModels.Abstractions.Common;
using DtoModel.DtoModels.Implementations.WindGeneratorDevice;
using DtoModel.DtoModels.Interfaces.WindGeneratorType;
using System;
using System.Collections.Generic;
using System.Text;

namespace DtoModel.DtoModels.Implementations.WindGeneratorType
{
    public class DtoWindGeneratorType : ADtoBaseEntity, IDtoWindGeneratorType
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

        #region -Turbines- property
        private String _Turbines;
        public String Turbines
        {
            get { return _Turbines; }
            set
            {
                if (_Turbines != value)
                {
                    _Turbines = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -PowerOfTurbines- property
        private String _PowerOfTurbines;
        public String PowerOfTurbines
        {
            get { return _PowerOfTurbines; }
            set
            {
                if (_PowerOfTurbines != value)
                {
                    _PowerOfTurbines = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -HeightOfWing- property
        private String _HeightOfWing;
        public String HeightOfWing
        {
            get { return _HeightOfWing; }
            set
            {
                if (_HeightOfWing != value)
                {
                    _HeightOfWing = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -WidthOfWing- property
        private String _WidthOfWing;
        public String WidthOfWing
        {
            get { return _WidthOfWing; }
            set
            {
                if (_WidthOfWing != value)
                {
                    _WidthOfWing = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -Weight- property
        private String _Weight;
        public String Weight
        {
            get { return _Weight; }
            set
            {
                if (_Weight != value)
                {
                    _Weight = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -MaxPowerTurbine- property
        private String _MaxPowerTurbine;
        public String MaxPowerTurbine
        {
            get { return _MaxPowerTurbine; }
            set
            {
                if (_MaxPowerTurbine != value)
                {
                    _MaxPowerTurbine = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -MaxSpeedTurbine- property
        private String _MaxSpeedTurbine;
        public String MaxSpeedTurbine
        {
            get { return _MaxSpeedTurbine; }
            set
            {
                if (_MaxSpeedTurbine != value)
                {
                    _MaxSpeedTurbine = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -GeneratorPower- property
        private String _GeneratorPower;
        public String GeneratorPower
        {
            get { return _GeneratorPower; }
            set
            {
                if (_GeneratorPower != value)
                {
                    _GeneratorPower = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -Guarantee- property
        private String _Guarantee;
        public String Guarantee
        {
            get { return _Guarantee; }
            set
            {
                if (_Guarantee != value)
                {
                    _Guarantee = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -ImageUrl- property
        private String _ImageUrl;
        public String ImageUrl
        {
            get { return _ImageUrl; }
            set
            {
                if (_ImageUrl != value)
                {
                    _ImageUrl = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -BasePrice- property
        private int _BasePrice;
        public int BasePrice
        {
            get { return _BasePrice; }
            set
            {
                if (_BasePrice != value)
                {
                    _BasePrice = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -InstallationCosts- property
        private int _InstallationCosts;
        public int InstallationCosts
        {
            get { return _InstallationCosts; }
            set
            {
                if (_InstallationCosts != value)
                {
                    _InstallationCosts = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        //COMPLEX PROPERTIES

        #region -ListOfGenerators- property
        private List<DtoWindGeneratorDevice> _ListOfGenerators;
        public  List<DtoWindGeneratorDevice> ListOfGenerators
        {
            get { return _ListOfGenerators; }
            set
            {
                if (_ListOfGenerators != value)
                {
                    _ListOfGenerators = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion
    }
}
