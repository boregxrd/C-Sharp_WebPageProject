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
        protected void Page_Load(object sender, EventArgs e)
        {
            string dbFileName = "techville.db";
            string pathDB = Path.Combine(Server.MapPath("~"), dbFileName);

            if (!IsPostBack)
            {
                if (Session["userID"] != null)
                {
                    int userID = (int)Session["userID"];
                    ServerLogic serverLogic = new ServerLogic();

                    User student = serverLogic.searchPersonalData(userID, pathDB);

                    name.Text = student.Name;
                    surname.Text = student.Surname;
                    dateOfBirth.Text = student.Dob != null ? student.Dob.ToString() : string.Empty;
                    nationality.Text = student.Nationality ?? string.Empty;
                    id.Text = student.IDNumber;
                    address.Text = student.Address ?? string.Empty;
                    career.Text = student.Degree ?? string.Empty;
                    semester.Text = student.Semester ?? string.Empty;
                    credits.Text = student.Credits.ToString();

                    foreach (string relation in student.SubjectsProfessors)
                    {
                        lbSubjectsProfessors.Items.Add(relation);
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
            editedUser.Dob = DateTime.ParseExact(dateOfBirth.Text, "dd/MM/yyyy", null);
            editedUser.Nationality = nationality.Text;
            editedUser.IDNumber = id.Text;
            editedUser.Address = address.Text;

            ServerLogic serverLogic = new ServerLogic();
            bool isAdminEdit = false;
            if (serverLogic.editUser(editedUser, userID, isAdminEdit, pathDB))
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