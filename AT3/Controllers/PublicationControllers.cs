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

            public Publication filterByTitle(String title)
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

        /*   public LINQPublicationControllers()
        {
            publications = new List<Publication>();
        } */

        public void LINQaddPublication(Publication publication)
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

        public Publication LinqFilterByAuthor(int author)
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
    }
}

