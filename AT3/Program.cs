using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ResearcherController control = new ResearcherController();
            List<Researcher> researchers = FakeData.GenerateResearcher();
            Console.WriteLine("All current researchers\n");
            DisplayResearcher(researchers);

            //add researcher
            Researcher researcher1 = new Researcher();  
            control.addResearcher(researcher1);

            //get all researchers
            List<Researcher>  allResearcher = control.researchers;
            foreach (Researcher researcher in allResearcher)
            {
                control.addResearcher(researcher);
            }

            //remove researcher
            control.removeResearcher(researcher1);

            //Filter By Name
            List<Researcher> researcher2 = control.filterByName("Alex");
            if (researcher2 != null)
            {
                foreach (Researcher researcher in researcher2)
                {
                    ResearcherController.DisplayResearcherDetails(researcher);
                }
            }

            //Filter By Level

            List<Researcher> researcher3 = control.filterByLevel(EmployeeLevel.A);
            if (researcher3 != null)
            {
                foreach (Researcher researcher in researcher2)
                {
                    ResearcherController.DisplayResearcherDetails(researcher);
                }
            }
        }


        static void DisplayResearcher(List<Researcher> a)
        {
            foreach (Researcher researcher in a)
            {
                Console.WriteLine(researcher);
            }
        }
    }
}
