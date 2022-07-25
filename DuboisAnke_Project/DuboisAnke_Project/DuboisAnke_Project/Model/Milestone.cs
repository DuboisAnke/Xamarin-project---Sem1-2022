using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DuboisAnke_Project.Data;
using SQLite;
using Xamarin.Forms;

namespace DuboisAnke_Project.Model
{
    public class Milestone
    {
        [PrimaryKey]
        public int MilestoneID { get; set; }
        
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public string ColourHex { get; set; }

        public string Description { get; set; }

        public double Progress { get; set; }

        public double CalculateProgress(DateTime startDate, DateTime endDate)
        {

            double progress = (endDate - startDate).Days;
            progress /= 365;
            progress *= 100;

            if (progress > 0)
            {
                return Math.Floor(progress);
            }
            else
            {
                progress = 0;
                return progress;
            }
        }


        public Milestone( int projectId, string name, DateTime startDate, DateTime endDate, string colourHex, string description, int milestoneId = 0)
        {
            MilestoneID = milestoneId;
            ProjectID = projectId;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            ColourHex = colourHex;
            Description = description;

            Progress = CalculateProgress(startDate, endDate);
        }

        public Milestone()
        {
                
        }


        private Command _removemilestone;
        public ICommand RemoveMilestone
        {
            get
            {
                _removemilestone = new Command(RemoveMilestoneFromDb);

                return _removemilestone;
            }
        }

        private async void RemoveMilestoneFromDb()
        {
            
            await DbService.RetrieveAllMilestones();
            var selectedMilestone = DbService.AllMilestoneCollection.FirstOrDefault(x => x.MilestoneID == MilestoneID);

            var answer = await App.Current.MainPage.DisplayAlert("DELETE", "Are you sure you want to delete this milestone?", "Yes", "No");
            if (answer)
            {

                if (selectedMilestone != null)
                {
                    await DbService.db.DeleteAsync<Milestone>(selectedMilestone.MilestoneID);

                }
            }
        }

        private Command _milestonefinished;
        public ICommand MilestoneFinished
        {
            get
            {
                _milestonefinished = new Command(RemoveFinishedMilestoneFromDb);

                return _milestonefinished;
            }
        }

        private async void RemoveFinishedMilestoneFromDb()
        {

            await DbService.RetrieveAllMilestones();
            var selectedMilestone = DbService.AllMilestoneCollection.FirstOrDefault(x => x.MilestoneID == MilestoneID);

            await App.Current.MainPage.DisplayAlert("FINISHED!", "Congratulations on finishing this milestone!", "Thanks!");
  
            if (selectedMilestone != null)
            {
                await DbService.db.DeleteAsync<Milestone>(selectedMilestone.MilestoneID);

            }
            
        }
    }
}
