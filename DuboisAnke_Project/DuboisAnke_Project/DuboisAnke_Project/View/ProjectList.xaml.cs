using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuboisAnke_Project.Data;
using DuboisAnke_Project.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DuboisAnke_Project.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectList : ContentPage
    {
        public static ObservableCollection<Project> SortedProjectsCollection = new ObservableCollection<Project>();
        public ProjectList()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTM3ODU2QDMxMzkyZTMzMmUzMEdCQmRqTjJkalFxK0lvTHcrc09MSDBqVHg0N3N3QW94eE45YXJvTm04bkU9");

            LoadProjects();

        }

        
        private void LoadProjects()
        {
            //await DbService.RetrieveAllProjects();
            var projects = DbService.AllProjectCollection;
            SortedProjectsCollection.Clear();
            foreach (var project in projects.OrderBy(x => x.Progress))
            {
                SortedProjectsCollection.Add(project);
            }

            ProjectListView.ItemsSource = SortedProjectsCollection;
        }

        public void ProjectWasSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem != null)
            {
                Project selected = e.SelectedItem as Project;
                Application.Current.MainPage = new ProjectDetails(selected);
            }
            ProjectListView.SelectedItem = null;
        }


        private void NavigateToHome(object sender, EventArgs e)
        {
            try
            {
                Application.Current.MainPage = new HomePage();
            }
            catch (ObjectDisposedException)
            {

                DisplayAlert("Oops", "Something went wrong!", "Ok");

            }
        }

        private void BtnInfoClicked(object sender, EventArgs e)
        {
            DisplayAlert("INFO", "This page display all your projects, tap one to see it's details. Tapping the green indicator lets you finish a project, while tapping the red indicator will delete it.", "Ok");
        }
    }
}