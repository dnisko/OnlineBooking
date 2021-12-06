<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="karti.aspx.cs" Inherits="OnlineBooking.Pages.Admin.karti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="txt/css">
    
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" CssClass="Center"
        BackColor="#FFFBD6" DynamicHorizontalOffset="2" Font-Names="Verdana" 
        Font-Size="XX-Large" ForeColor="#990000" StaticSubMenuIndent="10px" Width="100%">
        <DynamicHoverStyle BackColor="#990000" ForeColor="White" />
        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
        <DynamicMenuStyle BackColor="#FFFBD6" />
        <DynamicSelectedStyle BackColor="#FFCC66" />
        <Items>
                
            <asp:MenuItem NavigateUrl="~/Pages/Admin/pocetnaadmin.aspx" Text="Почетна" Value="pocetna" />
            <asp:MenuItem NavigateUrl="~/Pages/Admin/vnesinastan.aspx" Text="Внеси Настан" Value="vnesinastani" />
            <asp:MenuItem NavigateUrl="~/Pages/Admin/karti.aspx" Selected="True" Text="Карти" Value="korisnici" />
            <asp:MenuItem NavigateUrl="~/Pages/Admin/prikaznastani.aspx" Text="Настани" Value="prikaznastani" />
            <asp:MenuItem NavigateUrl="~/Pages/Admin/korisnici.aspx" Text="Корисници" Value="korisnici" />
            <asp:MenuItem NavigateUrl="~/Pages/Admin/admins.aspx" Text="Администратори" Value="admins" />

        </Items>
            <StaticHoverStyle BackColor="#990000" ForeColor="White" />
            <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
            <StaticSelectedStyle BackColor="#FFCC66" />
        </asp:Menu>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Внеси карти" Font-Size="X-Large"/>
    <table class="stylekorisnici">
        <tr>
            <td colspan="2" align="center">Карта поединечно</td>
            <td colspan="2" align="center">Сите одеднаш</td>
        </tr>
        <tr>
            <td>Настан</td>
            <td><asp:DropDownList ID="ddn1" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Text="--Select--" Value="" />
                </asp:DropDownList>
            </td>
            
            <td>Настан</td>
            <td><asp:DropDownList ID="ddn2" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Text="--Select--" Value="" />
                </asp:DropDownList></td>            
        </tr>
        <tr>
            <td>Зона</td>
            <td><asp:DropDownList ID="ddz" runat="server">
                <asp:ListItem Selected="True" Value="0">---Select----</asp:ListItem>
                <asp:ListItem Value="1">Зона 1</asp:ListItem>
                <asp:ListItem Value="2">Зона 2</asp:ListItem>
                <asp:ListItem Value="3">Зона 3</asp:ListItem>
                <asp:ListItem Value="4">Зона 4</asp:ListItem>
                <asp:ListItem Value="5">Зона 5</asp:ListItem>
                </asp:DropDownList></td>

            <td>Цена</td>
            <td>
                <asp:TextBox ID="txtcena2" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Ред</td>
            <td><asp:DropDownList ID="ddr" runat="server">
                <asp:ListItem Selected="True" Value="0">---Select----</asp:ListItem>
                <asp:ListItem Value="1">Ред 1</asp:ListItem>
                <asp:ListItem Value="2">Ред 2</asp:ListItem>
                <asp:ListItem Value="3">Ред 3</asp:ListItem>
                <asp:ListItem Value="4">Ред 4</asp:ListItem>
                <asp:ListItem Value="5">Ред 5</asp:ListItem>
                </asp:DropDownList></td>
            
            <td colspan="2" align="center">
                <asp:Button ID="btnSite" runat="server" Text="Внеси" Width="110px" OnClick="btnSite_Click" />
            </td>
        </tr>
        <tr>
            <td>Место</td>
            <td><asp:DropDownList ID="ddm" runat="server">
                <asp:ListItem Selected="True" Value="0">---Select----</asp:ListItem>
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
                <asp:ListItem Value="5">5</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Цена</td>
            <td>
                <asp:TextBox ID="txtcena1" runat="server"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnEdna" runat="server" Text="Внеси" Width="110px" OnClick="btnEdna_Click" />
            </td>
            <td colspan="2" align="center">
                <asp:Label ID="lblodgovor" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>


