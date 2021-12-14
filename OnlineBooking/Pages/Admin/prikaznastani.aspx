<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="prikaznastani.aspx.cs" Inherits="OnlineBooking.Pages.Admin.Prikaznastani" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
	<asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" CssClass="Center"
		BackColor="#FFFBD6" DynamicHorizontalOffset="2" Font-Names="Verdana" 
		Font-Size="XX-Large" ForeColor="#990000" StaticSubMenuIndent="10px" Width="100%">
		<DynamicHoverStyle BackColor="#990000" ForeColor="White" />
		<DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
		<DynamicMenuStyle BackColor="#FFFBD6" />
		<DynamicSelectedStyle BackColor="#FFCC66" />
		<Items>
				
			<asp:MenuItem NavigateUrl="~/Pages/Admin/pocetnaadmin.aspx" Text="Почетна" Value="pocetna" />
			<asp:MenuItem NavigateUrl="~/Pages/Admin/vnesinastan.aspx" Text="Внеси Настан" Value="vnesinastani" />
			<asp:MenuItem NavigateUrl="~/Pages/Admin/karti.aspx" Text="Карти" Value="korisnici" />
			<asp:MenuItem NavigateUrl="~/Pages/Admin/prikaznastani.aspx" Selected="True" Text="Настани" Value="prikaznastani" />
			<asp:MenuItem NavigateUrl="~/Pages/Admin/korisnici.aspx" Text="Корисници" Value="korisnici" />
			<asp:MenuItem NavigateUrl="~/Pages/Admin/admins.aspx" Text="Администратори" Value="admins" />
		</Items>
			<StaticHoverStyle BackColor="#990000" ForeColor="White" />
			<StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
			<StaticSelectedStyle BackColor="#FFCC66" />
		</asp:Menu>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<asp:GridView ID="gvNastani" runat="server" AutoGenerateColumns="False" 
		EnableModelValidation="True" BackColor="LightGoldenrodYellow" 
		BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" 
		GridLines="None" Width="100%" onrowediting="GvNastani_RowEditing" 
		onrowupdating="GvNastani_RowUpdating" DataKeyNames="id_nastan,naziv" 
		onrowcancelingedit="GvNastani_RowCancelingEdit" 
		onrowdeleting="GvNastani_RowDeleting" 
		onrowdatabound="GvNastani_RowDataBound">
		<AlternatingRowStyle BackColor="PaleGoldenrod" />
		<Columns>
			<asp:TemplateField ItemStyle-Height="20" HeaderText="Р. Број">
				<ItemTemplate>  
					<%#Container.DataItemIndex+1 %>
				</ItemTemplate>

<ItemStyle Height="20px"></ItemStyle>
			</asp:TemplateField>

			<asp:TemplateField HeaderText="Настан">
				<EditItemTemplate>
					<asp:TextBox ID="txtNastan" runat="server" Text='<%#Eval("Naziv") %>'/>
				</EditItemTemplate>
				<ItemTemplate>
					<asp:Label ID="lblitemNastan" runat="server" Text='<%#Eval("naziv") %>'/>
				</ItemTemplate>
			</asp:TemplateField>

			<asp:TemplateField HeaderText="Опис">
				<EditItemTemplate>
					<asp:DropDownList ID="ddOpis" runat="server"/>
				</EditItemTemplate>
				<ItemTemplate>
					<asp:Label ID="lblitemOpis" runat="server" Text='<%#Eval("opis") %>'/>
				</ItemTemplate>
			</asp:TemplateField>

			<asp:TemplateField HeaderText="Место">
				<EditItemTemplate>
					<asp:DropDownList ID="ddIme" runat="server"/>
				</EditItemTemplate>
				<ItemTemplate>
					<asp:Label ID="lblitemIme" runat="server" Text='<% #Eval("ime") %>'/>
				</ItemTemplate>
			</asp:TemplateField>

			<asp:TemplateField HeaderText="Град">  
				<ItemTemplate>
					<asp:Label ID="lblitemGrad" runat="server" Text='<%#Eval("grad") %>'/>
				</ItemTemplate>
			</asp:TemplateField>

			<asp:TemplateField HeaderText="Време">
				<EditItemTemplate>
					<asp:TextBox ID="txtVreme" runat="server" Text='<%#Eval("Vreme") %>'/>
				</EditItemTemplate>
				<ItemTemplate>
					<asp:Label ID="lblitemVreme" runat="server" Text='<%#Eval("vreme") %>'/>
				</ItemTemplate>
			</asp:TemplateField>

			<asp:TemplateField HeaderText="Слика">
				<ItemTemplate>
					<asp:Image id="myImage" 
						runat="server"  Width="140px" Height="140px"
						ImageUrl='<%# string.Format
						("~/Sliki.ashx?fileName={0}",DataBinder.Eval(Container.DataItem, "slika"))%>'>
					</asp:Image>
				</ItemTemplate>
			</asp:TemplateField>

			<asp:TemplateField>
				<EditItemTemplate>
					<asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Sliki/update.jpg" 
						ToolTip="Промени" Height="20px" Width="20px" />
					<asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Sliki/Cancel.jpg" 
						ToolTip="Откажи" Height="20px" Width="20px" />
				</EditItemTemplate>
				<ItemTemplate>
					<asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Sliki/Edit.jpg" 
						ToolTip="Промени" Height="20px" Width="20px" />
					<asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Sliki/delete.jpg" 
						ToolTip="Избриши" Height="20px" Width="20px" />
				</ItemTemplate>
			</asp:TemplateField>

		</Columns>
		<FooterStyle BackColor="Tan" />
		<HeaderStyle BackColor="Tan" Font-Bold="True" />
		<PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
			HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
	</asp:GridView>
	<br />
	<br />
	<asp:Label ID="lblresult" runat="server"></asp:Label>
</asp:Content>

