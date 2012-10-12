using System;
using System.Net;
using System.ComponentModel;

namespace CoreService.Models
{
    public class userItem : INotifyPropertyChanged
    {
        #region userId
        private int _userId;
        public int userId
        {
            get
            {
                return _userId;
            }
            set
            {
                if (_userId != value)
                {
                    _userId = value;
                    NotifyPropertyChanged("userId");
                }
            }
        }
        #endregion

        #region userName
        private string _userName;
        public string userName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    NotifyPropertyChanged("userName");
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

        #region pwd
        private string _pwd;
        public string pwd
        {
            get
            {
                return _pwd;
            }
            set
            {
                if (_pwd != value)
                {
                    _pwd = value;
                    NotifyPropertyChanged("pwd");
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

        #region userAddress
        private string _userAddress;
        public string userAddress
        {
            get
            {
                return _userAddress;
            }
            set
            {
                if (_userAddress != value)
                {
                    _userAddress = value;
                    NotifyPropertyChanged("userAddress");
                }
            }
        }
        #endregion

        #region points
        private float _points;
        public float points
        {
            get
            {
                return _points;
            }
            set
            {
                if (_points != value)
                {
                    _points = value;
                    NotifyPropertyChanged("points");
                }
            }
        }
        #endregion

        #region userRank
        private string _userRank;
        public string userRank
        {
            get
            {
                return _userRank;
            }
            set
            {
                if (_userRank != value)
                {
                    _userRank = value;
                    NotifyPropertyChanged("phone");
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

        #region webUri
        private string _webUri;
        public string webUri
        {
            get
            {
                return _webUri;
            }
            set
            {
                if (_webUri != value)
                {
                    _webUri = value;
                    NotifyPropertyChanged("webUri");
                }
            }
        }
        #endregion

        #region phoneUri
        private string _phoneUri;
        public string phoneUri
        {
            get
            {
                return _phoneUri;
            }
            set
            {
                if (_phoneUri != value)
                {
                    _phoneUri = value;
                    NotifyPropertyChanged("phoneUri");
                }
            }
        }
        #endregion

        #region numComplaints
        private int _numComplaints;
        public int numComplaints
        {
            get
            {
                return _numComplaints;
            }
            set
            {
                if (_numComplaints != value)
                {
                    _numComplaints = value;
                    NotifyPropertyChanged("numComplaints");
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

        public userItem GetCopy()
        {
            userItem copy = (userItem)this.MemberwiseClone();
            return copy;
        }
    }
}