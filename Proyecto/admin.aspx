﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="Proyecto.admin" %>

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>Administrator's Space</title>
        <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Montserrat&display=swap">
        <link rel="stylesheet" href="styles.css" />
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
                <div class="header-button-container">
                    <asp:Button runat="server" Text="Logout" CssClass="button" OnClick="btLogout_Click" ID="btLogout" />
                </div>
            </header>


            <section class="admin">
                <asp:Label CssClass="PageTitle" runat="server" Text="Administrator's Space"></asp:Label>
                <asp:Label id="operationMessage" runat="server" Text="Update or Delete"></asp:Label>
                </h5>
                <div class="listBoxsContainer">
                    <div class="editListBoxContainers">
                        <label class="lbTitles">Subjects</label>
                        <asp:ListBox ID="lbSubjects" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="lbSubjects_SelectedIndexChanged">
                            <asp:ListItem value="null">Deselect</asp:ListItem>
                        </asp:ListBox>
                        <asp:TextBox ID="txtDegree" runat="server" placeholder="Degree Name"></asp:TextBox>
                        <asp:TextBox ID="txtCredits" runat="server" placeholder="Credits"></asp:TextBox>
                        <asp:TextBox ID="txtSemester" runat="server" placeholder="Semester"></asp:TextBox>
                        <asp:Button class="adminBT" ID="btnUpdateSubject" runat="server" Text="Update Subject"
                            OnClick="btnUpdateSubject_Click" />
                        <asp:Button class="adminBT" ID="btnDeleteSubject" runat="server" Text="Delete Subject"
                            OnClick="btnDeleteSubject_Click" />
                        <asp:Button class="adminBT" ID="btFilterBySubjects" runat="server"
                            OnClick="btFilterBySubjects_Click" Text="Filter by subjects" />
                    </div>
                    <div></div>
                    <div class="editListBoxContainers">
                        <label class="lbTitles">Students</label>
                        <asp:ListBox ID="lbStudents" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="lbStudents_SelectedIndexChanged">
                            <asp:ListItem value="null">Deselect</asp:ListItem>
                        </asp:ListBox>
                        <asp:TextBox ID="txtStudentName" runat="server" placeholder="Student Name"></asp:TextBox>
                        <asp:TextBox ID="txtStudentID" runat="server" placeholder="Student ID"></asp:TextBox>
                        <asp:Button class="adminBT" ID="btnUpdateStudent" runat="server" Text="Update Student"
                            OnClick="btnUpdateStudent_Click" />
                        <asp:Button class="adminBT" ID="btnDeleteStudent" runat="server" Text="Delete Student"
                            OnClick="btnDeleteStudent_Click" />

                        <asp:Button class="adminBT" ID="btStudentAdd" runat="server" OnClick="btStudentAdd_Click"
                            Text="Add to subject" />

                    </div>
                    <div></div>
                    <div class="editListBoxContainers">
                        <label class="lbTitles">Professors</label>
                        <asp:ListBox ID="lbProfessors" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="lbProfessors_SelectedIndexChanged">
                            <asp:ListItem value="null">Deselect</asp:ListItem>
                        </asp:ListBox>
                        <asp:TextBox ID="txtProfessorName" runat="server" placeholder="Professor Name"></asp:TextBox>
                        <asp:TextBox ID="txtProfessorID" runat="server" placeholder="Professor ID"></asp:TextBox>
                        <asp:Button class="adminBT" ID="btnUpdateProfessor" runat="server" Text="Update Professor"
                            OnClick="btnUpdateProfessor_Click" />
                        <asp:Button class="adminBT" ID="btnDeleteProfessor" runat="server" Text="Delete Professor"
                            OnClick="btnDeleteProfessor_Click" />
                        <asp:Button class="adminBT" ID="btProfAdd" runat="server" OnClick="btProfAdd_Click"
                            Text="Add to subject" />
                    </div>
                </div>

                <asp:Label id="createMessage" runat="server" Text="Create"></asp:Label>
                <div id="createContainer">
                    <div id="newSubjectContainer">
                        <asp:Label id="subjectLabel" runat="server" Text="New subject"></asp:Label>
                        <asp:TextBox ID="txtNewSubjectName" runat="server" placeholder="New Subject Name"></asp:TextBox>
                        <asp:TextBox ID="txtNewSubjectProfessor" runat="server" placeholder="New Professor Name">
                        </asp:TextBox>
                        <asp:TextBox ID="TxtNewSubjectDegree" runat="server" placeholder="New Degree Name">
                        </asp:TextBox>
                        <asp:TextBox ID="TxtNewSubjectCredits" runat="server" placeholder="New Degree Credits">
                        </asp:TextBox>
                        <asp:TextBox ID="TxtNewSubjectSemester" runat="server" placeholder="New Degree Semester">
                        </asp:TextBox>
                        <asp:Button class="adminBT" ID="btnCreateSubject" runat="server" Text="Create Subject"
                            OnClick="btnCreateSubject_Click" />
                    </div>
                    <div id="newUserContainer">
                        <asp:Label id="userLabel" runat="server" Text="New user"></asp:Label>
                        <asp:TextBox ID="txtNewUserName" runat="server" placeholder="New User Name"></asp:TextBox>
                        <asp:TextBox ID="TxtNewUserSurname" runat="server" placeholder="New User Surname"></asp:TextBox>
                        <asp:TextBox ID="txtNewUserID" runat="server" placeholder="New User ID"></asp:TextBox>
                        <asp:TextBox ID="txtNewUserPassword" runat="server" placeholder="New User Password">
                        </asp:TextBox>
                        <asp:RadioButtonList ID="rblUserType" runat="server">
                            <asp:ListItem Text="Student" Value="Student" />
                            <asp:ListItem Text="Professor" Value="Professor" />
                        </asp:RadioButtonList>
                        <asp:Button class="adminBT" ID="btnCreateUser" runat="server" Text="Create User"
                            OnClick="btnCreateUser_Click" />
                    </div>
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