using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Researcher> researchers = Data.GenerateResearcher();
            Console.WriteLine("All current researchers\n");
            DisplayResearcher(researchers);
        }

        static void DisplayResearcher(List<Researcher> a)
        {
            foreach (Researcher researcher in a)
            {
                Console.WriteLine(researcher);
            }
        }
    }
}
