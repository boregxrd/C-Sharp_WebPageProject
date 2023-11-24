using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Proyecto
{
    public partial class students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) {
            string dbFileName = "techville.db";
            string pathDB = Path.Combine(Server.MapPath("~"), dbFileName);

            if (!IsPostBack) { 
                if (Session["userID"] != null)
                {
                    int userID = (int)Session["userID"];
                    ServerLogic serverLogic = new ServerLogic();

                    User student = serverLogic.searchStudentData(userID, pathDB);

                    name.Text = student.Name;
                    surname.Text = student.Surname;
                    dateOfBirth.Text = student.Dob.ToString();
                    nationality.Text = student.Nationality;
                    id.Text = student.IDNumber;
                    address.Text = student.Address;
                    career.Text = student.Degree;
                    semester.Text = student.Semester.ToString();
                    credits.Text = student.Credits.ToString();
                    lbSubjectsProfessors.Items.Add(student.SubjectsProfessors.ToString());
                } else
                {
                    Response.Redirect("login.aspx");
                }
            }
        }

        protected void btLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void btnEditUserConfirmed_Click(object sender, EventArgs e)
        {
            
        }

        protected void btEdit_Click(object sender, EventArgs e)
        {
            string dbFileName = "techville.db";
            string pathDB = Path.Combine(Server.MapPath("~"), dbFileName);
            int userID = (int)Session["userID"];

            User editedUser = new User();

            editedUser.Name = name.Text;
            editedUser.Surname = surname.Text;
            editedUser.Dob = DateTime.ParseExact(dateOfBirth.Text, "MM/dd/yyyy HH:mm:ss", null);
            editedUser.Nationality = nationality.Text;
            editedUser.IDNumber = id.Text;
            editedUser.Address = address.Text;

            ServerLogic serverLogic = new ServerLogic();
            if (serverLogic.editUserStudent(editedUser, userID, pathDB))
            {
                editMessage.Text = "Your data has been edited correctly";
            }
            else
            {
                editMessage.Text = "Error editing your data";
            }
        }
    }
}