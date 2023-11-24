using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto
{
    public class Subject
    {
        public int SubjectID { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public string Degree { get; set; }
        public int Semester { get; set; }

        public Subject(int subjectID, string name, int credits, string degree, int semester)
        {
            SubjectID = subjectID;
            Name = name;
            Credits = credits;
            Degree = degree;
            Semester = semester;
        }

        public Subject() { }
    }

}