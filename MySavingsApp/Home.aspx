<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="MySavingsApp.Home" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="HomePageContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="ButtonSavingsTracker" runat="server" Text="Savings Tracker" OnClick="ButtonSavingsTracker_Click" />
                </td>
                <td>
                    <asp:Button ID="ButtonInterestCalculator" runat="server" Text="Savings Calculator" OnClick="ButtonInterestCalculator_Click" />
                </td>
            </tr>
        </table>
    </div>
  

    </asp:Content>