<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Korisink/Korisik.Master" AutoEventWireup="true" CodeBehind="Pregled.aspx.cs" Inherits="OnlineBooking.Pages.Korisink.Pregled" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="container" class="tabcontainer">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                CellPadding="4" EnableModelValidation="True" ForeColor="#333333"
                GridLines="None" OnRowDataBound="GridView1_RowDataBound" >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField ItemStyle-Height="20" HeaderText="Р. Број">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>

                        <ItemStyle Height="20px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="naziv" HeaderText="Настан" />
                    <asp:BoundField DataField="opis" HeaderText="Опис" />
                    <asp:BoundField DataField="grad" HeaderText="Град" />
                    <asp:BoundField DataField="ime" HeaderText="Име" />
                    <asp:BoundField DataField="vreme" HeaderText="Дата" />
                    <asp:BoundField DataField="lager" HeaderText="Број на карти" />
                    <asp:HyperLinkField DataNavigateUrlFields="n_id_nastan" 
                DataNavigateUrlFormatString="~/Pregled-karti.aspx?id={0}"
                Text="Подетално"/>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
    </div>
</asp:Content>

