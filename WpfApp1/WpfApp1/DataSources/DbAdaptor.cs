﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AT3.Entity;

namespace AT3.DataSources
{
    public static class DbAdaptor
    {
        /*   private static bool localDB = true;
           private const string localUser = "root";
           private const string localPass = "990818";
           private const string localServer = "192.168.92.109"; */

        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";

        private static MySqlConnection conn = null;

        public static T parseEnum<T>(string str)
        {
            return (T)Enum.Parse(typeof(T), str);
        }
        public static MySqlConnection dbAdaptor()
        {
            if (conn == null)
            {
                /*    MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                    builder.Server = localDB? localServer : server;
                    builder.Database = db;
                    builder.UserID = localDB ? localUser : user;
                    builder.Password = localDB ? localPass :pass;
                    builder.Port = 3306;
                    Console.WriteLine(builder.ConnectionString);
                    conn = new MySqlConnection(builder.ConnectionString); */
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }
        /**Researcher method to load all researchers and their details
         */
        public static List<Researcher> LoadAll()
        {
            List<Researcher> researchers = new List<Researcher>();

            MySqlConnection conn = dbAdaptor();
            MySqlDataReader reader = null;
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM researcher;", conn);
                reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    //Conversions
                    int? supervisor_id = reader.IsDBNull(10) ? null : (int?)reader.GetInt32(10);
                    string degree = reader.IsDBNull(9) ? "N/A " : reader.GetString(9);
                    char? Level = reader.IsDBNull(11) ? null : (char?)reader.GetChar(11);
                    EmployeeLevel employeeLevel;
                    Campus campus;

                    switch (Level) { 
                        case 'A':
                            employeeLevel = EmployeeLevel.A;
                            break;
                        case 'B':
                            employeeLevel = EmployeeLevel.B;
                            break;
                        case 'C':
                            employeeLevel = EmployeeLevel.C;
                            break;
                        case 'D':
                            employeeLevel = EmployeeLevel.D;
                            break;
                        case 'E':
                            employeeLevel = EmployeeLevel.E;
                            break;
                        default:
                            employeeLevel = EmployeeLevel.Student;
                            break;
                    }

                    switch (reader.GetString(6)) {
                        case "Hobart":
                            campus = Campus.Hobart;
                            break;
                        case "Launceston":
                            campus = Campus.Launceston;
                            break;
                        default:
                            campus = Campus.Cradle;
                            break;
                    }

                    researchers.Add(new Researcher
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(2) + " " + reader.GetString(3),// given_name + family_name
                        Level = employeeLevel,
                        Title = reader.GetString(4),
                        Supervisor_id = supervisor_id == null ? 0 : supervisor_id.Value,
                        Unit = reader.GetString(5),
                        campus = campus,
                        Email = reader.GetString(7),
                        Photo = new Uri(reader.GetString(8)),
                        Degree = degree,
                    }) ;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            Console.WriteLine("List of Researchers\n");

            foreach (Researcher researcher in researchers)
            {
                /// add things to this
                Console.WriteLine(researcher.Name);
            }
            return researchers;
        }

        /**Publication method to print all publications and their details
         */
        public static List<Publication> LoadPublication()
        {
            List<Publication> publications = new List<Publication>();

            MySqlConnection conn = dbAdaptor();
            MySqlDataReader reader = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM publication", conn);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    OutputRanking outputRanking;
                    OutputType outputType;

                    switch (reader.GetString(2)) {
                        case "Q1":
                            outputRanking = OutputRanking.Q1;
                            break;
                        case "Q2":
                            outputRanking = OutputRanking.Q2;
                            break;
                        case "Q3":
                            outputRanking = OutputRanking.Q3;
                            break;
                        case "Q4":
                            outputRanking = OutputRanking.Q4;
                            break;
                        default:
                            outputRanking = OutputRanking.NA;
                            break;
                    }

                    switch (reader.GetString(5)) {
                        case "Conference":
                            outputType = OutputType.Conference;
                            break;
                        case "Workshop":
                            outputType = OutputType.Workshop;
                            break;
                        default:
                            outputType = OutputType.Other;
                            break;
                    }
                    //You have specified an invalid column ordinal. Error
                    publications.Add(new Publication
                    {
                        Doi = reader.GetString(0),
                        Title = reader.GetString(1),
                        Ranking = outputRanking,
                        // the authors may contains space in the name
                        Authors = reader.GetString(3).Split(',').ToList(),
                        Year = reader.GetInt32(4),
                        Type = outputType,
                        Cite = reader.GetString(6),
                        AvailableFrom = reader.GetDateTime(7)
                    });
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            Console.WriteLine("\nList of Publications\n");
            foreach (Publication publication in publications)
            {

                Console.WriteLine(publication.Doi);
            }

            return publications;
        }

