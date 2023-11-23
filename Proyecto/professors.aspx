<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="professors.aspx.cs" Inherits="Proyecto.professors" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Professor's Space</title>
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


        <section class="professor">
            <asp:Label ID="Label1" runat="server" Text="Professor's Space"></asp:Label>
            <asp:ListBox ID="lbSubjectsStudents" runat="server" OnSelectedIndexChanged="lbSubjects_SelectedIndexChanged"></asp:ListBox>
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
