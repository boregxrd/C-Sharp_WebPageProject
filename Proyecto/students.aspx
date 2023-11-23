<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="students.aspx.cs" Inherits="Proyecto.students" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student's Space</title>
    <!--<link rel="stylesheet" href="styles.css" />-->
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
            <asp:Button runat="server" Text="Logout" CssClass="button" OnClick="btLogout_Click" ID="btLogout" />
        </header>


        <section class="student">
            <div>
                <asp:Label ID="Label1" runat="server" Text="Student's Space"></asp:Label>
            </div>
            <p>
                <asp:Label ID="Label2" runat="server" Text="Personal Information"></asp:Label>
            </p>
            <asp:Image src="img/editUser.png" alt="Edit user icon" runat="server" Height="133px" Width="155px" />
            <asp:Label ID="name" runat="server" Text="Name"></asp:Label>
            <asp:Label ID="surname" runat="server" Text="Surname"></asp:Label>
            <asp:Label ID="dateOfBirth" runat="server" Text="DOB"></asp:Label>
            <asp:Label ID="nationality" runat="server" Text="Nationality"></asp:Label>
            <asp:Label ID="id" runat="server" Text="ID"></asp:Label>
            <p>
                <asp:Button ID="btEdit" runat="server" Text="Edit" OnClick="btEdit_Click" />
            </p>
            <asp:Label ID="Label10" runat="server" Text="Course Information"></asp:Label>
            <p>
                <asp:Label ID="career" runat="server" Text="Career"></asp:Label>
            </p>
            <p>
                <asp:Label ID="semester" runat="server" Text="Semester"></asp:Label>
                <asp:Label ID="credits" runat="server" Text="Credits"></asp:Label>
            </p>
            <asp:ListBox ID="lbSubjectsProfessors" runat="server" style="margin-left: 0px" Width="422px"></asp:ListBox>
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
