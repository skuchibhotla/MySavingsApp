<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MySavingsApp.Login" MasterPageFile="~/LoginRegister.Master" %>


      <asp:Content ID="LoginPageContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
          
        <table style="align-content:center">
            <tr>
                <td colspan="4"><strong>Login Page</strong> </td>
            </tr>
            <tr>
                <td>Email ID: </td>
                <td>
                    <asp:TextBox ID="TextBoxEmailID" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmailID" runat="server" ControlToValidate="TextBoxEmailID" ErrorMessage="Email ID cannot be blank!" CssClass="text-danger"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmailID" runat="server" ControlToValidate="TextBoxEmailID" ErrorMessage="Email ID: Incorrect format!" CssClass="text-danger" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>Password: </td>
                <td class="auto-style1">
                    <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ControlToValidate="TextBoxPassword" ErrorMessage="Password cannot be blank!" CssClass="text-danger"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <input id="Reset1" type="reset" value="reset" /></td>
                <td class="auto-style1">
                    <asp:Button ID="ButtonLogin" runat="server" OnClick="ButtonLogin_Click" Text="Login" />
                &nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="HyperLinkLoginToRegistration" runat="server" NavigateUrl="~/Register.aspx">New User?</asp:HyperLink>
                </td>
                <td>
                    <asp:Label ID="LabelLoginStatus" CssClass="alert-danger" runat="server"></asp:Label></td>
            </tr>
            </table>

          </asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="head">

</asp:Content>
