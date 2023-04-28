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
            List<Researcher> researchers = Data.GenerateResearcher();
            Console.WriteLine("All current researchers\n");
            DisplayResearcher(researchers);

            //add researcher
            Researcher researcher1 = new Researcher();  
            control.addResearcher(researcher1);

            //get all researchers
            List<Researcher> researcher = control.addResearcher();
            foreach (Researcher researcher in researchers)
            {
                control.addResearcher(researcher);
            }

            //remove researcher
            control.removeResearcher(researcher1);

            //Filter By Name
            Researcher researcher2 = control.filterByName("Alex");
            if (researcher2 != null)
            {
                control.DisplayResearcherDetails(researcher2);
            }

            PublicationController
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
