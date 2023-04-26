using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AT3
{
    abstract class Data
    {

        public static List<Researcher> GenerateResearcher()
        {
            List<Researcher> researchers = new List<Researcher>();

           //loop 10 times for fake researchers 
            for (int i = 1; i <= 10; i++)
            {
                {
                    researchers.Add(new Researcher{ Name = "Alex", Title = "IDK", School = "school1", Unit = "LMK101", Campus = "School", Email = "ExampleEmail@utas.edu.au", Level = EmployeeLevel.A, commenceWithInstitute = "jan/01/1990", commenceCurrentPosition = "jan/01/1990", Tenure = 1, Publications = 1, Q1Percentage = 1, ThreeYearAverage = 3, Funding = 23000, performancePublication = 3, Supervisions = "Monty", Degree = "Bachealor of kelp", Supervisor = "jones" });
                }
            }
            return researchers;

        }

        public static List<Publication> GeneratePublication()
        {
            List<Publication> publications = new List<Publication>();
           for ( int i = 1;i <= 10; i++)
            {
                Publication publication = new Publication();
                {
                    publications.Add(new Publication { DOI = "lol", Publications = 1, Title = 1, Authors = 1, PublicationYear = 2000, Ranking = 2, Type = 3, CiteAs = 9, AvaliabilityDate = 3, Age = 20, Cite = "citationExample" });
                }
            }
           return publications; 
        }
    }
}
