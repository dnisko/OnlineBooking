<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Korisink/Korisik.Master" AutoEventWireup="true" CodeBehind="Nastani.aspx.cs" Inherits="OnlineBooking.Pages.Korisink.Nastani" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="container" class="tabcontainer">
        <table>
            <asp:DataList ID="DataList1" runat="server" Width="80%">
                <HeaderTemplate>
                    <tr style="background-color: #004080; color: White;">
                        <td><b>Настан</b></td>
                        <td><b>Време</b></td>
                        <td><b>Слика</b></td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#  DataBinder.Eval(Container.DataItem, "naziv")%></td>
                        <td><%#  DataBinder.Eval(Container.DataItem, "vreme")%></td>
                        <td>
                            <asp:Image ID="myImage"
                                       runat="server" Width="140px" Height="140px"
                                       ImageUrl='<%# $"~/Sliki.ashx?fileName={DataBinder.Eval(Container.DataItem, "slika")}" %>'></asp:Image>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </table>
    </div>
</asp:Content>
