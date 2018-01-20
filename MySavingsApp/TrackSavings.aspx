<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrackSavings.aspx.cs" Inherits="MySavingsApp.TrackSavings" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="TrackSavingsContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="padding-left:20px">

        <table style="border:none">
            <tr>
                <td>
                    Creditor Name: 
                </td>
                <td>
                    <asp:TextBox ID="TextBoxCreditorName" runat="server" CssClass="form-control" OnTextChanged="TextBoxCreditorName_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorCreditorName" runat="server" ControlToValidate="TextBoxCreditorName" CssClass="text-danger" ErrorMessage="Creditor Name field cannot be blank!"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Principal Amount: 
                </td>
                <td>
                    <asp:TextBox ID="TextBoxPrincipalAmount" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPrincipalAmount" runat="server" ControlToValidate="TextBoxPrincipalAmount" CssClass="text-danger" ErrorMessage="Principal Amount field cannot be blank!"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxPrincipalAmount" ErrorMessage="Enter only numbers/Enter positive numbers only!" ForeColor="Red" ValidationExpression="[0-9]+(\.[0-9][0-9]?)?"></asp:RegularExpressionValidator>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TextBoxPrincipalAmount" Type="Double" ErrorMessage="Enter value greater than zero and less than 100,000,000" ForeColor="Red" MaximumValue="100000000" MinimumValue="0.10"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Interest Rate: 
                </td>
                <td>
                    <asp:TextBox ID="TextBoxInterestRate" runat="server" CssClass="form-control"></asp:TextBox> 
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorInterestRate" runat="server" ControlToValidate="TextBoxInterestRate" CssClass="text-danger" ErrorMessage="Interest Rate field cannot be blank!"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBoxInterestRate" ErrorMessage="Enter only numbers/Enter positive numbers only!" ForeColor="Red" ValidationExpression="[0-9]+(\.[0-9][0-9]?)?"></asp:RegularExpressionValidator>
                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="TextBoxInterestRate" Type="Double" ErrorMessage="Enter value greater than zero and less than 100,000,000" ForeColor="Red" MaximumValue="100000000" MinimumValue="0.10"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Interest Type: 
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonListInterestType" runat="server" AutoPostBack="true" CssClass="radio">
                        <asp:ListItem>Simple Interest</asp:ListItem>
                        <asp:ListItem>Compound Interest</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorInterestType" runat="server" ControlToValidate="RadioButtonListInterestType" CssClass="text-danger" ErrorMessage="Select Interest Type"></asp:RequiredFieldValidator>
                    <br />
                    Compound Interest Term:
                    <br />
                    <asp:DropDownList ID="DropDownListInterestCalculationMethod" runat="server" Enabled="false" Visible="false" AutoPostBack="true" >
                        <asp:ListItem>Quarterly</asp:ListItem>
                        <asp:ListItem>Half Yearly</asp:ListItem>
                        <asp:ListItem>Yearly</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Owner:
                </td>
                <td>
                    <asp:DropDownList ID="DropDownListOwner" runat="server" CssClass="dropdown" OnSelectedIndexChanged="DropDownListOwner_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Status: 
                </td>
                <td>
                    <asp:DropDownList ID="DropDownListStatus" runat="server" AutoPostBack="true">
                        <asp:ListItem>Open</asp:ListItem>
                        <asp:ListItem>Closed</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Data being entered by:
                </td>
                <td>
                    <asp:Label ID="LabelDataEnteredBy" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="LabelStatus" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="ButtonClearAll" runat="server" Text="Clear" OnClick="ButtonClearAll_Click" CausesValidation="false"/>
                </td>
                <td>
                    <asp:Button ID="ButtonSave" runat="server" Text="Save" OnClick="ButtonSave_Click1" />
                </td>
                <td>
                    <asp:Button ID="ButtonDelete" runat="server" Text="Delete" OnClick="ButtonDelete_Click" />
                </td>
            </tr>
            </table>

        <table>
            <tr>
                <td style="padding-left:20px">
                    <asp:GridView ID="GridViewSavingsTracker" AutoGenerateColumns="false" runat="server" DataKeyNames="savingsID" OnRowCommand="GridViewSavingsTracker_RowCommand" AllowPaging="true" OnSelectedIndexChanged="GridViewSavingsTracker_SelectedIndexChanged" OnPageIndexChanging="GridViewSavingsTracker_PageIndexChanging" PageSize="10">
                        <Columns>
                            <asp:CommandField ShowSelectButton="true" />
                            <asp:BoundField DataField="creditorName" HeaderText="Creditor Name" />
                            <%--<asp:BoundField DataField="amount" HeaderText="Principal Amount (In US$)" />--%>

                            <asp:TemplateField HeaderText="Principal Amount">
                                <ItemTemplate>
                                    <asp:Literal ID="PrincipalAmount" runat="server" Text='<%# "$" + Eval("amount") %>'></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>

                           <%-- <asp:BoundField DataField="interestRate" HeaderText="Interest Rate (In Percent '%')" />--%>
                            <asp:TemplateField HeaderText="Interest Rate" >
                                <ItemTemplate>
                                    <asp:Literal ID="IntRate" runat="server" Text='<%# Eval("interestRate") + "%" %>'></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:boundfield datafield="interesttype" headertext="interest type" />
                            <asp:BoundField DataField="interestCalculationMethod" HeaderText="Interest Calculation Method" />
                            <asp:BoundField DataField="insertedDate" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" />
                            <asp:BoundField DataField="owner" HeaderText="Owner" />
                            <asp:BoundField DataField="dataEnteredBy" HeaderText="Data Entered By" />
                            <asp:BoundField DataField="status" HeaderText="status" />
                            <asp:ButtonField Text="Compute Interest" CommandName="Compute" ButtonType="Link" HeaderText ="Compute Interest?" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

<asp:Content ID="Content1" runat="server" contentplaceholderid="head">

</asp:Content>
