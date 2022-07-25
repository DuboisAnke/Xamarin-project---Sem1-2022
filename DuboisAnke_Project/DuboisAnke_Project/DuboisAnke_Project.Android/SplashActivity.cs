using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuboisAnke_Project.Droid
{
    [Activity(Theme = "@style/Theme.Splash",
        MainLauncher = false,
        NoHistory = true,
        Icon = "@drawable/splashscrn")]
    public class SplashActivity : Activity
    {
        protected override async void OnResume()
        {
            base.OnResume();
            await SimulateStartup();
        }

        async Task SimulateStartup()
        {
            await Task.Delay(TimeSpan.FromSeconds(10));
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}