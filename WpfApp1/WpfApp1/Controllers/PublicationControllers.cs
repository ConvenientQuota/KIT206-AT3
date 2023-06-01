using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using AT3.Entity;
using AT3.DataSources;
using MySql.Data.MySqlClient;

namespace AT3.Controllers
{
    public class PublicationControllers
    {
            public List<Publication> publications { get; }
        private static List<Publication> publication;

            public PublicationControllers()
            {
                publications = new List<Publication>();
            }

            //Enables the ability to add Publication(s) to the Publication list
            public void addPublication(Publication publication)
            {
                publications.Add(publication);
            }

            //Enables the ability to remove Publication(s) from the Publication list
            public void removePublication(Publication publication)
            {
                publications.Remove(publication);
            }

            public Publication filterByTitle(String title)
            {
                return publications.FirstOrDefault(pub => pub.Title == title);
            }

            public Publication filterByAuthor(List<string> author)
            {
                return publications.FirstOrDefault(pub => pub.Authors == author);
            }

            public List<Publication> filterByYear(int year)
            {
                return publications.Where(pub => pub.PublicationYear == year).ToList();
            }

            public List<Publication> reverseSortByYear()
            {
                return publications.OrderByDescending(pub => pub.PublicationYear).ToList();
            }

        /*
         * LINQ equivalent
         */

        /*   public LINQPublicationControllers()
        {
            publications = new List<Publication>();
        } */

        public void LinqAddPublication(Publication publication)
        {
            publications.Add(publication);
        }

        public void LinqRemovePublication(Publication publication)
        {
            publications.Remove(publication);   
        }

        public Publication LinqfilterByTitle(String title)
        {
            return publications.FirstOrDefault(pub => pub.Title == title);
        }

        public Publication LinqFilterByAuthor(List<string> author)
        {
            return publications.FirstOrDefault(pub => pub.Authors == author);
        }

        public List<Publication> LinqfilterByYear(int year)
        {
            return (from pub in publications
                    where pub.PublicationYear == year
                    select pub).ToList();
        }

        public List<Publication> LinqReverseSortByYear()
        {
            return (from pub in publications
                    orderby pub.PublicationYear descending
                    select pub).ToList();
        }

        public static List<Publication> LoadPublications()
        {
            return DbAdaptor.LoadPublication();
        }

        public static List<Publication> ResearchersPublications(string researcherName)
        {
            return DbAdaptor.ResearchersPublications(researcherName);
        }

        public static List<Publication> SearchByResearcher(Researcher researcher)
        {
            if(researcher == null)
            {
                return publication;
            }

            if(publication == null)
            {
                publication = LoadPublications();
            }

            var pubsByAuthor = from pub in publication
                               from author in pub.Authors
                               where author.Equals(researcher.Name)
                               select pub;

            return (List<Publication>) pubsByAuthor.ToList();

        }



    }
}

