using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Proyecto
{
    public partial class admin : System.Web.UI.Page
    {
        protected int SelectedSubjectID { get; set; }
        protected int SelectedStudentID { get; set; }
        protected int SelectedProfessorID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string dbFileName = "techville.db";
            string pathDB = Path.Combine(Server.MapPath("~"), dbFileName);

            if (!IsPostBack)
            {
                if (Session["userID"] != null)
                {
                    loadListboxesData(pathDB);
                } else
                {
                    Response.Redirect("Default.aspx");
                }

            }
        }

        private void loadListboxesData(string pathDB)
        {
            ServerLogic serverLogic = new ServerLogic();
            string[] subjects = serverLogic.getAllFromTable("Subjects", "null", pathDB);
            string[] students = serverLogic.getAllFromTable("Users", "Student", pathDB);
            string[] professors = serverLogic.getAllFromTable("Users", "Professor", pathDB);

            lbSubjects.Items.Clear();
            lbStudents.Items.Clear();
            lbProfessors.Items.Clear();
            
            lbSubjects.Items.Add("Deselect");
            lbStudents.Items.Add("Deselect");
            lbProfessors.Items.Add("Deselect");

            foreach (string subject in subjects)
            {
                lbSubjects.Items.Add(subject);
            }

            foreach (string student in students)
            {
                lbStudents.Items.Add(student);
            }

            foreach (string professor in professors)
            {
                lbProfessors.Items.Add(professor);
            }
        }

        protected void btLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void lbSubjects_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (lbSubjects.SelectedItem != null)
            {
                string dbFileName = "techville.db";
                string pathDB = Path.Combine(Server.MapPath("~"), dbFileName);
                string selectedSubjectName = lbSubjects.SelectedItem.Text.ToString();

                if (selectedSubjectName == "Deselect")
                {
                    loadListboxesData(pathDB);
                    return;
                }

                Subject subject = new Subject();
                ServerLogic serverLogic = new ServerLogic();

                SelectedSubjectID = serverLogic.getItemId(selectedSubjectName, "null",  pathDB);
                subject = serverLogic.getSubjectData(SelectedSubjectID, pathDB);

                txtCredits.Text = subject.Credits.ToString(); 
                txtSemester.Text = subject.Semester.ToString();
                txtDegree.Text = subject.Degree;
            }

        }

        protected void lbStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dbFileName = "techville.db";
            string pathDB = Path.Combine(Server.MapPath("~"), dbFileName);
            string selectedStudentName = lbStudents.SelectedItem.Text.ToString();

            if (selectedStudentName == "Deselect")
            {
                loadListboxesData(pathDB);
                return;
            }

            User student = new User();
            ServerLogic serverLogic = new ServerLogic();

            SelectedStudentID = serverLogic.getItemId(selectedStudentName, "Student", pathDB);
            student = serverLogic.searchPersonalData(SelectedStudentID, pathDB);

            txtStudentName.Text = student.Name;
            txtStudentID.Text = student.IDNumber;
        }

        protected void lbProfessors_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dbFileName = "techville.db";
            string pathDB = Path.Combine(Server.MapPath("~"), dbFileName);
            string selectedProfessorName = lbProfessors.SelectedItem.Text.ToString();

            if (selectedProfessorName == "Deselect")
            {
                loadListboxesData(pathDB);
                return;
            }

            User professor = new User();
            ServerLogic serverLogic = new ServerLogic();

            SelectedProfessorID = serverLogic.getItemId(selectedProfessorName, "Professor", pathDB);
            professor = serverLogic.searchPersonalData(SelectedProfessorID, pathDB);

            txtProfessorName.Text = professor.Name;
            txtProfessorID.Text = professor.IDNumber;

        }

        protected void btnUpdateSubject_Click(object sender, EventArgs e)
        {
            string dbFileName = "techville.db";
            string pathDB = Path.Combine(Server.MapPath("~"), dbFileName);
            ServerLogic serverLogic = new ServerLogic();
            Subject editedSubject = new Subject();

            string selectedSubjectName = lbSubjects.SelectedItem.Text.ToString();
            int privateSelectedSubjectID = serverLogic.getItemId(selectedSubjectName, "null", pathDB);

            editedSubject.Semester = Convert.ToInt32(txtSemester.Text);
            editedSubject.Degree = txtDegree.Text;
            editedSubject.Credits = Convert.ToInt32(txtCredits.Text);

            if (serverLogic.editSubject(editedSubject, privateSelectedSubjectID, pathDB))
            {
                operationMessage.Text = "The subject data has been edited correctly";
                loadListboxesData(pathDB);
            }
            else
            {
                operationMessage.Text = "Error editing subject data";
            }

        }
        protected void btnDeleteSubject_Click(object sender, EventArgs e)
        {
            string dbFileName = "techville.db";
            string pathDB = Path.Combine(Server.MapPath("~"), dbFileName);
            ServerLogic serverLogic = new ServerLogic();

            string selectedSubjectName = lbSubjects.SelectedItem.Text.ToString();
            int privateSelectedSubjectID = serverLogic.getItemId(selectedSubjectName, "null", pathDB);

            if (serverLogic.deleteItem(privateSelectedSubjectID, "null", pathDB))
            {
                operationMessage.Text = "deleted succesfully";
                loadListboxesData(pathDB);
            } else
            {
                operationMessage.Text = "error deleting that item";
            }

        }

        protected void btnUpdateStudent_Click(object sender, EventArgs e)
        {
            string dbFileName = "techville.db";
            string pathDB = Path.Combine(Server.MapPath("~"), dbFileName);
            ServerLogic serverLogic = new ServerLogic();
            User editedUser = new User();

            string selectedStudentName = lbStudents.SelectedItem.Text.ToString();

            int privateSelectedStudentID = serverLogic.getItemId(selectedStudentName, "Student", pathDB);

            editedUser.Name = txtStudentName.Text;
            editedUser.IDNumber = txtStudentID.Text;

            bool isAdminEdit = true;

            if (serverLogic.editUser(editedUser, privateSelectedStudentID, isAdminEdit, pathDB))
            {
                operationMessage.Text = "The student data has been edited correctly";
                loadListboxesData(pathDB);
            }
            else
            {
                operationMessage.Text = "Error editing student data";
            }
        }

        protected void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            string dbFileName = "techville.db";
            string pathDB = Path.Combine(Server.MapPath("~"), dbFileName);
            ServerLogic serverLogic = new ServerLogic();

            string selectedStudentName = lbStudents.SelectedItem.Text.ToString();
            int privateSelectedStudentID = serverLogic.getItemId(selectedStudentName, "Student", pathDB);

            if (serverLogic.deleteItem(privateSelectedStudentID, "Student", pathDB))
            {
                operationMessage.Text = "deleted succesfully";
                loadListboxesData(pathDB);
            }
            else
            {
                operationMessage.Text = "error deleting that item";
            }
        }

        protected void btnUpdateProfessor_Click(object sender, EventArgs e)
        {
            string dbFileName = "techville.db";
            string pathDB = Path.Combine(Server.MapPath("~"), dbFileName);
            ServerLogic serverLogic = new ServerLogic();
            User editedUser = new User();

            string selectedProfessorName = lbProfessors.SelectedItem.Text.ToString();

            int privateSelectedProfessorID = serverLogic.getItemId(selectedProfessorName, "Professor", pathDB);

            editedUser.Name = txtProfessorName.Text;
            editedUser.IDNumber = txtProfessorID.Text;

            bool isAdminEdit = true;

            if (serverLogic.editUser(editedUser, privateSelectedProfessorID, isAdminEdit, pathDB))
            {
                operationMessage.Text = "The professor data has been edited correctly";
                loadListboxesData(pathDB);
            }
            else
            {
                operationMessage.Text = "Error editing professor data";
            }
        }

        protected void btnDeleteProfessor_Click(object sender, EventArgs e)
        {
            string dbFileName = "techville.db";
            string pathDB = Path.Combine(Server.MapPath("~"), dbFileName);
            ServerLogic serverLogic = new ServerLogic();

            string selectedProfessorName = lbProfessors.SelectedItem.Text.ToString();
            int privateSelectedProfessorID = serverLogic.getItemId(selectedProfessorName, "Professor", pathDB);

            if (serverLogic.deleteItem(privateSelectedProfessorID, "Professor", pathDB))
            {
                operationMessage.Text = "deleted succesfully";
                loadListboxesData(pathDB);
            }
            else
            {
                operationMessage.Text = "error deleting the professor";
            }
        }

        protected void btnCreateSubject_Click(object sender, EventArgs e)
        {
            string dbFileName = "techville.db";
            string pathDB = Path.Combine(Server.MapPath("~"), dbFileName);
            ServerLogic serverLogic = new ServerLogic();

            Subject subject = new Subject();

            subject.Name = txtNewSubjectName.Text;
            subject.Degree = TxtNewSubjectDegree.Text;
            string professor = txtNewSubjectProfessor.Text;
            subject.Semester = Convert.ToInt32(TxtNewSubjectSemester.Text);
            subject.Credits = Convert.ToInt32(TxtNewSubjectCredits.Text);

            if(serverLogic.createSubject(subject, professor, pathDB))
            {
                operationMessage.Text = "created succesfully";
                loadListboxesData(pathDB);
            } else
            {
                operationMessage.Text = "error creating the Subject";
            }
        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            string dbFileName = "techville.db";
            string pathDB = Path.Combine(Server.MapPath("~"), dbFileName);
            ServerLogic serverLogic = new ServerLogic();

            User user = new User();

            user.Name = txtNewUserName.Text;
            user.Surname = TxtNewUserSurname.Text;
            user.IDNumber = txtNewUserID.Text;
            user.UserType = rblUserType.SelectedValue;

            string password = txtNewUserPassword.Text;

            if (serverLogic.createUser(user, password, pathDB))
            {
                operationMessage.Text = "created succesfully";
                loadListboxesData(pathDB);
            }
            else
            {
                operationMessage.Text = "error creating the User";
            }
        }

        protected void btFilterBySubjects_Click(object sender, EventArgs e)
        {
            string dbFileName = "techville.db";
            string pathDB = Path.Combine(Server.MapPath("~"), dbFileName);
            string selectedSubjectName = lbSubjects.SelectedItem.Text.ToString();
            ServerLogic serverLogic = new ServerLogic();   

            int privateSelectedSubjectID = serverLogic.getItemId(selectedSubjectName, "null", pathDB);
            string[] filteredStudents = serverLogic.filterStudents(privateSelectedSubjectID, pathDB);
            string[] filteredProfessors = serverLogic.filterProfessors(privateSelectedSubjectID, pathDB);

            lbProfessors.Items.Clear();
            lbStudents.Items.Clear();

            foreach (string student in filteredStudents)
            {
                lbStudents.Items.Add(student);
            }
            foreach (string professor in filteredProfessors)
            {
                lbProfessors.Items.Add(professor);
            }
        }

        protected void btStudentAdd_Click(object sender, EventArgs e)
        {
            string dbFileName = "techville.db";
            string pathDB = Path.Combine(Server.MapPath("~"), dbFileName);
            ServerLogic serverLogic = new ServerLogic();
            string selectedSubjectName = lbSubjects.SelectedItem.Text.ToString();
            string selectedStudentName = lbStudents.SelectedItem.Text.ToString();
            int privateSelectedSubjectID = serverLogic.getItemId(selectedSubjectName, "null", pathDB);
            int privateSelectedStudentID = serverLogic.getItemId(selectedStudentName, "Student", pathDB);

            if(serverLogic.AssignUserToSubject(privateSelectedStudentID, privateSelectedSubjectID, "Student", pathDB))
            {
                operationMessage.Text = "Student succesfully added to the subject";
            } else
            {
                operationMessage.Text = "Error adding the student to that subject";
            }

        }

        protected void btProfAdd_Click(object sender, EventArgs e)
        {
            string dbFileName = "techville.db";
            string pathDB = Path.Combine(Server.MapPath("~"), dbFileName);
            ServerLogic serverLogic = new ServerLogic();
            string selectedSubjectName = lbSubjects.SelectedItem.Text.ToString();
            string selectedProfessorName = lbProfessors.SelectedItem.Text.ToString();
            int privateSelectedSubjectID = serverLogic.getItemId(selectedSubjectName, "null", pathDB);
            int privateSelectedProfessorID = serverLogic.getItemId(selectedProfessorName, "Professor", pathDB);

            if (serverLogic.AssignUserToSubject(privateSelectedProfessorID, privateSelectedSubjectID, "Professor", pathDB))
            {
                operationMessage.Text = "Professor succesfully added to the subject";
            }
            else
            {
                operationMessage.Text = "Error adding the professor to that subject";
            }
        }
    }
}