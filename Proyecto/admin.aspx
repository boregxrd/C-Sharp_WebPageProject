<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="Proyecto.admin" %>

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
            <asp:Label ID="Label1" runat="server" Text="Administrator's Space"></asp:Label>
            <asp:ListBox ID="lbStudents" runat="server" Height="384px" Width="367px"></asp:ListBox>
            <asp:ListBox ID="lbProfessorsSubjects" runat="server" Height="384px" Width="367px"></asp:ListBox>
            <asp:Button ID="btAdd" runat="server" Text="Add" />
            <asp:Button ID="btEdit" runat="server" Text="Edit" />
            <asp:Button ID="btDelete" runat="server" Text="Delete" />
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
