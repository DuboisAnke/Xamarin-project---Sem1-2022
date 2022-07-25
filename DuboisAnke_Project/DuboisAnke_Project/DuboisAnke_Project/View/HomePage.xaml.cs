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
    public partial class HomePage : FlyoutPage
    {


        public HomePage()
        {

            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTM3ODU2QDMxMzkyZTMzMmUzMEdCQmRqTjJkalFxK0lvTHcrc09MSDBqVHg0N3N3QW94eE45YXJvTm04bkU9");


            flyout.listview.ItemSelected += OnSelectedItem;


        }

        private void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = e.SelectedItem as FlyoutItemPage;
                if (item != null)
                {
                    Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetPage));
                    flyout.listview.SelectedItem = null;
                    IsPresented = false;
                }
            }

            catch (ObjectDisposedException)
            {

                App.Current.MainPage.DisplayAlert("Oops", "Something went wrong!", "Ok");
                

            }




        }
    }
}