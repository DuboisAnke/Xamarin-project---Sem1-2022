using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuboisAnke_Project.Data;
using Firebase.Auth;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DuboisAnke_Project.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(Auth.APIkey));
        // 
        public Login()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTM3ODU2QDMxMzkyZTMzMmUzMEdCQmRqTjJkalFxK0lvTHcrc09MSDBqVHg0N3N3QW94eE45YXJvTm04bkU9");
        }


        private async void LoginUser(object sender, EventArgs e)
        {
            try
            {
                var user = await authProvider.SignInWithEmailAndPasswordAsync(emailEntry.Text, passwordEntry.Text);
                var content = await user.GetFreshAuthAsync();
                var jsonContent = JsonConvert.SerializeObject(content);
                Preferences.Set("MyFirebaseRefreshToken", jsonContent);
                await DisplayAlert("LOGIN", "You've been succesfully logged in", "OK");
                Application.Current.MainPage = new HomePage();
            }
            catch (FirebaseAuthException exception)
            {
                await DisplayAlert("Error", exception.Message, "OK");
            }
           
        }

        private void NavigateToRegister(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Register();
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