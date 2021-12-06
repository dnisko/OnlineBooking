<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Korisink/Korisik.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="OnlineBooking.Pages.Korisink.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../../Scripts/jquery-photostack.js"></script>
</asp:Content>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="MainContent">
    <div class="wrapper col1">
        <div id="featured_slide">
            <!-- ####################################################################################################### -->
            <div id="slider">
                <ul id="categories">

                    
                    <li><asp:Repeater ID="reNastani" runat="server">
                            <ItemTemplate>
                                <li>
                                
                                    <h2><%# Eval("naziv") %></h2>
                                    <a href="<%# $"PreviewItem.aspx?id={Eval("id_nastan")}" %>">
                                        <img src='<%# $"~/Sliki.ashx?fileName={DataBinder.Eval(Container.DataItem, "slika")}" %>' runat="server" alt="" /></a>
                                    <p>Датум:<br/><%#  DataBinder.Eval(Container.DataItem, "vreme") %></p>
                                    <asp:Literal ID="lblNastanInfo" runat="server" Text='<%# Eval("kratok_opis") %>' />
                                    <p class="readmore"><a href="<%# $"PreviewItem.aspx?id={Eval("id_nastan")}" %>">Read More &raquo;</a></p>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </li>
                </ul>
                <a class="prev disabled"></a><a class="next disabled"></a>
                <div style="clear: both"></div>
            </div>
            <!-- ####################################################################################################### -->
        </div>
    </div>
</asp:Content>

