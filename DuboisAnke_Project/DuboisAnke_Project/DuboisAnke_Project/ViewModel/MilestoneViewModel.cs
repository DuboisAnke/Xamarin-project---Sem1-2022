using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DuboisAnke_Project.ViewModel
{
    public class MilestoneViewModel : INotifyPropertyChanged
    {
        private int _milestoneId;
        private string _name;
        private DateTime _endDate;

        public int MilestoneID {
            get => _milestoneId;
            set
            {
                _milestoneId = value;
                OnPropertyChanged(nameof(MilestoneID));
            }
        }

       public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
