using System;
using System.Net;
using System.ComponentModel;

namespace CoreService.Models
{
    public class muteAuthorityItem : INotifyPropertyChanged
    {
        #region muteAuthId
        private int _muteAuthId;
        public int muteAuthId
        {
            get
            {
                return _muteAuthId;
            }
            set
            {
                if (_muteAuthId != value)
                {
                    _muteAuthId = value;
                    NotifyPropertyChanged("muteAuthId");
                }
            }
        }
        #endregion

        #region muteAuthName
        private string _muteAuthName;
        public string muteAuthName
        {
            get
            {
                return _muteAuthName;
            }
            set
            {
                if (_muteAuthName != value)
                {
                    _muteAuthName = value;
                    NotifyPropertyChanged("muteAuthName");
                }
            }
        }
        #endregion

        #region MuteAuthAddress
        private string _MuteAuthAddress;
        public string MuteAuthAddress
        {
            get
            {
                return _MuteAuthAddress;
            }
            set
            {
                if (_MuteAuthAddress != value)
                {
                    _MuteAuthAddress = value;
                    NotifyPropertyChanged("MuteAuthAddress");
                }
            }
        }
        #endregion

        #region city
        private string _city;
        public string city
        {
            get
            {
                return _city;
            }
            set
            {
                if (_city != value)
                {
                    _city = value;
                    NotifyPropertyChanged("city");
                }
            }
        }
        #endregion

        #region state
        private string _state;
        public string state
        {
            get
            {
                return _state;
            }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    NotifyPropertyChanged("state");
                }
            }
        }
        #endregion

        #region country
        private string _country;
        public string country
        {
            get
            {
                return _country;
            }
            set
            {
                if (_country != value)
                {
                    _country = value;
                    NotifyPropertyChanged("country");
                }
            }
        }
        #endregion

        #region email
        private string _email;
        public string email
        {
            get
            {
                return _email;
            }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    NotifyPropertyChanged("email");
                }
            }
        }
        #endregion

        #region phone
        private int _phone;
        public int phone
        {
            get
            {
                return _phone;
            }
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    NotifyPropertyChanged("phone");
                }
            }
        }
        #endregion

        #region website
        private string _website;
        public string website
        {
            get
            {
                return _website;
            }
            set
            {
                if (_website != value)
                {
                    _website = value;
                    NotifyPropertyChanged("website");
                }
            }
        }
        #endregion

        #region pic
        private string _pic;
        public string pic
        {
            get
            {
                return _pic;
            }
            set
            {
                if (_pic != value)
                {
                    _pic = value;
                    NotifyPropertyChanged("pic");
                }
            }
        }
        #endregion

        #region latitude
        private float _latitude;
        public float latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                if (_latitude != value)
                {
                    _latitude = value;
                    NotifyPropertyChanged("latitude");
                }
            }
        }
        #endregion

        #region longitude
        private float _longitude;
        public float longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                if (_longitude != value)
                {
                    _longitude = value;
                    NotifyPropertyChanged("longitude");
                }
            }
        }
        #endregion

        #region flag
        private int _flag;
        public int flag
        {
            get
            {
                return _flag;
            }
            set
            {
                if (_flag != value)
                {
                    _flag = value;
                    NotifyPropertyChanged("flag");
                }
            }
        }
        #endregion

        #region date
        private DateTime _date;
        public DateTime date
        {
            get
            {
                return _date;
            }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    NotifyPropertyChanged("date");
                }
            }
        }
        #endregion

        #region pincode
        private string _pincode;
        public string pincode
        {
            get
            {
                return _pincode;
            }
            set
            {
                if (_pincode != value)
                {
                    _pincode = value;
                    NotifyPropertyChanged("pincode");
                }
            }
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        public muteAuthorityItem GetCopy()
        {
            muteAuthorityItem copy = (muteAuthorityItem)this.MemberwiseClone();
            return copy;
        }
    }
}