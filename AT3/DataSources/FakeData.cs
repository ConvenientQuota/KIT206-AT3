﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AT3
{
    abstract class FakeData
    {

        public static List<Researcher> GenerateResearcher()
        {
            List<Researcher> researchers = new List<Researcher>();
                    researchers.Add(new Researcher
                    {
                        Name = "Alex",
                        Title = "IDK",
                        School = "school1",
                        Unit = "LMK101",
                        Campus = "School",
                        Email = "ExampleEmail@utas.edu.au",
                        Level = EmployeeLevel.A,
                        commenceWithInstitute = "jan/01/1990",
                        commenceCurrentPosition = "jan/01/1990",
                        Tenure = 1,
                        Publications = 1,
                        Q1Percentage = 1,
                        ThreeYearAverage = 3,
                        Funding = 23000,
                        performancePublication = 3,
                        Supervisions = "Monty",
                        Degree = "Bachealor of kelp",
                        Supervisor = "jones"
                    });

                    researchers.Add(new Researcher
                    {
                        Name = "John",
                        Title = "Mr",
                        School = "school1",
                        Unit = "INT101",
                        Campus = "Campus1",
                        Email = "ExampleEmail@utas.edu.au",
                        Level = EmployeeLevel.A,
                        commenceWithInstitute = "jan/01/2000",
                        commenceCurrentPosition = "jan/01/2010",
                        Tenure = 14,
                        Publications = 17,
                        Q1Percentage = 11,
                        ThreeYearAverage = 12,
                        Funding = 232000,
                        performancePublication = 6,
                        Supervisions = "Ethan",
                        Degree = "Bachealor of Belp",
                        Supervisor = "Barnes"
                    });
                    researchers.Add(new Researcher
                    {
                        Name = "Pedro",
                        Title = "Mr",
                        School = "school1",
                        Unit = "OTB602",
                        Campus = "Campus2",
                        Email = "ExampleEmail@utas.edu.au",
                        Level = EmployeeLevel.A,
                        commenceWithInstitute = "jan/01/2020",
                        commenceCurrentPosition = "Feb/22/2025",
                        Tenure = 7,
                        Publications = 9,
                        Q1Percentage = 5,
                        ThreeYearAverage = 7,
                        Funding = 125233,
                        performancePublication = 12,
                        Supervisions = "Devin",
                        Degree = "Bachealor of kelp",
                        Supervisor = "Rein"
                    });   
            return researchers;
        }

        public static List<Publication> GeneratePublication()
        {
            List<Publication> publications = new List<Publication>();
           
                Publication publication = new Publication();
                {
                    publications.Add(new Publication { DOI = "lol", Publications = 1, Title = "A", Authors = 1, PublicationYear = 2000, Ranking = 2, Type = 3, CiteAs = 9, AvailabilityDate = 3, Age = 20, Cite = "citationExample" });
                }
            
            return publications;
        }
    }
}
