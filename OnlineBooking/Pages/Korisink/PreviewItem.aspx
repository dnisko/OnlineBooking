<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Korisink/Korisik.Master" AutoEventWireup="true" CodeBehind="PreviewItem.aspx.cs" Inherits="OnlineBooking.Pages.Korisink.PreviewItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    
    <div id="container" class="tabcontainer">
        <div id="Div1">
            <h1><asp:Literal ID="lblNasov" runat="server" /></h1>
            <img class="imgl" src="~/Sliki.ashx" id="imgSlika" runat="server" alt="" width="125" height="125" />
            <p><asp:Literal ID="lblSopis" runat="server" /></p>
            <p>Видео:</p>
           
            <asp:Label ID="lbl" runat="server"></asp:Label>
            <asp:Literal runat="server" ID="myObject"></asp:Literal>
            <p>Официјален сајт: <asp:HyperLink ID="sajtHL" runat="server" Target="_blank"
                                               BackColor="Transparent" ForeColor="#3366ff" Font-Italic="true"></asp:HyperLink></p>
        
        </div>
    </div>

</asp:Content>