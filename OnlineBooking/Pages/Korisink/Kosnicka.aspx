<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Korisink/Korisik.Master" AutoEventWireup="true" CodeBehind="Kosnicka.aspx.cs" Inherits="OnlineBooking.Pages.Korisink.Kosnicka" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <br />
    <div id="container" class="tabcontainer">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="#616161"
        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
        EnableModelValidation="True" ForeColor="Black" GridLines="Vertical" ShowFooter="true"
        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="naziv" HeaderText="Назив" />
            <asp:BoundField DataField="vreme" HeaderText="Време" />
            <asp:BoundField DataField="zona" HeaderText="Зона" />
            <asp:BoundField DataField="red" HeaderText="Ред" />
            <asp:BoundField DataField="mesto" HeaderText="Место" FooterText="Вкупно"/>
            <asp:TemplateField HeaderText="Цена">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("cena") %>' runat="server"></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                </FooterTemplate>   
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Слика">
                <ItemTemplate>
                    <asp:ImageButton runat="server" id="ImageButton1" ImageUrl="../../Sliki/kosnicka/kosnicka_-.jpg"
                     Width="30px" CommandName="kosnicka" CommandArgument='<%# Eval("k_id_karti") %>' ToolTip="Избриши ја картата од кошничка">
                    </asp:ImageButton>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:HyperLink ID="hLkupi" runat="server" CssClass="hyperlink" BackColor="Transparent" NavigateUrl="~/Naracka.aspx"></asp:HyperLink>
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#3a3a3a" ForeColor="White"/>
        <HeaderStyle BackColor="#3a3a3a" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#808080" ForeColor="White" HorizontalAlign="Right" />
        <RowStyle BackColor="#161616" ForeColor="White"/>
        <AlternatingRowStyle BackColor="#999999" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    <br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label> <br />
        </div>
</asp:Content>

