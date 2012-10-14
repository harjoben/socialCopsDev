using System;
using System.Net;
using System.ComponentModel;

namespace ConsoleTest.Models
{
    public class subscriptionItem : INotifyPropertyChanged
    {
        #region subscriberId
        private int _subscriberId;
        public int subscriberId
        {
            get
            {
                return _subscriberId;
            }
            set
            {
                if (_subscriberId != value)
                {
                    _subscriberId = value;
                    NotifyPropertyChanged("subscriberId");
                }
            }
        }
        #endregion

        #region subscribedToId
        private int _subscribedToId;
        public int subscribedToId
        {
            get
            {
                return _subscribedToId;
            }
            set
            {
                if (_subscribedToId != value)
                {
                    _subscribedToId = value;
                    NotifyPropertyChanged("subscribedToId");
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

        public likeItem GetCopy()
        {
            likeItem copy = (likeItem)this.MemberwiseClone();
            return copy;
        }
    }
}