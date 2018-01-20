<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="MySavingsApp.AdminPage" MasterPageFile="~/MasterPage.Master" %>
<asp:Content ID="AdminPageContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding-left:20px">
    
<%--        <div class="auto-style1">
            <span class="auto-style2"><strong></strong></span><br />
        </div>--%>

        <asp:GridView ID="GridViewUsersAdmin" runat="server" OnSelectedIndexChanged="GridViewUsersAdmin_SelectedIndexChanged">
        </asp:GridView>
        <br />
        <asp:Button ID="ButtonLogout" runat="server" OnClick="ButtonLogout_Click" Text="Logout" />
        <asp:Button ID="ButtonTrackSavings" runat="server" OnClick="ButtonTrackSavings_Click" Text="Savings Tracker" />
        <asp:Button ID="ButtonInterestCalculator" runat="server" OnClick="ButtonInterestCalculator_Click" Text="Interest Calculator" />

        <asp:Label ID="LabelEmailID" runat="server"></asp:Label>
    </div>
</asp:Content>