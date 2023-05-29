using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT3.Entity
{
    public enum EmployeeLevel { A, B, C, D, E }
    public enum Campus { Hobart, Launceston, CradleCoast}

    public class Researcher
    {
        /*   Researcher Details    */
        public string Name { get; set; }
        public int Id { get; set; }
        public int Supervisor_id { get; set; }
        public string Title { get; set; }
        public string School { get; set; }
        public string Unit { get; set; }
        public Campus campus { get; set; }
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
            return Name + " (" + Title + ") " + School + " " + Unit + " " + campus + " " + Email + " " + Level + " " + commenceWithInstitute + " " + commenceCurrentPosition + " " + Tenure + " " + Publications + " " + Q1Percentage + " " + ThreeYearAverage + " " + Funding + " " + performancePublication + " " + performanceFunding + " " + Supervisions + " " + Degree + " " + Supervisor;
        }
    }

}
