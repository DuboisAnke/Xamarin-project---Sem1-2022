using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DuboisAnke_Project.Model;

namespace DuboisAnke_Project.Data
{
    public class SeedData
    {
        public static async Task EnsurePopulatedSeedData()
        {
            var projectsList = await DbService.db.Table<Project>().ToListAsync();
            if (projectsList.Count == 0)
            {
                List<Project> projects = new List<Project>
                {
                    new Project("C#Mobile", new DateTime(2021, 10, 14),
                        new DateTime(2022, 02, 14), "#9B2226",
                        "Write a mobile application in Xamarin.Forms for C# Mobile"),
                    new Project("C#Web", new DateTime(2021, 7, 26),
                        new DateTime(2022, 02, 14), "#AE2012",
                        "A MVC-project for C#Web")
                };
                foreach (var project in projects)
                {
                    await DbService.AddProject(project.Name, project.StartDate, project.EndDate, project.ColourHex,
                        project.Description);
                }

                List<Milestone> milestones = new List<Milestone>
                {
                    new Milestone(1,"Styles afwerken", new DateTime(2021, 11, 23),
                        new DateTime(2022, 02, 14), "#BB3E03",
                        "Finish up some of the styling for the homepage etc."),
                    new Milestone(1,"Update readme", new DateTime(2022, 1, 23),
                        new DateTime(2022, 02, 7), "##0A9396",
                        "Finish up some of the styling for the homepage etc."),
                    new Milestone(2,"Presentation", new DateTime(2022, 1, 23),
                        new DateTime(2022, 02, 14), "#005F73",
                        "Need to give a presentation for C#Web!")
                };
                foreach (var milestone in milestones)
                {
                    await DbService.AddMilestone(milestone.ProjectID, milestone.Name, milestone.StartDate, milestone.EndDate, milestone.ColourHex,
                        milestone.Description);
                }
            }

            await DbService.RetrieveAllProjects();
        }
    }
}
