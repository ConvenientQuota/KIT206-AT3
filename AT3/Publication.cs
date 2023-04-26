﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT3
{
    public class Publication
    {
        public string DOI {get; set;}
        public int Publications{get; set;}
        public int Title{get; set;}
        public int Authors{get; set;}
        public int PublicationYear{get; set;}       
        public int Ranking{get; set;} 
        public int Type{get; set;}
        public int CiteAs{get; set;}
        public int AvaliabilityDate{get; set;}
        public int Age{get; set;}
        public string Cite { get; set; }

        public List<Researcher> Researchers { get; set;}


    }

    /*
    Public Staff() 
    {

    }

    public Students() 
    {

    }

    */

}