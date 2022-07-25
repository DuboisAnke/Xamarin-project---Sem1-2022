using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuboisAnke_Project.Data;
using Syncfusion.SfRadialMenu.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DuboisAnke_Project.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class MilestoneAdd : ContentPage
    {
        private string hexcode;
        public MilestoneAdd()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTM3ODU2QDMxMzkyZTMzMmUzMEdCQmRqTjJkalFxK0lvTHcrc09MSDBqVHg0N3N3QW94eE45YXJvTm04bkU9");
            LoadProjectNames();

            
        }

        private async void LoadProjectNames()
        {
            await DbService.RetrieveProjectNames();
            if (projectPicker.ItemsSource == null)
            {
                projectPicker.ItemsSource = DbService.ProjectNameCollection;
            }
        }
        private async void AddMilestone(object sender, EventArgs e)
        {
            if (projectPicker.SelectedItem != null)
            {
                var projectName = projectPicker.SelectedItem.ToString();
                var selectedProject = DbService.AllProjectCollection.FirstOrDefault(x => x.Name == projectName);

                await DbService.AddMilestone(selectedProject.ProjectID, milestoneNameEntry.Text, startDateEntry.Date, endDateEntry.Date, hexcode,
                    shortDescriptionEntry.Text);
            }

            await DisplayAlert("", "Milestone has been added", "OK");
            Application.Current.MainPage = new HomePage();
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

        private void RadialMenuItemWasTapped(object sender, Syncfusion.SfRadialMenu.XForms.ItemTappedEventArgs e)
        {
            hexCodeSelected.CenterButtonBackgroundColor = (sender as SfRadialMenuItem).BackgroundColor;

            hexcode = (sender as SfRadialMenuItem).BackgroundColor.ToHex();
        }

        private void ProjectWasSelected(object sender, EventArgs e)
        {
            if (projectPicker.SelectedItem != null)
            {
                var projectName = projectPicker.SelectedItem.ToString();
                var selectedProject = DbService.AllProjectCollection.FirstOrDefault(x => x.Name == projectName);

                startDateEntry.MinimumDate = selectedProject.StartDate;
                startDateEntry.MaximumDate = selectedProject.EndDate;
                startDateEntry.Date = DateTime.Now;

                endDateEntry.MinimumDate = DateTime.Now;
                endDateEntry.MaximumDate = selectedProject.EndDate;
                endDateEntry.Date = DateTime.Now;
            }
            else
            {
                startDateEntry.MinimumDate = DateTime.Now;
                startDateEntry.Date = DateTime.Now;

                endDateEntry.MinimumDate = DateTime.Now;
                endDateEntry.Date = DateTime.Now;
            }
        }

        private async void BtnInfoClicked(object sender, EventArgs e)
        {
            await DisplayAlert("INFO", "Press the pink circle in the middle to bring up the colorwheel!", "Understood!");
        }

        
    }
}