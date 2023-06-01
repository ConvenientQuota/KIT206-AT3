using AT3.DataSources;
using System;
using AT3.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows;

namespace AT3.Controllers
{
    public class ResearcherController
    {
        public List<Researcher> researchers { get; }
        public static List<Researcher> researcher { get; set; }

        private static List<Researcher> tempResearcherList;

        public ResearcherController()
        {
            researchers = new List<Researcher>();
        }

        //Enables the ability to add Researchers to database
        public void addResearcher(Researcher researcher)
        {
            researchers.Add(researcher);
        }

        //Enables the ability to remove Researchers from database
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

        public static List<Researcher> FilterByLevel(EmployeeLevel level)
        {
            return researcher.Where(re => re.Level == level).ToList();
        }

        //Displays the selected Researcher's details e

        public static void DisplayResearcherDetails(Researcher researcher)
        {
            Console.WriteLine(researcher.Name);
            Console.WriteLine(researcher.Title);
            Console.WriteLine(researcher.School);
            Console.WriteLine(researcher.Unit);
            Console.WriteLine(researcher.campus);
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

        /*
         * LINQ equivalent
         */

        /*   public LINQPublicationControllers()
        {
            publications = new List<Publication>();
        } */

        public void LinqAddResearcher(Researcher researcher)
        {
            researchers.Add(researcher);
        }

        //Enables the ability to remove Researchers from database
        public void LinqRemoveResearcher(Researcher researcher)
        {
            researchers.Remove(researcher);
        }

        // show only those staff whose give name or family name contains the text entered by the user
        public List<Researcher> LinqFilterByName(string name)
        {
            return (
                from re in researchers
                where re.Name.Contains(name)
                select re).ToList();
        }

        // filter the researchers by position
        public List<Researcher> LinqFilterByLevel(EmployeeLevel level)
        {
            return (
                from re in researchers
                where re.Level == level
                select re).ToList();
        }

        public static List<Researcher> FilterByType(bool isStudent, ObservableCollection<Researcher> researchers)
        {
            List<Researcher> tempResearcherList;
            if (isStudent)
            {             
                var filtered = from Researcher re in researchers
                               where re.Level == EmployeeLevel.Student
                               select re;
                tempResearcherList = filtered.ToList();
            }
            else
            {
                var filtered = from Researcher re in researchers
                               where re.Level != EmployeeLevel.Student
                               select re;
                tempResearcherList = filtered.ToList();
            }
            return tempResearcherList;
        }

        public static List<Researcher> awilterByType(EmployeeLevel level, ObservableCollection<Researcher> researchers)
        {
            List<Researcher> tempList;
            if (level == EmployeeLevel.A)
            {
                var filtered = from Researcher re in researchers
                               where re.Level == EmployeeLevel.A
                               select re;
                tempList = filtered.ToList();
            }
            else if (level == EmployeeLevel.B)
            {
                var filtered = from Researcher re in researchers
                               where re.Level == EmployeeLevel.B
                               select re;
                tempList = filtered.ToList();
            }
            else if(level == EmployeeLevel.C)
            {
                var filtered = from Researcher re in researchers
                               where re.Level == EmployeeLevel.C
                               select re;
                tempList = filtered.ToList();
            }
            else if(level == EmployeeLevel.D)
            {
                var filtered = from Researcher re in researchers
                               where re.Level == EmployeeLevel.D
                               select re;
                tempList = filtered.ToList();
            }
            else
            {
                var filtered = from Researcher re in researchers
                               where re.Level == EmployeeLevel.E
                               select re;
                tempList = filtered.ToList();
            }
            return tempList;
        }

        public static List<Researcher> LoadResearchers()
        {
            researcher = DbAdaptor.LoadAll();
            return researcher;
        }
    }
}
