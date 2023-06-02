using AT3.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    internal class Class1
    {
   
            public static void Main(string[] args)
            {
            DbAdaptor adaptor =  new DbAdaptor();
                try
                {
                     DbAdaptor.UC8_WhiteBoxTest();
                    Console.WriteLine("White box testing completed successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("White box testing failed with the following error:");
                    Console.WriteLine(ex.Message);
                }
            }
        }
}
