using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Collections;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Drawing.Text;
using System.Text;
using System.Data.Entity.Infrastructure;

namespace Proyecto
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btOk_Click(object sender, EventArgs e)
        {
            string dbFileName = "techville.db";
            string pathDB = Path.Combine(Server.MapPath("~"), dbFileName);

            string inputUsername = tbUser.Text;
            string inputPassword = tbPassword.Text;
            string[] receivedData = new string[2];

            serverLogic serverLogic = new serverLogic();

            if (serverLogic.login(inputUsername, inputPassword, pathDB, receivedData))
            {
                string userType = receivedData[0];
                int userID = int.Parse(receivedData[1]);
                Session["userID"] = userID;

                switch (userType) {
                    case "Professor":
                        Response.Redirect("professors.aspx");
                        break;
                    case "Administrator":
                        Response.Redirect("admin.aspx");
                        break;
                    case "Student":
                        Response.Redirect("students.aspx");
                        break;
                    default:
                        errorMessage.Text = "Invalid user type";
                        break;
                } 
            } else {
                errorMessage.Text = "Invalid credentials";
            }
        }
    }
}
