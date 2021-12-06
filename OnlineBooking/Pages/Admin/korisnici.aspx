<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="korisnici.aspx.cs" Inherits="OnlineBooking.Pages.Admin.korisnici" %>

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
            <asp:MenuItem NavigateUrl="~/Pages/Admin/karti.aspx" Text="Карти" Value="korisnici" />
            <asp:MenuItem NavigateUrl="~/Pages/Admin/prikaznastani.aspx" Text="Настани" Value="prikaznastani" />
            <asp:MenuItem NavigateUrl="~/Pages/Admin/korisnici.aspx" Selected="True" Text="Корисници" Value="korisnici" />
            <asp:MenuItem NavigateUrl="~/Pages/Admin/admins.aspx" Text="Администратори" Value="admins" />

        </Items>
            <StaticHoverStyle BackColor="#990000" ForeColor="White" />
            <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
            <StaticSelectedStyle BackColor="#FFCC66" />
        </asp:Menu>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <p style="font-size:x-large">Преглед на купени карти</p><br />
    <asp:HyperLink ID="HyperLink1" runat="server" Font-Size="Large" 
        NavigateUrl="~/Pages/Admin/korisnici.aspx?korisnik=1">Корисници</asp:HyperLink>
&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="HyperLink2" runat="server" Font-Size="Large" 
        NavigateUrl="~/Pages/Admin/korisnici.aspx?nastan=1">Настани</asp:HyperLink>
    <br />
    <asp:Label ID="lblpregled" runat="server" Text="Label"></asp:Label>
    <asp:Panel ID="Kpanel" runat="server" Height="59px">
        <asp:TextBox ID="txtKorisnik" runat="server" Width="148px" ValidationGroup="korisnik"></asp:TextBox>
        <asp:Button ID="Button8" runat="server" onclick="Button8_Click" Text="Внеси" ValidationGroup="korisnik" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button10" runat="server" CausesValidation="False" onclick="Button10_Click" Text="Прикажи ги сите" />
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtKorisnik" 
            ErrorMessage="Полето е задолжително" ValidationGroup="korisnik"></asp:RequiredFieldValidator>
    </asp:Panel>
    <asp:Panel ID="Npanel" runat="server" Height="59px">
        <asp:TextBox ID="txtNastan" runat="server" ValidationGroup="nastan" Width="148px"></asp:TextBox>
        <asp:Button ID="Button11" runat="server" Text="Внеси" ValidationGroup="nastan" OnClick="Button11_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button12" runat="server" CausesValidation="False" Text="Прикажи ги сите" OnClick="Button12_Click" />
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNastan" 
            ErrorMessage="Полето е задолжително" ValidationGroup="nastan"></asp:RequiredFieldValidator>
    </asp:Panel>
    <asp:Label ID="lblErr" runat="server" ForeColor="Red" Text="Label"></asp:Label>
    <br />
    <br />
    <table width="100%">
        <asp:DataList ID="DataList1" runat="server" Width="100%" GridLines="Both"
            Font-Bold="True" Font-Size="Large">
                <HeaderTemplate>
                    <tr align="center" style="background-color: #004080; color: White;">
                        <td><b>Корисник</b></td>
                        <td><b>Настан</b></td>
                        <td><b>Време</b></td>
                        <td><b>Слика</b></td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#  DataBinder.Eval(Container.DataItem, "username") %></td>
                        <td><%#  DataBinder.Eval(Container.DataItem, "naziv")%></td>
                        <td><%#  DataBinder.Eval(Container.DataItem, "datum_prodazba")%></td>
                        <td>
                            <asp:Image ID="myImage"
                                runat="server" Width="140px" Height="140px"
                                ImageUrl='<%# string.Format
                            ("~/Sliki.ashx?fileName={0}",DataBinder.Eval(Container.DataItem, "slika"))%>'></asp:Image>
                        </td>
                    </tr>
                   <!-- <tr>
                        <td colspan="4">--------------------------------------------------------------------------------</td>
                    </tr>-->
                </ItemTemplate>
            </asp:DataList>
        
        <asp:DataList ID="DataList2" runat="server" Width="100%" GridLines="Both"
            Font-Bold="True" Font-Size="Large">
                <HeaderTemplate>
                    <tr align="center" style="background-color: #004080; color: White;">
                        <td><b>Слика</b></td>
                        <td><b>Настан</b></td>
                        <td><b>Време</b></td>
                        <td><b>Корисник</b></td
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Image ID="myImage"
                                runat="server" Width="140px" Height="140px"
                                ImageUrl='<%# string.Format
                            ("~/Sliki.ashx?fileName={0}",DataBinder.Eval(Container.DataItem, "slika"))%>'></asp:Image>
                        </td>
                        <td><%#  DataBinder.Eval(Container.DataItem, "naziv")%></td>
                        <td><%#  DataBinder.Eval(Container.DataItem, "datum_prodazba")%></td>
                        <td><%#  DataBinder.Eval(Container.DataItem, "username")%></td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
    </table>
    <br />
    <br />
    <asp:Panel ID="Panel1" runat="server" Height="115px" style="text-align:center" 
        Width="100%" Visible="False">
        <table width="100%" style="text-align:center">
            <tr>
                <td>Име</td>
                <td>Презиме</td>
                <td>Username</td>
                <td>Password</td>
                <td>email</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtIme1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtPrezime1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtUser1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtPass1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtEmail1" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
    </asp:Panel>
    <br />
    <br />
    <asp:Label ID="lblOdgovor" runat="server" Font-Bold="True" Font-Size="Medium" 
        Text="Label" Visible="False"></asp:Label><br />
        <asp:Label ID="lblresult" runat="server"></asp:Label>
    
    </asp:Content>

