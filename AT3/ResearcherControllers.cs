using System;
using System.Collections.Generic;
using System.Linq;

namespace AT3
{
    public class ResearcherController
    {
        public List<Researcher> researchers { get; }

        public ResearcherController()
        {
            researchers = new List<Researcher>();
        }

        public void addResearcher(Researcher researcher)
        {
            researchers.Add(researcher);
        }

        public void removeResearcher(Researcher researcher)
        {
            researchers.Remove(researcher);
        }

        // show only those staff whose give name or family name contains the text entered by the user
        public List<Researcher> filterByName(string name)
        {
            return researchers.Where(re => re.Name.Contains(name)).ToList();
        }

        // filter the researchers by position
        public List<Researcher> filterByLevel(EmployeeLevel level)
        {
            return researchers.Where(re => re.Level == level).ToList();
        }

        public static void DisplayResearcherDetails(Researcher researcher)
        {
            Console.WriteLine(researcher.Name);
            Console.WriteLine(researcher.Title);
            Console.WriteLine(researcher.School);
            Console.WriteLine(researcher.Unit);
            Console.WriteLine(researcher.Campus);
            Console.WriteLine(researcher.Email);
            Console.WriteLine(researcher.Level);
            Console.WriteLine(researcher.commenceWithInstitute);
            Console.WriteLine(researcher.commenceCurrentPosition);
            Console.WriteLine(researcher.Tenure);
            Console.WriteLine(researcher.Publications);
            Console.WriteLine(researcher.Q1Percentage);
            Console.WriteLine(researcher.ThreeYearAverage);
            Console.WriteLine(researcher.Funding);
            Console.WriteLine(researcher.performancePublication);
            Console.WriteLine(researcher.Supervisions);
            Console.WriteLine(researcher.Degree);
        }

    }
}
