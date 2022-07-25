using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuboisAnke_Project.Data;
using DuboisAnke_Project.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DuboisAnke_Project.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Welcome : ContentPage
    {
        public static ObservableCollection<Project> ProjectsCollection = new ObservableCollection<Project>();
        public static ObservableCollection<Project> SelectedProject = new ObservableCollection<Project>();

        

        public Welcome()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTM3ODU2QDMxMzkyZTMzMmUzMEdCQmRqTjJkalFxK0lvTHcrc09MSDBqVHg0N3N3QW94eE45YXJvTm04bkU9");
            LoadQuote();
            GetUsername();
            LoadProject();
     
            
        }

        private void GetUsername()
        {
            
            var username = Preferences.Get("username", string.Empty);

            if (username == string.Empty)
            {
                lblUsername.Text = "Hey!";
            } else
            {
                lblUsername.Text = "Hey there," + " " + username + "!";
            }
            
        }

        private void LoadQuote()
        {
            var quotes = DbService.Quotes;

            Random rnd = new Random();
            int rndIndex = rnd.Next(0, quotes.Count);

            lblQuote.Text = "'" + quotes[rndIndex].Text + "'";
        }

        private void LoadProject()
        {
            var projects = DbService.AllProjectCollection;
            ProjectsCollection.Clear();
            foreach (var item in projects.OrderBy(x => x.Progress))
            {
                ProjectsCollection.Add(item);
            }
            
            SelectedProject.Clear();
            SelectedProject.Add(ProjectsCollection[0]);

            SelectedProjectList.ItemsSource = SelectedProject;

        }

        private void ProjectWasSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Project selected = e.SelectedItem as Project;
                Application.Current.MainPage = new ProjectDetails(selected);
            }
            SelectedProjectList.SelectedItem = null;
        }
    }
}