using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT3
{
    public enum EmployeeLevel { A, B, C, D, E }

    public class Researcher
    {

        //i hatw it when technology dosent work in your favour
        public string Name { get; set; }
        public string Title { get; set; }
        public string School { get; set; }
        public string Unit { get; set; }
        public string Campus { get; set; }
        public string Email { get; set; }
        public EmployeeLevel Level { get; set; }
        public string commenceWithInstitute { get; set; }
        public string commenceCurrentPosition { get; set; }
        public int Tenure { get; set; }
        public int Publications { get; set; }
        public int Q1Percentage { get; set; }
        public int ThreeYearAverage { get; set; }
        public int Funding { get; set; }
        public int performancePublication { get; set; }
        public int performanceFunding { get; set; }
        public string Supervisions { get; set; }
        public string Degree { get; set; }
        public string Supervisor { get; set; }

        public override string ToString()
        {
            return Name + " " + Title + " " + School + " " + Unit + " " + Campus + " " + Email + " " + Level + " " + commenceWithInstitute + " " + commenceCurrentPosition + " " + Tenure + " " + Publications + " " + Q1Percentage + " " + ThreeYearAverage + " " + Funding + " " + performancePublication + " " + performanceFunding + " " + Supervisions + " " + Degree + " " + Supervisor;
        }
    }

}
