﻿using System;
using System.Net;
using System.ComponentModel;

namespace ConsoleTest.Models
{
    public class commentItem: INotifyPropertyChanged
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

        #region comment
        private string _comment;
        public string comment
        {
            get
            {
                return _comment;
            }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    NotifyPropertyChanged("comment");
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

        public commentItem GetCopy()
        {
            commentItem copy = (commentItem)this.MemberwiseClone();
            return copy;
        }
    }
}