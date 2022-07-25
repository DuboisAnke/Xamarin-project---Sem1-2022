using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class Settings : ContentPage
    {
        public FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(Auth.APIkey));
        public Settings()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTM3ODU2QDMxMzkyZTMzMmUzMEdCQmRqTjJkalFxK0lvTHcrc09MSDBqVHg0N3N3QW94eE45YXJvTm04bkU9");

        }

        private async void BtnChangePassword_OnClicked(object sender, EventArgs e)
        {
            bool credentialsValidated = true;
            try
            {
                var user = await authProvider.SignInWithEmailAndPasswordAsync(emailEntry.Text, oldPasswordEntry.Text);
                var content = await user.GetFreshAuthAsync();
                var jsonContent = JsonConvert.SerializeObject(content);
                Preferences.Set("MyFirebaseRefreshToken", jsonContent);
                
            }
            catch (FirebaseAuthException ex)
            {
                credentialsValidated = false;
                await DisplayAlert("Wrong password", "The password you entered is incorrect!", "OK");
                Debug.WriteLine(ex.Reason);
            }

            if (credentialsValidated)
            {
                var oldAuth = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                var newAuth = await authProvider.RefreshAuthAsync(oldAuth);
                await authProvider.ChangeUserPassword(newAuth.FirebaseToken, newPasswordEntry.Text);
                await DisplayAlert("Password Changed", "Your password has been succesfully changed", "OK");
                oldPasswordEntry.Text = string.Empty;
                newPasswordEntry.Text = string.Empty;
            }
        }

        private async void SetUsernamePreference(object sender, EventArgs e)
        {
            Preferences.Set("username", userNameEntry.Text);
            await DisplayAlert("Username set", "You've successfully set your username", "OK");
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

        private async void ClearUsernamePreference(object sender, EventArgs e)
        {

            Preferences.Set("username", string.Empty);
            await DisplayAlert("Username cleared", "You've successfully cleared your username", "OK");
        }
    }
}