<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Korisink/Korisik.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="OnlineBooking.Pages.Korisink.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="scripts/jquery-photostack.js"></script>
</asp:Content>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="MainContent">
    <div class="wrapper col1">
        <div id="featured_slide">
            <!-- ####################################################################################################### -->
            <div id="slider">
                <ul id="categories">

                    <asp:Repeater ID="reNastani" runat="server">
                        <ItemTemplate>
                            <li class="category">
                                
                                <h2><%# Eval("naziv")%></h2>
                                <a href="<%# string.Format("PreviewItem.aspx?id={0}",Eval("id_nastan")) %>">
                                    <img src='<%# string.Format("~/Sliki.ashx?fileName={0}",DataBinder.Eval(Container.DataItem, "slika"))%>' runat="server" alt="" /></a>
                                <p>Датум:<br /><%#  DataBinder.Eval(Container.DataItem, "vreme")%></p>
                                <asp:Literal ID="lblNastanInfo" runat="server" Text='<%# Eval("kratok_opis")%>' />
                                <p class="readmore"><a href="<%# string.Format("PreviewItem.aspx?id={0}",Eval("id_nastan")) %>">Read More &raquo;</a></p>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <a class="prev disabled"></a><a class="next disabled"></a>
                <div style="clear: both"></div>
            </div>
            <!-- ####################################################################################################### -->
        </div>
    </div>
</asp:Content>

