using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT3.Entity
{
    public class Publication
    {
        /*     Publication Details           */
        public string Doi { get; set; }
        public int Publications { get; set; }
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public int Year { get; set; }
        public int PublicationYear { get; set; }
        public OutputRanking Ranking { get; set; }
        public OutputType Type { get; set; }
        //public int CiteAs { get; set; }
        public int AvailabilityDate { get; set; }
        public int Age { get; set; }
        public string Cite { get; set; }
        public DateTime AvailableFrom { get; set; }
        public String DisplayName { get; set; }


        public override string ToString()
        {
            return Doi + " " + Publications + " " + Title + " " + Authors + " " + PublicationYear + " " + Ranking + " " + Type + " "  + AvailabilityDate + " " + Age + " " + Cite;
        }

        public Publication()
        {
            Authors = new List<string>();
        }


    }

    public enum OutputType
    {
        Conference, Workshop, Other
    }

    public enum OutputRanking
    {
        Q1, Q2, Q3, Q4, NA
    }
}