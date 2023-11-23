using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Proyecto
{
    public partial class students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { 
                // Verificar si se han pasado datos a través de Session
                if (Session["userID"] != null)
                {
                    int userID = (int)Session["furnitureData"];
                    serverLogic serverLogic = new serverLogic();

                    User student = serverLogic.searchStudentData(userID);

                    name.Text = student.Name;
                    surname.Text = student.Surname;
                    dateOfBirth.Text = student.Dob;
                    nationality.Text = student.Nationality;
                    id.Text = student.Id;
                    career.Text = student.Degree;
                    semester.Text = student.Semester;
                    credits.Text = student.Credits;
                    lbSubjectsProfessors.Text = student.SubjectsProfessors;

                }
            }
        }

        protected void btLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void btEdit_Click(object sender, EventArgs e)
        {

        }
    }
}