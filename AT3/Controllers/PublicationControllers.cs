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

            //Filters the publications list by 
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

        /*
         * LINQ equivalent
         */
    }
}

