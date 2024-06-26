﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="students.aspx.cs" Inherits="Proyecto.students" %>

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>Student's Space</title>
        <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Montserrat&display=swap">
        <link rel="stylesheet" href="styles.css" />
    </head>

    <body style="height: 555px">
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


            <section class="student">
                <asp:Label CssClass="PageTitle" runat="server" Text="Student's Space"></asp:Label>

                <asp:Label ID="personalInformationLabel" runat="server" Text="Edit Personal Information">
                </asp:Label>
                <div id="styleContainer">
                    <div id="userInfoContainer">
                        <div id="imgAndLabel">
                            <asp:Image src="img/editUser.png" alt="Edit user icon" runat="server" Height="133px"
                                Width="155px" />
                        </div>

                        <div id="userInfoDataContainer">
                            <div class="labelAndTextBoxContainers">
                                <asp:Label class="infoLabel" runat="server" Text="Name: "></asp:Label>
                                <asp:TextBox ID="name" runat="server" Text="Name"></asp:TextBox>
                            </div>
                            <div class="labelAndTextBoxContainers">
                                <asp:Label class="infoLabel" runat="server" Text="Surname: "></asp:Label>
                                <asp:TextBox ID="surname" runat="server" Text="Surname"></asp:TextBox>
                            </div>
                            <div class="labelAndTextBoxContainers">
                                <asp:Label class="infoLabel" runat="server" Text="Date of birth: "></asp:Label>
                                <asp:TextBox ID="dateOfBirth" runat="server" Text="DOB"></asp:TextBox>
                            </div>
                            <div class="labelAndTextBoxContainers">
                                <asp:Label class="infoLabel" runat="server" Text="Nationality: "></asp:Label>
                                <asp:TextBox ID="nationality" runat="server" Text="Nationality"></asp:TextBox>
                            </div>
                            <div class="labelAndTextBoxContainers">
                                <asp:Label class="infoLabel" runat="server" Text="ID: "></asp:Label>
                                <asp:TextBox ID="id" runat="server" Text="ID"></asp:TextBox>
                            </div>
                            <div class="labelAndTextBoxContainers">
                                <asp:Label class="infoLabel" runat="server" Text="Address: "></asp:Label>
                                <asp:TextBox ID="address" runat="server" Text="Address"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <asp:Button class="button" ID="btEdit" runat="server" Text="Save" OnClick="btEdit_Click" />
                    <asp:Button ID="btnEditUserConfirmed" runat="server" Text="Confirm"
                        OnClick="btnEditUserConfirmed_Click" Style="display: none;" />
                    <asp:Label ID="editMessage" runat="server"></asp:Label>
                </div>

                <p>
                    <asp:Label ID="courseInfoLabel" runat="server" Text="Course Information"></asp:Label>
                </p>
                <div id="courseInformationContainer">
                    <div>
                        <p>
                            <asp:Label ID="careerLabel" runat="server" Text="Degree: "></asp:Label>
                            <asp:Label ID="career" runat="server" Text="Career"></asp:Label>
                        </p>
                        <p id="semesterAndCredits">
                            <asp:Label ID="semesterLabel" runat="server" Text="Semesters: "></asp:Label>
                            <asp:Label ID="semester" runat="server" Text="Semester"></asp:Label> <br>
                            <asp:Label ID="creditsLabel" runat="server" Text="Total credits:"></asp:Label>
                            <asp:Label ID="credits" runat="server" Text="Credits"></asp:Label>
                        </p>
                    </div>
                    <asp:ListBox ID="lbSubjectsProfessors" runat="server" style="margin-left: 0px">
                    </asp:ListBox>
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