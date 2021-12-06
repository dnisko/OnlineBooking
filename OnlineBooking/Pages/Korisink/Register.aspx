<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Korisink/Korisik.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="OnlineBooking.Pages.Korisink.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div id="container" class="tabcontainer">
        <table class="logstyle1">
            <tr>
                <td>Корисничко име*</td>
                <td>
                    <asp:TextBox ID="rUser" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rUser" 
                                                ErrorMessage="Полето е задолжително" ValidationGroup="register"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Лозинка*</td>
                <td>
                    <asp:TextBox ID="rPass" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rPass" 
                                                ErrorMessage="Полето е задолжително" ValidationGroup="register"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>email*</td>
                <td>
                    <asp:TextBox ID="rEmail" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rEmail" ErrorMessage="Полето е задолжително" ValidationGroup="register"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="rEmail" 
                                                    ErrorMessage="Внесете валидна email адреса" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                                    ValidationGroup="register"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>Име</td>
                <td>
                    <asp:TextBox ID="rIme" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Презиме</td>
                <td>
                    <asp:TextBox ID="rPrezime" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Регистрирај се" OnClick="Button1_Click" ValidationGroup="register" />
                </td>
            
            
                <td>
                    <asp:Button ID="Button2" runat="server" Text="Откажи" CausesValidation="False" />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblregister" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="regodgovor" runat="server" Text="Label"></asp:Label>
    </div>
</asp:Content>

