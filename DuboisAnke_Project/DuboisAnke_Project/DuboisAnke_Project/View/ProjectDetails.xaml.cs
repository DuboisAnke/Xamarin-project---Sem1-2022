using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuboisAnke_Project.Data;
using DuboisAnke_Project.Model;
using DuboisAnke_Project.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DuboisAnke_Project.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectDetails : ContentPage
    {
        private Project _project;
        public ObservableCollection<Milestone> Milestones { get; set; } =
            new ObservableCollection<Milestone>();
        public ObservableCollection<Milestone> SortedMilestones { get; set; } =
            new ObservableCollection<Milestone>();
        public ProjectDetails(Project project)
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTM3ODU2QDMxMzkyZTMzMmUzMEdCQmRqTjJkalFxK0lvTHcrc09MSDBqVHg0N3N3QW94eE45YXJvTm04bkU9");

            this.BindingContext = project;
            _project = project;

            
            LoadMileStones();
        }

        private async void LoadMileStones()
        {
            List<Milestone> milestones = new List<Milestone>();
            milestones.Clear();

            milestones = await DbService.db.Table<Milestone>().ToListAsync();

            DbService.MilestoneViewModelCollection.Clear();
            foreach (var item in milestones)
            {
                if (item.ProjectID == _project.ProjectID)
                {
                  
                    DbService.MilestoneViewModelCollection.Add(new MilestoneViewModel
                    {
                        MilestoneID = item.MilestoneID,
                        Name = item.Name,
                        EndDate = item.EndDate
                    });
                }
            }

            if (DbService.MilestoneViewModelCollection.Count > 0)
            {
                MilestonesListView.IsVisible = true;
                lblAreThereMilestones.Text = "YOUR MILESTONES FOR THIS PROJECT :";
                btnSortMilestones.IsVisible = true;
                if (MilestonesListView.ItemsSource == null)
                {

                    MilestonesListView.ItemsSource = DbService.MilestoneViewModelCollection;
                }   
            } else
            {
                MilestonesListView.IsVisible = false;
                lblAreThereMilestones.Text = "YOU DON'T HAVE ANY MILESTONES YET!";
                btnSortMilestones.IsVisible = false;
            }
        }

        public async void MilestoneWasSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem != null)
            {

                MilestoneViewModel selected = e.SelectedItem as MilestoneViewModel;

                var milestones = await DbService.db.Table<Milestone>().ToListAsync();
                if (selected != null)
                {
                    var milestone = milestones.FirstOrDefault(x => x.MilestoneID == selected.MilestoneID);
                    Application.Current.MainPage = new MilestoneDetails(milestone);
                }
                

                
            }
            MilestonesListView.SelectedItem = null;
        }

        private void SortMilestones(object sender, EventArgs e)
        {
           MilestonesListView.ItemsSource = null;
           MilestonesListView.ItemsSource = DbService.MilestoneViewModelCollection.OrderBy(x => x.EndDate.Date);

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