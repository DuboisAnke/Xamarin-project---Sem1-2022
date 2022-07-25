using System;
using System.Diagnostics;
using System.IO;
using DuboisAnke_Project.Data;
using DuboisAnke_Project.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


[assembly: ExportFont("zazasp.ttf", Alias = "zazasp")]
[assembly: ExportFont("ShadowsIntoLights-Regular.ttf", Alias = "ShadowsIntoLights-Regular")]
[assembly: ExportFont("212BabyGirl.otf", Alias = "212BabyGirl")]


namespace DuboisAnke_Project
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage();
        }

        protected async override void OnStart()
        {
            //await DbService.RemoveAllDb();

            await DbService.Init();
            await SeedData.EnsurePopulatedSeedData();
            MainPage = new HomePage();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
