﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="Proyecto.admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administrator's Space</title>
    <!--<link rel="stylesheet" href="styles.css" />-->
</head>
<body>
    <form id="form1" runat="server">

        <header>
            <div class="logo">
                <img src="img/techvilleLogo.png" alt="Techville University Logo" />
            </div>
            <nav>
                <ul>
                    <li><a href="#">Home</a></li>
                    <li><a href="#">About Us</a></li>
                    <li><a href="#">Academics</a></li>
                    <li><a href="#">Admissions</a></li>
                    <li><a href="#">Research</a></li>
                    <li><a href="#">Campus Life</a></li>
                    <li><a href="#">Contact</a></li>
                </ul>
            </nav>
            <asp:Button runat="server" Text="Logout" CssClass="button" OnClick="btLogout_Click" ID="btLogout" />
        </header>


        <section class = "admin">
            <h3>Administrator's Space</h3>
            <h5>Update or Delete<asp:Label ID="operationMessage" runat="server"></asp:Label>
            </h5>
            <div>
                <label>Subjects</label>
                <asp:ListBox ID="lbSubjects" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lbSubjects_SelectedIndexChanged" Height="236px" Width="300px">
                    <asp:ListItem value="null">Deselect</asp:ListItem>
                </asp:ListBox>
                <asp:TextBox ID="txtDegree" runat="server" placeholder="Degree Name"></asp:TextBox>
                <asp:TextBox ID="txtCredits" runat="server" placeholder="Credits"></asp:TextBox>
                <asp:TextBox ID="txtSemester" runat="server" placeholder="Semester"></asp:TextBox>
                <asp:Button ID="btnUpdateSubject" runat="server" Text="Update Subject" OnClick="btnUpdateSubject_Click" />
                <asp:Button ID="btnDeleteSubject" runat="server" Text="Delete Subject" OnClick="btnDeleteSubject_Click" />
                <asp:Button ID="btFilterBySubjects" runat="server" OnClick="btFilterBySubjects_Click" Text="Filter by subjects" />
            </div>
            <div></div>
            <div>
                <label>Students</label>
                <asp:ListBox ID="lbStudents" runat="server" AutoPostBack="true" Height="253px" Width="294px" OnSelectedIndexChanged="lbStudents_SelectedIndexChanged">
                    <asp:ListItem value="null">Deselect</asp:ListItem>
                </asp:ListBox>
                <asp:TextBox ID="txtStudentName" runat="server" placeholder="Student Name"></asp:TextBox>
                <asp:TextBox ID="txtStudentID" runat="server" placeholder="Student ID"></asp:TextBox>
                <asp:Button ID="btnUpdateStudent" runat="server" Text="Update Student" OnClick="btnUpdateStudent_Click" />
                <asp:Button ID="btnDeleteStudent" runat="server" Text="Delete Student" OnClick="btnDeleteStudent_Click" />

                <asp:Button ID="btStudentAdd" runat="server" OnClick="btStudentAdd_Click" Text="Add to subject" />

            </div>
            <div></div>
            <div>
                <label>Professors</label>
                <asp:ListBox ID="lbProfessors" runat="server" AutoPostBack="true" Height="245px" Width="271px" OnSelectedIndexChanged="lbProfessors_SelectedIndexChanged">
                    <asp:ListItem value="null">Deselect</asp:ListItem>
                </asp:ListBox>
                <asp:TextBox ID="txtProfessorName" runat="server" placeholder="Professor Name"></asp:TextBox>
                <asp:TextBox ID="txtProfessorID" runat="server" placeholder="Professor ID"></asp:TextBox>
                <asp:Button ID="btnUpdateProfessor" runat="server" Text="Update Professor" OnClick="btnUpdateProfessor_Click" />
                <asp:Button ID="btnDeleteProfessor" runat="server" Text="Delete Professor" OnClick="btnDeleteProfessor_Click" />
                <asp:Button ID="btProfAdd" runat="server" OnClick="btProfAdd_Click" Text="Add to subject" />
            </div>

            <h5>Create</h5>
            <div>
                <asp:TextBox ID="txtNewSubjectName" runat="server" placeholder="New Subject Name"></asp:TextBox>
                <asp:TextBox ID="txtNewSubjectProfessor" runat="server" placeholder="New Professor Name"></asp:TextBox>
                <asp:TextBox ID="TxtNewSubjectDegree" runat="server" placeholder="New Degree Name"></asp:TextBox>
                <asp:TextBox ID="TxtNewSubjectCredits" runat="server" placeholder="New Degree Credits"></asp:TextBox>
                <asp:TextBox ID="TxtNewSubjectSemester" runat="server" placeholder="New Degree Semester"></asp:TextBox>
                <div>
                <asp:Button ID="btnCreateSubject" runat="server" Text="Create Subject" OnClick="btnCreateSubject_Click" />
                </div>
                <div></div>
                <asp:TextBox ID="txtNewUserName" runat="server" placeholder="New User Name"></asp:TextBox>
                <asp:TextBox ID="TxtNewUserSurname" runat="server" placeholder="New User Surname"></asp:TextBox>
                <asp:TextBox ID="txtNewUserID" runat="server" placeholder="New User ID"></asp:TextBox>
                <asp:TextBox ID="txtNewUserPassword" runat="server" placeholder="New User Password"></asp:TextBox>
                <asp:RadioButtonList ID="rblUserType" runat="server">
                    <asp:ListItem Text="Student" Value="Student" />
                    <asp:ListItem Text="Professor" Value="Professor" />
                </asp:RadioButtonList>
                <asp:Button ID="btnCreateUser" runat="server" Text="Create User" OnClick="btnCreateUser_Click" />
            
            </div>
        </section>
      
        
        <footer>
            <div class="footer-links">
                <a href="#">Privacy Policy</a> | <a href="#">Terms of Service</a>
            </div>
            <p>© 2023 Techville University. All rights reserved.</p>
        </footer>


    </form>
</body>
</html>
