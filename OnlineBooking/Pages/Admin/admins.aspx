<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="admins.aspx.cs" Inherits="OnlineBooking.Pages.Admin.admins" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="txt/css">
    
</style>
     <style type="text/css">
         .auto-style1 {
             text-align: left;
         }
     </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" CssClass="Center"
        BackColor="#FFFBD6" DynamicHorizontalOffset="2" Font-Names="Verdana" 
        Font-Size="XX-Large" ForeColor="#990000" StaticSubMenuIndent="8px" Width="100%">
        <DynamicHoverStyle BackColor="#990000" ForeColor="White" />
        <DynamicMenuItemStyle HorizontalPadding="10px" VerticalPadding="2px" />
        <DynamicMenuStyle BackColor="#FFFBD6" />
        <DynamicSelectedStyle BackColor="#FFCC66" />
        <Items>
                
            <asp:MenuItem NavigateUrl="~/Pages/Admin/pocetnaadmin.aspx" Text="Почетна" Value="pocetna" />
            <asp:MenuItem NavigateUrl="~/Pages/Admin/vnesinastan.aspx" Text="Внеси Настан" Value="vnesinastani" />
            <asp:MenuItem NavigateUrl="~/Pages/Admin/karti.aspx" Text="Карти" Value="korisnici" />
            <asp:MenuItem NavigateUrl="~/Pages/Admin/prikaznastani.aspx" Text="Настани" Value="prikaznastani" />
            <asp:MenuItem NavigateUrl="~/Pages/Admin/korisnici.aspx" Text="Корисници" Value="korisnici" />
            <asp:MenuItem NavigateUrl="~/Pages/Admin/admins.aspx" Selected="True" Text="Администратори" Value="admins" />

        </Items>
            <StaticHoverStyle BackColor="#990000" ForeColor="White" />
            <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
            <StaticSelectedStyle BackColor="#FFCC66" />
        </asp:Menu>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" Height="229px">
        <div class="auto-style1">
            <div class="style2">
            </div>
            <table class="style4">
                <tr>
                    <td colspan="2" style="font-size:x-large">Додади нов администратор</td>
                </tr>
                <tr>
                    <td>Име</td>
                    <td>
                        <asp:TextBox ID="txtIme" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Презиме</td>
                    <td>
                        <asp:TextBox ID="txtPrezime" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>email</td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" CausesValidation="True"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="reUser" runat="server" ControlToValidate="txtEmail" 
                            ErrorMessage="Внесете валидна email адреса" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="vEmail" runat="server" ErrorMessage="Полето е задолжително" 
                            ControlToValidate="txtEmail">

                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Username</td>
                    <td>
                        <asp:TextBox ID="txtUser" runat="server" CausesValidation="True"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="vUser" runat="server" ErrorMessage="Полето е задолжително" 
                            ControlToValidate="txtUser"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Password</td>
                    <td>
                        <asp:TextBox ID="txtPass" runat="server" CausesValidation="True"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="vPass" runat="server" ErrorMessage="Полето е задолжително" 
                            ControlToValidate="txtPass"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="Button8" runat="server" Text="Откажи" CausesValidation="False" />
                    </td>
                    <td>
                        <asp:Button ID="Button9" runat="server" Text="Внеси" OnClick="Button9_Click" />
                    </td>
                    <td><asp:Label ID="lblodgovor" runat="server"></asp:Label></td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <p>
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EnableModelValidation="True" CellPadding="4" ForeColor="Black" GridLines="Vertical"
            onrowdatabound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
            DataKeyNames="id_klient,username" BackColor="White" BorderColor="#DEDFDE"
            BorderStyle="None" BorderWidth="1px" ShowFooter="true">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ime" HeaderText="Име" />
                <asp:BoundField DataField="prezime" HeaderText="Презиме" />
                <asp:BoundField DataField="email" HeaderText="email" FooterText="Вкупно" />

                <asp:TemplateField HeaderText="Username">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("username") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblVkupno" runat="server"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="isadmin"  HeaderText="admin" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server"
                            ImageUrl="~/Sliki/delete.jpg" ToolTip="Избриши" Height="20px" Width="20px" 
                            CausesValidation="false"/>
                    </ItemTemplate>
                </asp:TemplateField>
        </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </p>
    <asp:Label ID="lblresult" runat="server" Text="Label"></asp:Label>
</asp:Content>

