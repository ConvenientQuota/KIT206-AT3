using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AT3.Entity;
using System.Xml.Linq;
using System.Windows.Controls;
using System.Xml;
using AT3.Controllers;

namespace AT3.DataSources
{
    public class DbAdaptor
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

        // load xml data from the Fundings_Rankings.xml
        /*      public static void LoadXML(List<Researcher> researchers)
              {
                  XmlDocument xml = new XmlDocument();
                  xml.Load("./DataSources/Fundings_Rankings.xml");

                  XmlNodeList projectNode = xml.SelectNodes("/Projects/Project");

                  foreach (XmlNode node in projectNode)
                  {
                      int fund = int.Parse(node["Funding"].InnerText);
                      XmlNode Researchers = node["Researchers"];


                      foreach (XmlNode staff_id in Researchers.ChildNodes)
                      {
                          foreach (Researcher researcher in researchers)
                          {
                              if (researcher.Id == int.Parse(staff_id.InnerText))
                              {
                                  researcher.Funding += fund;
                                  researcher.FundingCount += 1;
                              }
                          }
                      }
                  } 
                  foreach (Researcher researcher in researchers)
                  {
                      if (researcher.FundingCount != 0)
                      {
                          researcher.performanceFunding = Math.Round((double)researcher.Funding / researcher.FundingCount, 2);
                      }


                      Console.WriteLine(researcher.performanceFunding);
                  }
              }*/


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
                    String employeeLevelString;
                    Campus campus;

                    switch (Level)
                    {
                        case 'A':
                            employeeLevel = EmployeeLevel.A;
                            employeeLevelString = "Research Associate";
                            break;
                        case 'B':
                            employeeLevel = EmployeeLevel.B;
                            employeeLevelString = "Lecturer";
                            break;
                        case 'C':
                            employeeLevel = EmployeeLevel.C;
                            employeeLevelString = "Assistant Professor";
                            break;
                        case 'D':
                            employeeLevel = EmployeeLevel.D;
                            employeeLevelString = "Associate Professor";
                            break;
                        case 'E':
                            employeeLevel = EmployeeLevel.E;
                            employeeLevelString = "Professor";
                            break;
                        default:
                            employeeLevel = EmployeeLevel.Student;
                            employeeLevelString = "Student";
                            break;
                    }

