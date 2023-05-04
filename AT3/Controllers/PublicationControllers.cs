using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AT3
{
    public class PublicationControllers
    {
            public List<Publication> publications { get; }

            public PublicationControllers()
            {
                publications = new List<Publication>();
            }
            public void addPublication(Publication publication)
            {
                publications.Add(publication);
            }

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
}

