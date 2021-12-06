<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="vnesinastan.aspx.cs" Inherits="OnlineBooking.Pages.Admin.vnesinastan" %>

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
            <asp:MenuItem NavigateUrl="~/Pages/Admin/vnesinastan.aspx" Selected="True" Text="Внеси Настан" Value="vnesinastani" />
            <asp:MenuItem NavigateUrl="~/Pages/Admin/karti.aspx" Text="Карти" Value="korisnici" />
            <asp:MenuItem NavigateUrl="~/Pages/Admin/prikaznastani.aspx" Text="Настани" Value="prikaznastani" />
            <asp:MenuItem NavigateUrl="~/Pages/Admin/prikaznastani.aspx" Text="Корисници" Value="korisnici" />
            <asp:MenuItem NavigateUrl="~/Pages/Admin/admins.aspx" Text="Администратори" Value="admins" />
        </Items>
            <StaticHoverStyle BackColor="#990000" ForeColor="White" />
            <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
            <StaticSelectedStyle BackColor="#FFCC66" />
        </asp:Menu>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style16
        {
            width: 56px;
        }
        .style17
        {
            width: 141px;
        }
        .style20
        {
            width: 235px;
        }
        .style21
        {
            width: 111px;
        }
        .auto-style1 {
            height: 23px;
        }
        .auto-style2 {
            width: 56px;
            height: 51px;
        }
        .auto-style3 {
            height: 51px;
            width: 141px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style5" style="text-align: left">
        <tr>
            <td colspan="2" class="auto-style1">
                Внеси настан</td>
        </tr>
        <tr>
            <td class="style16">
                Назив</td>
            <td class="style17">
                <asp:TextBox ID="txtNaziv" runat="server" Width="211px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style16">Краток опис</td>
            <td class="style17">
                <asp:TextBox ID="kOpisTxt" runat="server" Height="57px" TextMode="MultiLine" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style16">Долг опис</td>
            <td class="style17">
                <asp:TextBox ID="dOpisTxt" runat="server" Height="57px" TextMode="MultiLine" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style16">Видео</td>
            <td class="style17">
                <asp:TextBox ID="videotxt" runat="server" TextMode="SingleLine" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style16">Сајт</td>
            <td class="style17">
                <asp:TextBox ID="sajttxt" runat="server" TextMode="SingleLine" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style16">
                Опис</td>
            <td class="style17">
                <asp:DropDownList ID="ddOpis" runat="server" Height="16px" Width="211px" AppendDataBoundItems="true">
                    <asp:ListItem Text="-Select-" Value="" />
                </asp:DropDownList>
            </td>
            <td class="style20">
                Внеси нов опис <asp:TextBox ID="txtVopis" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="Button11" runat="server" Text="Внеси опис" 
                    onclick="Button11_Click" />
            </td>
        </tr>
        <tr>
            <td class="style16">
                Објект</td>
            <td class="style17">
                <asp:DropDownList ID="ddObjekt" runat="server" Height="16px" Width="211px" AppendDataBoundItems="true">
                    <asp:ListItem Text="-Select-" Value="" />
                </asp:DropDownList>
            </td>
            <td class="style20">
                Внеси нов објект <asp:TextBox ID="txtVobjekt" runat="server"></asp:TextBox>
            </td>
            <td class="style21">
                Град<asp:TextBox ID="txtVgrad" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="Button10" runat="server" Text="Внеси објект" 
                    onclick="Button10_Click" />
            </td>
        </tr>
        <tr>
            <td class="style16">
                Дата</td>
            <td class="style17">
                    <asp:Calendar ID="Calendar1" runat="server" FirstDayOfWeek="Monday" 
                        OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar> 
                    <br />
                    <asp:Label ID="lblcal" runat="server" Text="Внесен"></asp:Label>
            </td>
            <td>
                <table>
                    <tr>
                        <td>Час</td>
                        <td>
                            <asp:DropDownList ID="ddCas" runat="server">
                            </asp:DropDownList>
                            <br />
                            <asp:Label ID="Label16" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                 </table>
            </td>
        </tr>
        <tr>
            <td class="style16">
                Слика</td>
                <td class="style17">
                <asp:Label ID="lblSlika" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                
                <asp:Label ID="lblSlikaOdg" runat="server" Text="Label"></asp:Label>
                
            </td>          
        </tr>
        <tr>
            <td class="style16">
                <asp:Button ID="Button8" runat="server" Text="Внеси" onclick="Button8_Click" />
            </td>
            <td class="style17">
                &nbsp;</td>
            <td class="style20">
                <asp:Button ID="Button9" runat="server" Text="Откажи" />
            </td>

            <td>
                <asp:Label ID="lblOdgovor" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

