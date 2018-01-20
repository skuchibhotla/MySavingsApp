<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="MySavingsApp.Register" MasterPageFile="~/LoginRegister.Master" %>

<asp:Content ID="RegisterContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table>
            <tr>
                <td colspan="3" class="text-justify"><strong>Registeration Page</strong></td>
            </tr>
            <tr>
                <td>Full Name: </td>
                <td>
                    <asp:TextBox ID="TextBoxFullName" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorFullName" runat="server" ControlToValidate="TextBoxFullName" CssClass="text-danger" ErrorMessage="Full Name cannot be blank!"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Email ID: </td>
                <td>
                    <asp:TextBox ID="TextBoxEmailID" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmailID" runat="server" ControlToValidate="TextBoxEmailID" ErrorMessage="Email ID cannot be blank!" CssClass="text-danger"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmailID" runat="server" ControlToValidate="TextBoxEmailID" ErrorMessage="Email ID format incorrect!" CssClass="text-danger" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>Password: </td>
                <td>
                    <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ControlToValidate="TextBoxPassword" ErrorMessage="Password cannot be blank!" CssClass="text-danger"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Confirm Password:</td>
                <td>
                    <asp:TextBox ID="TextBoxConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorConfirmPassword" runat="server" ControlToValidate="TextBoxConfirmPassword" CssClass="text-danger" ErrorMessage="Confirm Password cannot be blank!"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidatorConfirmPassword" runat="server" ControlToCompare="TextBoxPassword" CssClass="text-danger" ControlToValidate="TextBoxConfirmPassword" ErrorMessage="Passwords do not match!"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <input id="Reset1" type="reset" value="reset" /></td>
                <td>
                    <asp:Button ID="ButtonRegister" runat="server" OnClick="ButtonRegister_Click" Text="Register" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="HyperLinkRegisterationToLogin" runat="server" NavigateUrl="~/Login.aspx">Already a User?</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelRegistrationStatus" runat="server" CssClass="alert-danger"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>