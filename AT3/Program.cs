
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
            ResearcherController researcherController = new ResearcherController();
            PublicationControllers publicationControllers = new PublicationControllers();

            DbAdaptor adaptor = new DbAdaptor();

            //Loading all researchers
            List<Researcher> researchers = DbAdaptor.LoadAll();

            Console.WriteLine("List of Researchers");

            foreach (Researcher researcher in researchers)
            {
                Console.WriteLine(researcher.Name, researcher.Title);
            }

            Console.WriteLine();

            //Loading publications
            int researcherId = 1;
            List<Publication> publications = DbAdaptor.AddPublication(researcherId);

            foreach (Publication publication in publications)
            {
                Console.WriteLine(publication.DOI);
                Console.WriteLine(publication.Title);
                Console.WriteLine(publication.PublicationYear);
            }

            /*
             * Linq Publication controllers
             */

            //Add publications
            Publication removeExample = publicationControllers.LinqAddPublication(new Publication("Title", new List<String> { "Author1" }, 2020));

            //Remove publication
            publicationControllers.LinqRemovePublication(removeExample);

            //Filter publications by title
            Publication titleFilter = publicationControllers.LinqfilterByTitle("Title");

            //Filter by author
            Publication authorFilter = publicationControllers.LinqFilterByAuthor(2); 

            //Filter By year
            List<Publication> yearFilter = publicationControllers.LinqfilterByYear(2020);

            //Reverse sort by year
            List<Publication> sortDecending = publicationControllers.LinqReverseSortByYear();

            /*
             * Fake Data
             * 


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

              //Get all researchers
              List<Researcher>  allResearcher = FakeData.GenerateResearcher();
              foreach (Researcher researcher in allResearcher)
              {
                  researcherController.addResearcher(researcher);
              }

              //Get all publications
              List<Publication> allPublications = FakeData.GeneratePublication();
               foreach (Publication Publication in allPublications)
               {
                   publicationControllers.addPublication(Publication);
               }

              //Remove researcher
              Researcher researcherToRemove = researcherController.researchers[0];
              researcherController.removeResearcher(researcherToRemove);

              //Remove Publication
              Publication publicationToRemove = new Publication();
              publicationControllers.removePublication(publicationToRemove);


              //Filter By Name (change name)
              List<Researcher> filterName = researcherController.filterByName("Alex");
              if (filterName != null)
              {
                  Console.WriteLine("\nResearchers matching description");
                  foreach (Researcher researcher in filterName)
                  {
                      ResearcherController.DisplayResearcherDetails(researcher);
                  }
              }

              //Publication Filter By Title
              Publication titleFilter = publicationControllers.filterByTitle("Title"); // change controller title 'int' to 'string'
              if (titleFilter != null)
              {
                  PublicationControllers.ReferenceEquals(titleFilter, publicationControllers); // Fix
              }

              //Filter By Level
              List<Researcher> filterLevel = researcherController.filterByLevel(EmployeeLevel.A);
              if (filterLevel != null)
              {
                  Console.WriteLine("\n Researchers filtered by level");
                  foreach (Researcher researcher in filterLevel)
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
