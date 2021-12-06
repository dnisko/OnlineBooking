<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Korisink/Korisik.Master" AutoEventWireup="true" CodeBehind="Naracka.aspx.cs" Inherits="OnlineBooking.Pages.Korisink.Naracka" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <br />
    <div id="container" class="tabcontainer">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
        EnableModelValidation="True" ForeColor="Black" GridLines="Vertical" ShowFooter="true"
        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="naziv" HeaderText="Назив" />
            <asp:BoundField DataField="vreme" HeaderText="Време" />
            <asp:BoundField DataField="zona" HeaderText="Зона" />
            <asp:BoundField DataField="red" HeaderText="Ред" />
            <asp:BoundField DataField="mesto" HeaderText="Место" FooterText="Total"/>
            <asp:TemplateField HeaderText="Цена">
                <ItemTemplate>
                    <asp:Label ID="Label2" Text='<%# Eval("cena") %>' runat="server"></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                </FooterTemplate>   
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Слика">
                <ItemTemplate>
                    <asp:ImageButton runat="server" id="ImageButton1" ImageUrl="~/Sliki/kosnicka/kosnicka_-.jpg"
                     Width="30px" CommandName="kosnicka" CommandArgument='<%# Eval("k_id_karti") %>'>
                    </asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#F7F7DE" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
        <asp:Label ID="lblinfo" runat="server" Text="Label"></asp:Label> 
        <br />
        <asp:Label ID="lblZad" runat="server" Text="Label"></asp:Label>
        <br />
    <br />
        <table class="logstyle2">
            <tr>
                <td>Име*</td>
                <td>
                    <asp:TextBox ID="txtIme" runat="server" CausesValidation="True" Width="250px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                                                runat="server" ControlToValidate="txtIme" ErrorMessage="*" 
                                                ValidationGroup="plati">

                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Презиме*</td>
                <td>
                    <asp:TextBox ID="txtPrezime" runat="server" CausesValidation="True" Width="250px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPrezime" 
                                                ErrorMessage="*" ValidationGroup="plati">

                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Адреса</td>
                <td>
                    <asp:TextBox ID="txtAdresa" runat="server" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Број на картичка*</td>
                <td>
                    <asp:TextBox ID="txtKarticka" runat="server" ToolTip="Напишете го вашиот број на картичка оц 16 бројки" Width="250px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtKarticka" 
                                                ErrorMessage="*" ValidationGroup="plati"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="&#1042;&#1085;&#1077;&#1089;&#1077;&#1090;&#1077; &#1074;&#1072;&#1083;&#1080;&#1076;&#1077;&#1085; &#1073;&#1088;&#1086;&#1112; &#1085;&#1072; &#1087;&#1083;&#1072;&#1090;&#1077;&#1078;&#1085;&#1072; &#1082;&#1072;&#1088;&#1090;&#1080;&#1095;&#1082;&#1072;" ControlToValidate="txtKarticka" ValidationExpression="^[0-9]{16}$" ValidationGroup="plati"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnOtkaz" runat="server" Text="Откажи" OnClick="btnOtkaz_Click" CausesValidation="False" />
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="Внеси" OnClick="Button2_Click" ValidationGroup="plati" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:Label ID="lblOdgovor" runat="server" Text="Label"></asp:Label>
        </div>
</asp:Content>

