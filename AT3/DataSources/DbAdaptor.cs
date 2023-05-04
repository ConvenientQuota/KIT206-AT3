using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.BC;
using Org.BouncyCastle.Asn1.X500;
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
        public static List<Researcher> LoadAll()
        {
            List<Researcher> researchers = new List<Researcher>();

            MySqlConnection conn = dbAdaptor();
            MySqlDataReader reader = null;
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT given_name, title, family_name from researcher;", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    researchers.Add(new Researcher { Name = reader.GetString(0) + " " + reader.GetString(2), Title = reader.GetString(1) });
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



        public static List<Publication> AddPublication(int id)
        {
            List<Publication> publications = new List<Publication>();

            MySqlConnection conn = dbAdaptor();
            MySqlDataReader reader = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT DOI, Title, PublicationYear " +
                                         "FROM publication AS pub, researcher_publication AS respub " +
                                         "WHERE pub.doi=respub.doi AND researcher_id=?id", conn);


                cmd.Parameters.AddWithValue("id", id);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //You have specified an invalid column ordinal. Error
                    publications.Add(new Publication
                    {
                        DOI = reader.GetString(0),
                        Title = reader.GetString(1),
                        PublicationYear = reader.GetInt32(2)
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
