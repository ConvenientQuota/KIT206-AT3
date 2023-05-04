
using AT3.DataSources;
using MySql.Data.MySqlClient;
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
            DbAdaptor adaptor = new DbAdaptor();
            adaptor.ReadData();

            /*
             * Displaying Fake data
             */
            ResearcherController researcherController = new ResearcherController();
            PublicationControllers publicationControllers = new PublicationControllers();
            //Fake Researchers
            List<Researcher> researchers = FakeData.GenerateResearcher();
            Console.WriteLine("All current researchers\n");
            DisplayResearcher(researchers);

            //Fake Publications
            List<Publication> publication = FakeData.GeneratePublication();
            Console.WriteLine("All current Publication\n");
            DisplayPublicaiton(publication);

            //Adding Researchers and Publications
            Researcher researcher1 = new Researcher();
            researcherController.addResearcher(researcher1);

            Publication publication1 = new Publication();
            publicationControllers.addPublication(publication1);

            //get all researchers
            List<Researcher>  allResearcher = FakeData.GenerateResearcher();
            foreach (Researcher researcher in allResearcher)
            {
                researcherController.addResearcher(researcher);
            }

            //remove researcher
            Researcher researcherToRemove = researcherController.researchers[0];
            researcherController.removeResearcher(researcherToRemove);
            

            //Filter By Name (change name)
            List<Researcher> researcher2 = researcherController.filterByName("Alex");
            if (researcher2 != null)
            {
                Console.WriteLine("\nResearchers matching description");
                foreach (Researcher researcher in researcher2)
                {
                    ResearcherController.DisplayResearcherDetails(researcher);
                }
            }

            //Filter By Level
      /*      List<Researcher> researcher3 = researcherController.filterByLevel(EmployeeLevel.A);
            if (researcher3 != null)
            {
                Console.WriteLine("\n Researchers filtered by level");
                foreach (Researcher researcher in researcher3)
                {
                    ResearcherController.DisplayResearcherDetails(researcher);
                }
            } */
        }

        /**
         * Functions for displaying researcher/publication detials using ToString function
         */
        static void DisplayResearcher(List<Researcher> a)
        {
            foreach (Researcher researcher in a)
            {
                Console.WriteLine(researcher.ToString());
            }
        }

        static void DisplayPublicaiton(List<Publication> publications)
        {
            foreach (Publication publication in publications)
            {
                Console.WriteLine(publication.ToString());
            }
        }

    }
}
