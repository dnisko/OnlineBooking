<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Korisink/Korisik.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="OnlineBooking.Pages.Korisink.MyProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="container" class="tabcontainer">
        <table class="logstyle1" width="100%">
            <tr>
                <td>
                    <table class="logstyle1">
                        <tr>
                            <td colspan="2">Промени профил</td>
                        </tr>
                        <tr>
                            <td>Име</td>
                            <td>
                                <asp:TextBox ID="txtPime" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Презиме</td>
                            <td>
                                <asp:TextBox ID="txtPprezime" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Корисничко име</td>
                            <td>
                                <asp:TextBox ID="txtPuser" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>email</td>
                            <td>
                                <asp:TextBox ID="txtPemail" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Лозинка</td>
                            <td>
                                <asp:TextBox ID="txtPpass" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="Button1" runat="server" Text="Промени" OnClick="Button1_Click" />
                                <br />
                                <asp:Label ID="lblpromena" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>Купени карти</td>
                        </tr>
                        <asp:DataList ID="DataList1" runat="server"
                            OnItemCommand="DataList1_ItemCommand" Width="100%">
                            <HeaderTemplate>
                                <tr>
                                    <td>Настан</td>
                                    <td>Време</td>
                                    <td>Цена</td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>


                                    <tr>
                                        <td><%#  DataBinder.Eval(Container.DataItem, "naziv")%></td>
                                        <td><%#  DataBinder.Eval(Container.DataItem, "datum_prodazba")%></td>
                                        <td><%#  DataBinder.Eval(Container.DataItem, "cena")%></td>
                                    </tr>

                                </tr>
                            </ItemTemplate>
                        </asp:DataList>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>


