using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuboisAnke_Project.Data;
using DuboisAnke_Project.Model;
using Syncfusion.SfRadialMenu.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DuboisAnke_Project.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectAdd : ContentPage
    {
        private string hexcode;
        public ProjectAdd()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTM3ODU2QDMxMzkyZTMzMmUzMEdCQmRqTjJkalFxK0lvTHcrc09MSDBqVHg0N3N3QW94eE45YXJvTm04bkU9");

            startDateEntry.MinimumDate = DateTime.Now;
            startDateEntry.Date = DateTime.Now;

            endDateEntry.MinimumDate = DateTime.Now;
            endDateEntry.Date = DateTime.Now;
           
        }  

        private async void AddProject(object sender, EventArgs e)
        {
            await DbService.AddProject(projectNameEntry.Text, startDateEntry.Date, endDateEntry.Date, hexcode,
                shortDescriptionEntry.Text);
            await Application.Current.MainPage.DisplayAlert("Alert", "Project has been added", "OK");
        }

        private void RadialMenuItemWasTapped(object sender, Syncfusion.SfRadialMenu.XForms.ItemTappedEventArgs e)
        {
            hexCodeSelected.CenterButtonBackgroundColor = (sender as SfRadialMenuItem).BackgroundColor;

            hexcode = (sender as SfRadialMenuItem).BackgroundColor.ToHex();

            

        }

        private async void BtnInfoClicked(object sender, EventArgs e)
        {
            await DisplayAlert("INFO", "Press the pink circle in the middle to bring up the colorwheel!", "Understood!");
        }

        private void NavigateToHome(object sender, EventArgs e)
        {
            try
            {
                Application.Current.MainPage = new HomePage();
            }
            catch (ObjectDisposedException)
            {

                DisplayAlert("Oops", "Something went wrong, we're rerouting you to the login page!", "Ok");
                Application.Current.MainPage = new Login();

            }
        }
    }
}