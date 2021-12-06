<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Korisink/Korisik.Master" AutoEventWireup="true" CodeBehind="PregledKarti.aspx.cs" Inherits="OnlineBooking.Pages.Korisink.PregledKarti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div id="container" class="tabcontainer">
                <table>
            <tr>
                <td>
                    <asp:Label ID="lblNastan" Font-Size="X-Large" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Зона&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddZona" runat="server" OnSelectedIndexChanged="ddZona_SelectedIndexChanged"
                        AutoPostBack="true" AppendDataBoundItems="true">
                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="mesto" runat="server" Text="Место"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddMesto" runat="server" OnSelectedIndexChanged="ddMesto_SelectedIndexChanged"
                        AutoPostBack="true" AppendDataBoundItems="true">
                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="red" runat="server" Text="Ред"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddRed" runat="server" OnSelectedIndexChanged="ddRed_SelectedIndexChanged"
                        AutoPostBack="true" AppendDataBoundItems="true">
                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCena" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnKosnicka" runat="server" Text="Додади во кошничка" OnClick="btnKosnicka_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnOtkazi" runat="server" Text="Откажи" OnClick="btnOtkazi_Click" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblOdgovor" runat="server" Text="Label"></asp:Label>
        <br />
    </div>
</asp:Content>

