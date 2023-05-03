using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT3
{
    internal class PublicationControllers
    {
        public class PublicationController
        {
            public List<Publication> publications { get; }

            public PublicationController()
            {
                publications = new List<Publication>();
            }

            // add publication
            public void addPublication(Publication publication)
            {
                publications.Add(publication);
            }

            // remove publication
            public void removePublication(Publication publication)
            {
                publications.Remove(publication);
            }

            public Publication filterByTitle(int title)
            {
                return publications.FirstOrDefault(pub => pub.Title == title);
            }

            public Publication filterByAuthor(int author)
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
        }

        public static List<Publication> GeneratePublication()
        {
            List<Publication> publications = new List<Publication>();
            for (int i = 1; i <= 10; i++)
            {
                Publication publication = new Publication();
                {
                    publications.Add(new Publication { DOI = "lol", Publications = 1, Title = 1, Authors = 1, PublicationYear = 2000, Ranking = 2, Type = 3, CiteAs = 9, AvailabilityDate = 3, Age = 20, Cite = "citationExample" });
                }
            }
            return publications;
        }
    }
}
}
