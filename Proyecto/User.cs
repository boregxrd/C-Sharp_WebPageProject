using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Dob { get; set; }
        public string Nationality { get; set; }
        public string IDNumber { get; set; }
        public string Address { get; set; }
        public string UserType { get; set; }
        public string Degree { get; set; }
        public string Semester {  get; set; }
        public int Credits { get; set; }
        public string SubjectsProfessors { get; set; }

        public User()
        {
        }
    }
}