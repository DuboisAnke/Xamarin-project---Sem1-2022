using DuboisAnke_Project.Data;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DuboisAnke_Project.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutMenu : ContentPage
    {
        
        public FlyoutMenu()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTM3ODU2QDMxMzkyZTMzMmUzMEdCQmRqTjJkalFxK0lvTHcrc09MSDBqVHg0N3N3QW94eE45YXJvTm04bkU9");

            var isUserLoggedIn = Preferences.Get("MyFirebaseRefreshToken", string.Empty);
            if (isUserLoggedIn == string.Empty)
            {
                lblLoggedinUser.Text = "You're currently not logged in";
                btnLogout.IsVisible = false;
            }
            else
            {
                lblLoggedinUser.Text = "You're currently logged in";
                btnLogout.IsVisible = true;
            }
        }

        private void btnLogoutClicked(object sender, EventArgs e)
        {
            DisplayAlert("LOGOUT", "You've succesfully logged out", "Ok");
            Preferences.Set("MyFirebaseRefreshToken", string.Empty);
            Application.Current.MainPage = new Login();
        }
    }
}