using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DuboisAnke_Project.Data;
using DuboisAnke_Project.View;
using SQLite;
using Xamarin.Forms;

namespace DuboisAnke_Project.Model
{
    public class Project
    {
        [PrimaryKey]
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public string ColourHex { get; set; }
        public string Description { get; set; }
        public double Progress { get; set; }

        public static double CalculateProgress(DateTime startDate, DateTime endDate)
        {

            double progress = (endDate - startDate).Days;
            progress /= 365;
            progress *= 100;

            if (progress > 0)
            {
                return Math.Floor(progress);
            } else
            {
                progress = 0;
                return progress;
            }
            
        }


        public Project(string name, DateTime startDate, DateTime endDate, string colourHex, string description, int projectid = 0)
        {
            ProjectID = projectid;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            ColourHex = colourHex;
            Description = description;
            Progress = CalculateProgress(startDate, endDate);
        }

        public Project()
        {

        }

        private Command _removeproject;
        public ICommand RemoveProject
        {
            get
            {
                _removeproject = new Command(RemoveProjectFromDb);

                return _removeproject;
            }
        }

        private async void RemoveProjectFromDb()
        {
            var answer = await App.Current.MainPage.DisplayAlert("DELETE", "Have all your milestones been finished!", "Yes", "No");
            
            if(answer)
            {
                var selectedProject = DbService.AllProjectCollection.FirstOrDefault(x => x.ProjectID == ProjectID);
                await DbService.db.DeleteAsync<Project>(selectedProject.ProjectID);

                
                await DbService.RetrieveAllProjects();
            }
            
        }

        private Command _projectfinished;
        public ICommand ProjectFinished
        {
            get
            {
                _projectfinished = new Command(RemoveFinishedProjectFromDb);

                return _projectfinished;
            }
        }

        private async void RemoveFinishedProjectFromDb()
        {
            await App.Current.MainPage.DisplayAlert("FINISHED!", "Congratulations for finishing this project!", "Thanks!");
            var selectedProject = DbService.AllProjectCollection.FirstOrDefault(x => x.ProjectID == ProjectID);
            await DbService.db.DeleteAsync<Project>(selectedProject.ProjectID);
            await DbService.RetrieveAllProjects();
        }
    }
}
