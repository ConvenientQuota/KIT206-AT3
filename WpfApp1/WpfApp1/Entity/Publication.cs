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
        public double DOI { get; set; }
        public int Publications { get; set; }
        public string Title { get; set; }
        public int Authors { get; set; }
        public int Year { get; set; }
        public int PublicationYear { get; set; }
        public int Ranking { get; set; }
        public int Type { get; set; }
        public int CiteAs { get; set; }
        public int AvailabilityDate { get; set; }
        public int Age { get; set; }
        public string Cite { get; set; }
        public DateTime AvailableFrom { get; set; }


        public override string ToString()
        {
            return DOI + " " + Publications + " " + Title + " " + Authors + " " + PublicationYear + " " + Ranking + " " + Type + " " + CiteAs + " " + AvailabilityDate + " " + Age + " " + Cite;
        }


    }
}