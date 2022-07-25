using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DuboisAnke_Project.Model;
using DuboisAnke_Project.View;
using DuboisAnke_Project.ViewModel;
using Newtonsoft.Json;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DuboisAnke_Project.Data
{
    public static class DbService
    {
        public static SQLiteAsyncConnection db;
        public static ObservableCollection<string> ProjectNameCollection;
        public static ObservableCollection<Project> AllProjectCollection = new ObservableCollection<Project>();
        public static ObservableCollection<Milestone> AllMilestoneCollection = new ObservableCollection<Milestone>();
        public static ObservableCollection<MilestoneViewModel> MilestoneViewModelCollection = new ObservableCollection<MilestoneViewModel>();
        public static ObservableCollection<Quote> Quotes = new ObservableCollection<Quote>();

        public static string DatabasePath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "DatabaseCMobileProjectDuboisAnke2022_4.db");
        public static async Task Init()
        {
            db = new SQLiteAsyncConnection(DatabasePath);
            await db.CreateTableAsync<Project>();
            await db.CreateTableAsync<Milestone>();
            FetchQuotes();
        }

        private static string APICallQuotes()
        {
            var URL = new UriBuilder($"https://type.fit/api/quotes");
            var client = new WebClient();
            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(URL.ToString());
        }

        private static void FetchQuotes()
        {
            string jsonQuotes = APICallQuotes();
            var deserializedQuotes = JsonConvert.DeserializeObject<List<Quote>>(jsonQuotes);

            if (deserializedQuotes != null)
            {
                var quoteCollection = new ObservableCollection<Quote>();
                foreach (var quote in deserializedQuotes)
                {
                    
                    quoteCollection.Add(quote);
                }

                Quotes = quoteCollection;

                
            }
            
        } 

        #region Project
        public static async Task AddProject(string name, DateTime startDate, DateTime endDate, string hexCode, string description, int projectid = 0)
        {
            await Init();

            var projects = await db.Table<Project>().ToListAsync();
            if (projects.Count == 0)
            {
                projectid = 1;
            }
            else
            {
                projectid = projects.Max(x => x.ProjectID) + 1;
            }

            Project project = new Project(name, startDate, endDate, hexCode, description,projectid);

            if (project.Name != null)
            {
                await db.InsertAsync(project);
            }

            await RetrieveAllProjects();
        }


        public static async Task RetrieveAllProjects()
        {
            AllProjectCollection = null;
            var projects = await db.Table<Project>().ToListAsync();

            var somecoll = new ObservableCollection<Project>();
            if (projects.Count > 0)
            {
                foreach (var item in projects)
                {
                    somecoll.Add(new Project(item.Name, item.StartDate, item.EndDate, item.ColourHex, item.Description, item.ProjectID));
                }
                AllProjectCollection = somecoll;
            }

            Application.Current.MainPage = new HomePage();
        }

        public static async Task RetrieveProjectNames()
        {
            ProjectNameCollection = null;
            var somecoll = new ObservableCollection<string>();
            if (AllProjectCollection.Count > 0)
            {
                foreach (var proj in AllProjectCollection)
                {
                    somecoll.Add(proj.Name);
                }
                ProjectNameCollection = somecoll;
            }
        }


        #endregion

        #region Milestone

        public static async Task AddMilestone(int projectId, string name, DateTime startDate, DateTime endDate, string hexCode, string description, int milestoneid = 0)
        {
            await Init();
            var milestones = await db.Table<Milestone>().ToListAsync();
            if (milestones.Count == 0)
            {
                milestoneid = 1;
            }
            else
            {
                milestoneid = milestones.Max(x => x.MilestoneID) + 1;
            }

            Milestone milestone = new Milestone(projectId, name, startDate, endDate, hexCode, description, milestoneid);

            await db.InsertAsync(milestone);

        }

        public static async Task RetrieveAllMilestones()
        {
            AllMilestoneCollection = null;
            var projects = await db.Table<Milestone>().ToListAsync();

            var somecoll = new ObservableCollection<Milestone>();
            if (projects.Count > 0)
            {
                foreach (var item in projects)
                {
                    somecoll.Add(new Milestone(item.ProjectID,item.Name,item.StartDate, item.EndDate,item.ColourHex,item.Description,item.MilestoneID));
                }
                AllMilestoneCollection = somecoll;
                foreach (var item2 in AllMilestoneCollection)
                {
                    MilestoneViewModelCollection.Add(new MilestoneViewModel
                    {
                        Name = item2.Name,
                        EndDate = item2.EndDate
                       
                    });
                }
            }

            Application.Current.MainPage = new HomePage();
        }

        #endregion

        /*public static async Task RemoveAllDb()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            var files = Directory.GetFiles(path);
            foreach (var item in files)
            {
                File.Delete(item);
            }
        }*/
    }
}
