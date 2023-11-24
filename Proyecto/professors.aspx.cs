using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Proyecto
{
    public partial class professors : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string dbFileName = "techville.db";
            string pathDB = Path.Combine(Server.MapPath("~"), dbFileName);

            
                if (Session["userID"] != null)
                {
                    int userID = (int)Session["userID"];
                    ServerLogic serverLogic = new ServerLogic();

                    String[] subjectsFromProfessor = serverLogic.searchProfessorData(userID, pathDB);

                    foreach (string subject in subjectsFromProfessor)
                    {
                        lbSubjects.Items.Add(subject);
                    }
                }
                else
                {
                    Response.Redirect("login.aspx");
                }
            }
        }

        protected void btLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void lbSubjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("lbSubjects_SelectedIndexChanged triggered");

            if (lbSubjects.SelectedItem != null)
            {
                string dbFileName = "techville.db";
                string pathDB = Path.Combine(Server.MapPath("~"), dbFileName);
                string selectedSubjectName = lbSubjects.SelectedItem.Text.ToString();

                Subject subject = new Subject();
                ServerLogic serverLogic = new ServerLogic();

                int subjectID = serverLogic.getItemId(selectedSubjectName,"null", pathDB);
                string studentsForSubject = serverLogic.GetStudentsForSubject(subjectID, pathDB);
                subject = serverLogic.getSubjectData(subjectID, pathDB);

                lbStudents.Items.Clear();

                string[] studentArray = studentsForSubject.Split(',');
                foreach (string student in studentArray)
                {
                    lbStudents.Items.Add(new ListItem(student, student));
                }

                credits.Text = subject.Credits.ToString();
                semester.Text = subject.Semester.ToString();
                degree.Text = subject.Degree;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No item selected in lbSubjects");
            }
        }
    }
}