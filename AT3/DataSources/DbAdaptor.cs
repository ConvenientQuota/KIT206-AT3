using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AT3.DataSources
{
    internal class DbAdaptor
    {
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
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3};", db, server, user, pass);
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
                    reader.GetString(0) + " " +// ID
                    reader.GetString(1) + " " +// Type
                    reader.GetString(2) + " " +// given_name
                    reader.GetString(3) + " " +// family_name
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
                        DOI = 
                        reader.GetString(0) + " " + //DOI
                        reader.GetString(1) + " " + //Title
                        reader.GetString(2) + " " + //Ranking
                        reader.GetString(3) + " " + //Authors
                        reader.GetString(4) + " " +//Year 
                        reader.GetString(5) + " " + //Type
                        reader.GetString(6) + " " + //cite_as
                        reader.GetString(7) //Avaliable
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

            return publications;
        }
    }


}
