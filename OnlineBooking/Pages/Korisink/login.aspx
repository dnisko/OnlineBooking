<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Korisink/Korisik.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="OnlineBooking.Pages.Korisink.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div id="container" class="tabcontainer">
        <br />
        <br />
        <table class="logstyle1">
            <tr>
                <td>Корисничко име</td>
            
                <td>
                    <asp:TextBox ID="usertxt" runat="server" CausesValidation="True"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="usertxt" ErrorMessage="Полето е задолжително" 
                        ValidationGroup="login"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Лозинка</td>
            
                <td>
                    <asp:TextBox ID="passtxt" runat="server" TextMode="Password" CausesValidation="True"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="passtxt" ErrorMessage="Полето е задолжително" 
                        ValidationGroup="login"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>

                    <asp:Button ID="LogInbtn" runat="server" OnClick="LogInbtn_Click" Text="LogIn" 
                        ValidationGroup="login" />
&nbsp;&nbsp;</td><td>
                    <asp:Button ID="LogInCanclebtn" runat="server" CausesValidation="False" 
                        OnClick="LogInCanclebtn_Click" Text="Откажи" />

                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblPoraka" runat="server" Text="Label"></asp:Label>
                    <asp:HyperLink BackColor="Transparent" ForeColor="SkyBlue" ID="HyperLink1" 
                        runat="server" NavigateUrl="~/Register.aspx" CssClass="hyperlink">
                        овде.
                    </asp:HyperLink>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>


