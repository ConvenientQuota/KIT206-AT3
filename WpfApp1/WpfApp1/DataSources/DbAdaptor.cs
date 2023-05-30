using MySql.Data.MySqlClient;
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
        /**
         * Researcher method to load all researchers and their details
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

                    researchers.Add(new Researcher
                    {
                        Name =
                        
                    //reader.GetString(0) + " " +// ID
                    //reader.GetString(1) + " " +// Type
                    reader.GetString(4) + " " +// Title
                    reader.GetString(2) + " " +// given_name
                    reader.GetString(3) + " " // family_name
                    /*
                    reader.GetString(4) + " " +// Title
                    reader.GetString(5) + " " +// Unit
                    reader.GetString(6) + " " +// Campus
                    reader.GetString(7) + " " +// Email
                    reader.GetString(8) + " " +//Photo
                    degree + " " +
                    (supervisor_id.HasValue ? supervisor_id.Value.ToString() : "N/A " +
                    (Level.HasValue ? Level.Value.ToString() : "N/A ")) + " " +
                    reader.GetString(12) + " " + //utas_start
                    reader.GetString(13) // Current_start
                    */

                    });
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

        /**
         * Publication method to print all publications and their details
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
                    //You have specified an invalid column ordinal. Error
                    publications.Add(new Publication
                    {
                       // DOI = reader.GetDouble(0),
                        Title = reader.GetString(1),
                       /* Ranking = reader.GetInt32(2),
                        Authors = reader.GetInt32(3),
                        Year = reader.GetInt32(4),
                        Type = reader.GetInt32(5),
                        CiteAs = reader.GetInt32(6),
                        AvailableFrom = reader.GetDateTime(7) */
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

       /*
        * 
        * Function for looking for a specific researcher depending on their 'id' 
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
                    // Create a new Publication object and set its properties
                    Publication publication = new Publication
                    {
                        Doi = reader.GetString(0) + " " + // 
                    reader.GetString(1) + " " +// 
                    reader.GetString(2) + " " +//
                    reader.GetString(3) + " " +// 
                    reader.GetString(4) + " " +// 
                    reader.GetString(5) + " " +// 
                    reader.GetString(6) + " " + reader.GetString(7)
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

        public static List<Publication> FindPublicationsByResearcher(string researcherName)
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
                    // FIX THIS
                    Publication publication = new Publication
                    {
                        Doi = reader.GetString(0) + " " + // 
                    reader.GetString(1) + " " +// 
                    reader.GetString(2) + " " +//
                    reader.GetString(3) + " " +// 
                    reader.GetString(4) + " " +// 
                    reader.GetString(5) + " " +// 
                    reader.GetString(6) + " " + reader.GetString(7)
                    };

                    publications.Add(publication);
                }

                if (publications.Count > 0)
                {
                    Console.WriteLine("\nPublications for Researcher: " + researcherName + "\n");
                    foreach (Publication publication in publications)
                    {
                        Console.WriteLine(publication.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("\nNo publications found for Researcher: " + researcherName + "\n");
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
        /**
         * 
         * Add researcher function
         
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
