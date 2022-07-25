using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuboisAnke_Project.Data;
using Firebase.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DuboisAnke_Project.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        public FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(Auth.APIkey));
        public Register()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTM3ODU2QDMxMzkyZTMzMmUzMEdCQmRqTjJkalFxK0lvTHcrc09MSDBqVHg0N3N3QW94eE45YXJvTm04bkU9");
        }

        private async void RegisterUser(object sender, EventArgs e)
        {
            var user = await authProvider.CreateUserWithEmailAndPasswordAsync(eMailEntry.Text, passWordEntry.Text);
            await authProvider.SendEmailVerificationAsync(user.FirebaseToken);
            await DisplayAlert("REGISTRATION", "You've been succesfully registered", "OK");
            
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

        private void NavigateToLogin(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Login();
        }
    }
}