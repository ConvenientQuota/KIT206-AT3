using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AT3.Entity
{
    public enum EmployeeLevel
    {
        [Description("Research Associate")]
        A,
        [Description("Lecturer")]
        B,
        [Description("Assistant Professor")]
        C,
        [Description("Associate Professor")]
        D,
        [Description("Professor")]
        E,
        [Description("Student")]
        Student
    }

    public enum Campus
    {
        Hobart, Launceston, Cradle
    }

    public class Researcher
    {
        /*   Researcher Details    */
        public string Name { get; set; }
        public string employeeLevelString {get; set;}
        public int Id { get; set; }
        public int Supervisor_id { get; set; }
        public string Title { get; set; }
        public string School { get; set; }
        public string Unit { get; set; }
        public Uri Photo { get; set; }
        public Campus campus { get; set; }
        public string Email { get; set; }
        public EmployeeLevel Level { get; set; }
        public DateTime commenceWithInstitute { get; set; }
        public DateTime commenceCurrentPosition { get; set; }
        public int Tenure { get; set; }
        public int Publications { get; set; }
        public int Q1Percentage { get; set; }
        public double ThreeYearAverage { get; set; }
        public int Funding { get; set; }
        public int performancePublication { get; set; }
        public int performanceFunding { get; set; }
        public string Supervisions { get; set; }
        public string Degree { get; set; }
        public string Supervisor { get; set; }

        public override string ToString()
        {
            return Name;// + " (" + Title + ") " + School + " " + Unit + " " + campus + " " + Email + " " + Level + " " + commenceWithInstitute + " " + commenceCurrentPosition + " " + Tenure + " " + Publications + " " + Q1Percentage + " " + ThreeYearAverage + " " + Funding + " " + performancePublication + " " + performanceFunding + " " + Supervisions + " " + Degree + " " + Supervisor;
        }
    }



}
