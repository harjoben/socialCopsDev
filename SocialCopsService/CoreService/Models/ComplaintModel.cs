using System;
using System.Net;
using System.ComponentModel;

namespace CoreService.Models
{
    public class complaintItem : INotifyPropertyChanged
    {
        #region complaintId
        private int _complaintId;
        public int complaintId
        {
            get
            {
                return _complaintId;
            }
            set
            {
                if (_complaintId != value)
                {
                    _complaintId = value;
                    NotifyPropertyChanged("complaintId");
                }
            }
        }
        #endregion

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

        #region title
        private string _title;
        public string title
        {
            get
            {
                return _title;
            }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    NotifyPropertyChanged("title");
                }
            }
        }
        #endregion

        #region details
        private string _details;
        public string details
        {
            get
            {
                return _details;
            }
            set
            {
                if (_details != value)
                {
                    _details = value;
                    NotifyPropertyChanged("details");
                }
            }
        }
        #endregion

        #region numLikes
        private int _numLikes;
        public int numLikes
        {
            get
            {
                return _numLikes;
            }
            set
            {
                if (_numLikes != value)
                {
                    _numLikes = value;
                    NotifyPropertyChanged("numLikes");
                }
            }
        }
        #endregion

        #region numComments
        private int _numComments;
        public int numComments
        {
            get
            {
                return _numComments;
            }
            set
            {
                if (_numComments != value)
                {
                    _numComments = value;
                    NotifyPropertyChanged("numComments");
                }
            }
        }
        #endregion

        #region picture
        private string _picture;
        public string picture
        {
            get
            {
                return _picture;
            }
            set
            {
                if (_picture != value)
                {
                    _picture = value;
                    NotifyPropertyChanged("picture");
                }
            }
        }
        #endregion

        #region complaintDate
        private DateTime _complaintDate;
        public DateTime complaintDate
        {
            get
            {
                return _complaintDate;
            }
            set
            {
                if (_complaintDate != value)
                {
                    _complaintDate = value;
                    NotifyPropertyChanged("complaintDate");
                }
            }
        }
        #endregion

        #region location
        private string _location;
        public string location
        {
            get
            {
                return _location;
            }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    NotifyPropertyChanged("location");
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

        #region category
        private string _category;
        public string category
        {
            get
            {
                return _category;
            }
            set
            {
                if (_category != value)
                {
                    _category = value;
                    NotifyPropertyChanged("category");
                }
            }
        }
        #endregion

        #region complaintStatus
        private string _complaintStatus;
        public string complaintStatus
        {
            get
            {
                return _complaintStatus;
            }
            set
            {
                if (_complaintStatus != value)
                {
                    _complaintStatus = value;
                    NotifyPropertyChanged("complaintStatus");
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

        #region isAnonymous
        private int _isAnonymous;
        public int isAnonymous
        {
            get
            {
                return _isAnonymous;
            }
            set
            {
                if (_isAnonymous != value)
                {
                    _isAnonymous = value;
                    NotifyPropertyChanged("isAnonymous");
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

        #region thumbImage1
        private string _thumbImage1;
        public string thumbImage1
        {
            get
            {
                return _thumbImage1;
            }
            set
            {
                if (_thumbImage1 != value)
                {
                    _thumbImage1 = value;
                    NotifyPropertyChanged("thumbImage1");
                }
            }
        }
        #endregion

        #region thumbImage2
        private string _thumbImage2;
        public string thumbImage2
        {
            get
            {
                return _thumbImage2;
            }
            set
            {
                if (_thumbImage2 != value)
                {
                    _thumbImage2 = value;
                    NotifyPropertyChanged("thumbImage2");
                }
            }
        }
        #endregion

        #region ImageByte
        private byte[] _ImageByte;
        public byte[] ImageByte
        {
            get
            {
                return _ImageByte;
            }
            set
            {
                if (_ImageByte != value)
                {
                    _ImageByte = value;
                    NotifyPropertyChanged("ImageByte");
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

        public complaintItem GetCopy()
        {
            complaintItem copy = (complaintItem)this.MemberwiseClone();
            return copy;
        }
    }
}