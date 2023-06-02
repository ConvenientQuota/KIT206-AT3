
using AT3.DataSources;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AT3
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            DbAdaptor adaptor = new DbAdaptor();
            try
            {
                //DbAdaptor.UC8_WhiteBoxTest();
                // Console.WriteLine("White box testing completed successfully.");
                //DbAdaptor.UC16_WhiteBoxTest();
                //DbAdaptor.UC34_WhiteBoxTest();
                DbAdaptor.UC40_WhiteBoxTest(); 
            }


            catch (Exception ex)
            {
               // Console.WriteLine("White box testing failed with the following error:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}