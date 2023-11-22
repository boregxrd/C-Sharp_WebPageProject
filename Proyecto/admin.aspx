<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="Proyecto.admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <section class = "admin">

            <asp:Label ID="Label1" runat="server" Text="Administrator's Space"></asp:Label>

        </section>
        <asp:ListBox ID="lbStudents" runat="server" Height="384px" Width="367px"></asp:ListBox>
        <asp:ListBox ID="lbProfessorsSubjects" runat="server" Height="384px" Width="367px"></asp:ListBox>
        <p>
            &nbsp;</p>
        <p>
            <asp:Button ID="btAdd" runat="server" Text="Add" />
            <asp:Button ID="btEdit" runat="server" Text="Edit" />
            <asp:Button ID="btDelete" runat="server" Text="Delete" />
        </p>
    </form>
</body>
</html>
