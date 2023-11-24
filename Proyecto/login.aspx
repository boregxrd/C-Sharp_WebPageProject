<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Proyecto.login" %>

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>Login to TechVille</title>
        <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Montserrat&display=swap">
        <link rel="stylesheet" href="styles.css" />
    </head>

    <body>
        <form id="loginform" runat="server">

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
                <div class="header-button-container"></div>
            </header>

            <section class="login">
                <asp:Label ID="labelLogin" runat="server" Text="Login"></asp:Label>
                <asp:Label ID="labelId" runat="server" Text="Id"></asp:Label>
                <p>
                    <asp:TextBox ID="tbUser" runat="server"></asp:TextBox>
                </p>
                <asp:Label ID="labelPassword" runat="server" Text="Password"></asp:Label>
                <p>
                    <asp:TextBox ID="tbPassword" runat="server"></asp:TextBox>
                </p>
                <asp:Label ID="errorMessage" runat="server"></asp:Label>
                <asp:Button class="button" ID="btOk" runat="server" Text="Login" OnClick="btOk_Click" />
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