                    switch (reader.GetString(6))
                    {
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
                        Name = reader.GetString(2) + " " + reader.GetString(3) + "(" + reader.GetString(4) + ")",// given_name + family_name
                        Level = employeeLevel,
                        Title = reader.GetString(4),
                        Supervisor_id = supervisor_id == null ? 0 : supervisor_id.Value,
                        Unit = reader.GetString(5),
                        campus = campus,
                        Email = reader.GetString(7),
                        Photo = new Uri(reader.GetString(8)),
                        Degree = degree,
                        commenceWithInstitute = reader.GetDateTime(12),
                        employeeLevelString = employeeLevelString,
                        commenceCurrentPosition = reader.GetDateTime(13),
                        Tenure = (int)((DateTime.Now - reader.GetDateTime(12)).TotalDays / 365.25),
                        Funding = 0,
                        FundingCount = 0,
                        performanceFunding = 0,
                        Supervisions = 0
                    });
                }

                // update the supervisions count
                foreach (var researcher in researchers)
                {
                    if (researcher.Supervisor_id == 0)
                    {
                        continue;
                    }

                    foreach (var supervisor in researchers)
                    {
                        if (supervisor.Id == researcher.Supervisor_id)
                        {
                            supervisor.Supervisions += 1;
                            researcher.Supervisor = supervisor.Name;
                            break;
                        }
                    }
                }

                //    LoadXML(researchers);
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
                    List<String> authors = reader.GetString(3).Split(',').ToList();
                    string displayName = reader.GetInt32(4) + " " + reader.GetString(1);

                    for (int i = 0; i < authors.Count; i++)
                    {
                        authors[i] = authors[i].Trim();
                    }

                    switch (reader.GetString(2))
                    {
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

                    switch (reader.GetString(5))
                    {
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
                        Authors = authors,
                        Year = reader.GetInt32(4),
                        Type = outputType,
                        Cite = reader.GetString(6),
                        AvailableFrom = reader.GetDateTime(7),
                        Age = (int)((DateTime.Now - reader.GetDateTime(7)).TotalDays / 365.25),
                        DisplayName = displayName
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
                    EmployeeLevel employeeLevel;
                    String employeeLevelString;
                    Campus campus;

                    switch (Level)
                    {
                        case 'A':
                            employeeLevel = EmployeeLevel.A;
                            employeeLevelString = "Research Associate";
                            break;
                        case 'B':
                            employeeLevel = EmployeeLevel.B;
                            employeeLevelString = "Lecturer";
                            break;
                        case 'C':
                            employeeLevel = EmployeeLevel.C;
                            employeeLevelString = "Assistant Professor";
                            break;
                        case 'D':
                            employeeLevel = EmployeeLevel.D;
                            employeeLevelString = "Associate Professor";
                            break;
                        case 'E':
                            employeeLevel = EmployeeLevel.E;
                            employeeLevelString = "Professor";
                            break;
                        default:
                            employeeLevel = EmployeeLevel.Student;
                            employeeLevelString = "Student";
                            break;
                    }

                    switch (reader.GetString(6))
                    {
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

                    Researcher r = new Researcher
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(2) + " " + reader.GetString(3) + "(" + reader.GetString(4) + ")",// given_name + family_name
                        Level = employeeLevel,
                        Title = reader.GetString(4),
                        Supervisor_id = supervisor_id == null ? 0 : supervisor_id.Value,
                        Unit = reader.GetString(5),
                        campus = campus,
                        Email = reader.GetString(7),
                        Photo = new Uri(reader.GetString(8)),
                        Degree = degree,
                        commenceWithInstitute = reader.GetDateTime(12),
                        employeeLevelString = employeeLevelString,
                        commenceCurrentPosition = reader.GetDateTime(13),
                        Tenure = (int)((DateTime.Now - reader.GetDateTime(12)).TotalDays / 365.25)
                    };
                    Console.WriteLine("\nResearcher Found\n");
                    Console.WriteLine(r.Name);
                    return r;
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
                    List<String> authors = reader.GetString(3).Split(',').ToList();
                    string displayName = reader.GetInt32(4) + " " + reader.GetString(1);

                    for (int i = 0; i < authors.Count; i++)
                    {
                        authors[i] = authors[i].Trim();
                    }

                    switch (reader.GetString(2))
                    {
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

                    switch (reader.GetString(5))
                    {
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
                        Authors = authors,
                        Year = reader.GetInt32(4),
                        Type = outputType,
                        Cite = reader.GetString(6),
                        AvailableFrom = reader.GetDateTime(7),
                        Age = (int)((DateTime.Now - reader.GetDateTime(7)).TotalDays / 365.25),
                        DisplayName = displayName
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

        public static double Researchers3YearAvgPublicCount(string researcherName)
        {
            MySqlConnection conn = dbAdaptor();
            MySqlDataReader reader = null;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM publication WHERE authors LIKE CONCAT('%', @authors, '%');", conn);
                cmd.Parameters.AddWithValue("@authors", researcherName.Split('(')[0]);
                reader = cmd.ExecuteReader();

                double count = 0;

                while (reader.Read())
                {

                    if (reader.GetInt32(4) < DateTime.Now.Year - 3)
                    {
                        count += 1;
                    }
                }

                return count / 3;
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
                cmd.Parameters.AddWithValue("@authors", researcherName.Split('(')[0]);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    OutputRanking outputRanking;
                    OutputType outputType;
                    List<String> authors = reader.GetString(3).Split(',').ToList();
                    string displayName = reader.GetInt32(4) + " " + reader.GetString(1);

                    for (int i = 0; i < authors.Count; i++)
                    {
                        authors[i] = authors[i].Trim();
                    }

                    switch (reader.GetString(2))
                    {
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

                    switch (reader.GetString(5))
                    {
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
                        Authors = authors,
                        Year = reader.GetInt32(4),
                        Type = outputType,
                        Cite = reader.GetString(6),
                        AvailableFrom = reader.GetDateTime(7),
                        Age = (int)((DateTime.Now - reader.GetDateTime(7)).TotalDays / 365.25),
                        DisplayName = displayName
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
        public static bool testResearchSelect()
        {
            Researcher researcher;
            // some 
            //researcher = ResearcherId(id);

            //researcher = ResearcherId(id);

            //researcher = ResearcherId(id);
            return true;
        }


        public static void UC8_WhiteBoxTest()
        {
            Console.WriteLine("White Box Testing - UC8_User_views_ResearcherList\n");

            // White Box Test 1: Verify if the researcher list is loaded correctly
            List<Researcher> researchers = LoadAll();
            Console.WriteLine("Test Case 1: Researchers loaded successfully");

            // White Box Test 2: Verify if each researcher has the required attributes populated
            bool allAttributesPopulated = researchers.All(researcher =>
                !string.IsNullOrEmpty(researcher.Name) &&
                // researcher.Level != EmployeeLevel.Student &&
                //  !string.IsNullOrEmpty(researcher.Unit) &&
                !string.IsNullOrEmpty(researcher.Email) &&
                researcher.Photo != null &&
                !string.IsNullOrEmpty(researcher.Degree) &&
                researcher.commenceWithInstitute != DateTime.MinValue &&
                researcher.commenceCurrentPosition != DateTime.MinValue &&
                researcher.Tenure != 0
            //  researcher.Funding != 0 &&
            //   researcher.FundingCount != 0 &&
            //   researcher.Supervisions != 0
            );
            Console.WriteLine("Test Case 2: All researchers have the required attributes populated");

            // Display overall test results
            if (allAttributesPopulated)
            {
                Console.WriteLine("\npassed");
            }
            else
            {
                Console.WriteLine("failed");
            }
        }
        /* Testing reseracher selection functionality
        * Selects both real and fake researcher then checks if returned researcher matches an expected researcher then prints result*/
        public static bool UC16_WhiteBoxTest()
        {

            int existingResearcherId = 123463; //Real researcher Id
            Researcher existingResearcher = ResearcherId(existingResearcherId);
            if (existingResearcher == null || existingResearcher.Id != existingResearcherId)
            {
                Console.WriteLine("WhiteBoxTest failed: Existing researcher selection failed.");
                return false;
            }

            // Test researcher selection with a fake researcher ID
            int nonExistingResearcherId = 1000;
            Researcher nonExistingResearcher = ResearcherId(nonExistingResearcherId);
            if (nonExistingResearcher != null)
            {
                Console.WriteLine("WhiteBoxTest failed: Fake researcher selection failed.");
                return false;
            }

            Console.WriteLine("WhiteBoxTest passed.");
            return true;
        }
        /** Testing for total amount of publications and testing for doi content in publications */
        public static void UC34_WhiteBoxTest()
        {

            List<Publication> expectedPublications = new List<Publication>
            {
                new Publication { Doi = "doi1", Title = "Publication 1" },
                new Publication { Doi = "doi2", Title = "Publication 2" }
            };


            List<Publication> actualPublications = DbAdaptor.LoadPublication();


            AssertPublications(expectedPublications, actualPublications);
        }

        /* Function for confirming real amount of total publications and real Doi in publications */
        private static void AssertPublications(List<Publication> expected, List<Publication> actual)
        {
            //White Box Test 1:
            if (expected.Count != actual.Count)
            {
                throw new Exception("Number of publications does not match. Expected: " + expected.Count + ", Actual: " + actual.Count);
            }
            //White Box Test 2:
            for (int i = 0; i < expected.Count; i++)
            {
                if (expected[i].Doi != actual[i].Doi || expected[i].Title != actual[i].Title)
                {
                    throw new Exception("Publication data does not match at index " + i + ".");
                }
                Console.WriteLine("Publication loaded successfully");
            }

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
