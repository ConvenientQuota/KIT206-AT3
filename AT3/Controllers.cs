using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT3
{
    internal class Control
    {
        List<Researcher> researchers;

        static void ResearcherController()
        {

        }

        static void addResearcher (Researcher researcher)
        {
            researchers.Add(researcher);
        }

        static void RemoveResearcher(Researcher researcher)
        {
            researchers.Remove(researcher);
        }

        static List<Researcher> GetResearchers()
        {
            return researchers;
        }
        static void Researcher FilterByName(string name)
        {
            return researchers.FirstOrDefault(re => re.Name == name);
        }

        static void DisplayResearcherDetials(Researcher researcher)
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
}