       /* Function for looking for a specific researcher depending on their 'id' 
        */
        public static Researcher ResearcherId(int id)
        {

            MySqlConnection conn = dbAdaptor();
            MySqlDataReader reader = null;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM researcher WHERE id = @id;", conn);
                cmd.Parameters.AddWithValue("@id", id);
                reader = cmd.ExecuteReader();

               
                if (reader.Read())
                {
                    //Conversions
                    int? supervisor_id = reader.IsDBNull(10) ? null : (int?)reader.GetInt32(10);
                    string degree = reader.IsDBNull(9) ? "N/A " : reader.GetString(9);
                    char? Level = reader.IsDBNull(11) ? null : (char?)reader.GetChar(11);

                    Researcher researcher = new Researcher
                    {
                        Name =
                    reader.GetString(0) + " " +// ID
                    reader.GetString(1) + " " +// Type
                    reader.GetString(2) + " " +// given_name
                    reader.GetString(3) + " " +// family_name
                    reader.GetString(4) + " " +// Title
                    reader.GetString(5) + " " +// Unit
                    reader.GetString(6) + " " +// Campus
                    reader.GetString(7) + " " +// Email
                    reader.GetString(8) + " " +//Photo
                    degree + " " +             // Degree 
                    (supervisor_id.HasValue ? supervisor_id.Value.ToString() : "N/A " +
                    (Level.HasValue ? Level.Value.ToString() : "N/A ")) + " " +
                    reader.GetString(12) + " " + //utas_start
                    reader.GetString(13) // Current_start


                    };
                    Console.WriteLine("\nResearcher Found\n");
                    Console.WriteLine(researcher.Name);
                    return researcher;
                    
                }
                else
                {
                    Console.WriteLine("\nResearcher not found");
                    return null;
                }
            }
            finally
            {
               
                if (reader != null) reader.Close();
                conn.Close();
            } 
        }
        public static Publication FindPublication(string doi)
        {
            MySqlConnection conn = dbAdaptor();
            MySqlDataReader reader = null;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM publication WHERE doi = @doi;", conn);
                cmd.Parameters.AddWithValue("@doi", doi);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    OutputRanking outputRanking;
                    OutputType outputType;

                    switch (reader.GetString(2)) {
                        case "Q1":
                            outputRanking = OutputRanking.Q1;
                            break;
                        case "Q2":
                            outputRanking = OutputRanking.Q2;
                            break;
                        case "Q3":
                            outputRanking = OutputRanking.Q3;
                            break;
                        case "Q4":
                            outputRanking = OutputRanking.Q4;
                            break;
                        default:
                            outputRanking = OutputRanking.NA;
                            break;
                    }

                    switch (reader.GetString(5)) {
                        case "Conference":
                            outputType = OutputType.Conference;
                            break;
                        case "Workshop":
                            outputType = OutputType.Workshop;
                            break;
                        default:
                            outputType = OutputType.Other;
                            break;
                    }
                    //You have specified an invalid column ordinal. Error
                    Publication publication = new Publication
                    {
                        Doi = reader.GetString(0),
                        Title = reader.GetString(1),
                        Ranking = outputRanking,
                        // the authors may contains space in the name
                        Authors = reader.GetString(3).Split(',').ToList(),
                        Year = reader.GetInt32(4),
                        Type = outputType,
                        Cite = reader.GetString(6),
                        AvailableFrom = reader.GetDateTime(7)
                    };

                    Console.WriteLine("\nPublication Found\n");
                    Console.WriteLine(publication.Doi);
                    return publication;
                }
                else
                {
                    Console.WriteLine("\nPublication Not Found\n");
                    return null;
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                conn.Close();
            }
        }

        public static List<Publication> ResearchersPublications(string researcherName)
        {
            List<Publication> publications = new List<Publication>();

            MySqlConnection conn = dbAdaptor();
            MySqlDataReader reader = null;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM publication WHERE authors LIKE CONCAT('%', @authors, '%');", conn);
                cmd.Parameters.AddWithValue("@authors", researcherName);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    OutputRanking outputRanking;
                    OutputType outputType;

                    switch (reader.GetString(2)) {
                        case "Q1":
                            outputRanking = OutputRanking.Q1;
                            break;
                        case "Q2":
                            outputRanking = OutputRanking.Q2;
                            break;
                        case "Q3":
                            outputRanking = OutputRanking.Q3;
                            break;
                        case "Q4":
                            outputRanking = OutputRanking.Q4;
                            break;
                        default:
                            outputRanking = OutputRanking.NA;
                            break;
                    }

                    switch (reader.GetString(5)) {
                        case "Conference":
                            outputType = OutputType.Conference;
                            break;
                        case "Workshop":
                            outputType = OutputType.Workshop;
                            break;
                        default:
                            outputType = OutputType.Other;
                            break;
                    }
                    //You have specified an invalid column ordinal. Error
                    publications.Add(new Publication
                    {
                        Doi = reader.GetString(0),
                        Title = reader.GetString(1),
                        Ranking = outputRanking,
                        // the authors may contains space in the name
                        Authors = reader.GetString(3).Split(',').ToList(),
                        Year = reader.GetInt32(4),
                        Type = outputType,
                        Cite = reader.GetString(6),
                        AvailableFrom = reader.GetDateTime(7)
                    });
                }

                if (publications.Count > 0)
                {
                    Console.WriteLine("\nPublications for Researcher: " + researcherName + "\n");
                    foreach (Publication publication in publications)
                    {
                        Console.WriteLine(publication.Title);
                    }
                }
                else
                {
                    Console.WriteLine("\nPublications not found for Researcher: " + researcherName + "\n");
                }

                return publications;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                conn.Close();
            }
        }
            
        // white box test for uc16
        public static bool testResearchSelect() {
            Researcher researcher;
            // some 
            //researcher = ResearcherId(id);

            //researcher = ResearcherId(id);

            //researcher = ResearcherId(id);
            return true;
        }
        
        /**Add researcher function
         * 
         * 
         
            public static List<Researcher> AddResearcher(string id, string type, string given_name, string family_name,string title, string unit, string campus, string email, string photo, string degree, int? supervisorId, char? level, string utasStart,string currentStart)
            {
                MySqlConnection conn = dbAdaptor();
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO researcher (id,", conn);
                }
                finally
                {
                    conn.Close();
                }
            }*/


    }
}
