using System;
using System.Net;
using System.ComponentModel;

namespace ConsoleTest.Models
{
    public class jurisdictionItem : INotifyPropertyChanged
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

        public jurisdictionItem GetCopy()
        {
            jurisdictionItem copy = (jurisdictionItem)this.MemberwiseClone();
            return copy;
        }
    }
}