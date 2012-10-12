using System;
using System.Net;
using System.ComponentModel;

namespace CoreService.Models
{
    public class authorityItem : INotifyPropertyChanged
    {
        #region authId
        private int _authId;
        public int authId
        {
            get
            {
                return _authId;
            }
            set
            {
                if (_authId != value)
                {
                    _authId = value;
                    NotifyPropertyChanged("authId");
                }
            }
        }
        #endregion

        #region authName
        private string _authName;
        public string authName
        {
            get
            {
                return _authName;
            }
            set
            {
                if (_authName != value)
                {
                    _authName = value;
                    NotifyPropertyChanged("authName");
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

        #region authAddress
        private string _authAddress;
        public string authAddress
        {
            get
            {
                return _authAddress;
            }
            set
            {
                if (_authAddress != value)
                {
                    _authAddress = value;
                    NotifyPropertyChanged("authAddress");
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

        #region numPending
        private int _numPending;
        public int numPending
        {
            get
            {
                return _numPending;
            }
            set
            {
                if (_numPending != value)
                {
                    _numPending = value;
                    NotifyPropertyChanged("numPending");
                }
            }
        }
        #endregion

        #region numResolved
        private int _numResolved;
        public int numResolved
        {
            get
            {
                return _numResolved;
            }
            set
            {
                if (_numResolved != value)
                {
                    _numResolved = value;
                    NotifyPropertyChanged("numResolved");
                }
            }
        }
        #endregion

        #region wesite
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

        #region profilePic
        private string _profilePic;
        public string profilePic
        {
            get
            {
                return _profilePic;
            }
            set
            {
                if (_profilePic != value)
                {
                    _profilePic = value;
                    NotifyPropertyChanged("profilePic");
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

        public authorityItem GetCopy()
        {
            authorityItem copy = (authorityItem)this.MemberwiseClone();
            return copy;
        }
    }
}