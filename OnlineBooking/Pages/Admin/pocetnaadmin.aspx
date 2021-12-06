<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="pocetnaadmin.aspx.cs" Inherits="OnlineBooking.Pages.Admin.pocetnaadmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                
            <asp:MenuItem NavigateUrl="~/Pages/Admin/pocetnaadmin.aspx" Selected="true" Text="Почетна" Value="pocetna" />
            <asp:MenuItem NavigateUrl="~/Pages/Admin/vnesinastan.aspx" Text="Внеси Настан" Value="vnesinastani" />
            <asp:MenuItem NavigateUrl="~/Pages/Admin/karti.aspx" Text="Карти" Value="korisnici" />
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
</asp:Content>

