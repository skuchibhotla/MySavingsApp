<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InterestCalculator.aspx.cs" Inherits="MySavingsApp.InterestCalculator" MasterPageFile="~/MasterPage.Master" EnableEventValidation="false" %>


<asp:Content ID="InterestCalculator" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {            
            $("#FromDate").datepicker({
                autoclose: true,
                format: 'MM/dd/yyyy',
                todayHighlight: true,
                clearBtn: true,
                orientation: 'bottom'
                

            });
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {            
            $("#ToDate").datepicker({
                    autoclose: true,
                    format: 'MM/dd/yyyy',
                    todayHighlight: true,
                    clearBtn: true,
                    orientation: 'bottom', 
                });
            });
    </script>

    <div style="padding:20px">
        <table>
            <tr>
                <td colspan="3">
                    <strong><u>Input Details</u></strong>
                </td>
  
            </tr>
            <tr>
                <td class="auto-style1">
                    From Date: 
                </td>
                <td rowspan="2" class="auto-style1">  
                    <div class="form-group">  
                        <%--<label for="usr">FromDate:</label> --%>                     
                        <div class='input-group date' id='FromDate'>
                            <asp:TextBox ID="TextBoxFromDate" runat="server" CssClass="form-control"/>
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>
                <div class="form-group">
                    <%--<label for="usr">ToDate:</label>--%>
                    <div class="input-group date" id="ToDate">
                        <asp:TextBox ID="TextBoxToDate" runat="server" CssClass="form-control"></asp:TextBox>
                        <span class="input-group-addon">
                        <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                    </div> 
                    </div>

                </td>
                <td>
                    <%--<asp:Button ID="ButtonFromDate" runat="server" Text="Set Starting Date" OnClick="ButtonFromDate_Click" />--%>

                    <%--<asp:Calendar ID="CalendarFrom" runat="server" OnSelectionChanged="CalendarFrom_SelectionChanged"></asp:Calendar>--%>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxFromDate" CssClass="text-danger" ErrorMessage="Start Date cannot be blank!" ValidationGroup="ComputeGroup"></asp:RequiredFieldValidator>

                    <br />
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    To Date: 
                </td>
                <td class="auto-style3">

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxToDate" CssClass="text-danger" ErrorMessage="End Date cannot be blank!" ValidationGroup="ComputeGroup"></asp:RequiredFieldValidator>

                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBoxFromDate" ControlToValidate="TextBoxToDate" CssClass="text-danger" ErrorMessage="To Date cannot be less than or equal to the Start Date" Operator="GreaterThan" Type="Date"></asp:CompareValidator>

                    </td>
            </tr>
            <tr>
                <td>
                    Principal Amount: 
                </td>
                <td>
                    <asp:TextBox ID="TextBoxPrincipal" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBoxPrincipal" ErrorMessage="Principal Amount cannot be blank!" CssClass="text-danger" ValidationGroup="ComputeGroup"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxPrincipal" ErrorMessage="Enter only numbers/Enter positive numbers only!" ForeColor="Red" ValidationExpression="[0-9]+(\.[0-9][0-9]?)?"></asp:RegularExpressionValidator>
                    <br />
                    $<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TextBoxPrincipal" ErrorMessage="Enter value greater than zero and less than 100,000,000" Type="Double" ForeColor="Red" MaximumValue="100000000" MinimumValue="0.10"></asp:RangeValidator>
&nbsp;</td>
            </tr>
            <tr>
                <td>
                    Interest Rate: 
                </td>
                <td>
                    <asp:TextBox ID="TextBoxInterestRate" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxInterestRate" ErrorMessage="Interest Rate cannot be blank!" CssClass="text-danger" ValidationGroup="ComputeGroup"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBoxInterestRate" ErrorMessage="Enter only numbers/Enter positive numbers only!" ForeColor="Red" ValidationExpression="[0-9]+(\.[0-9][0-9]?)?"></asp:RegularExpressionValidator>
                    <br />
                    %
                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="TextBoxInterestRate" ErrorMessage="Enter value greater than zero and less than 100,000,000" ForeColor="Red" Type="Double" MaximumValue="100000000" MinimumValue="0.10"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Interest Type: 
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonListInterestType" CssClass="radio" runat="server" AutoPostBack="true">
                        <asp:ListItem>Simple Interest</asp:ListItem>
                        <asp:ListItem>Compound Interest</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="RadioButtonListInterestType" ErrorMessage="Select one Interest Type!" CssClass="text-danger" ValidationGroup="ComputeGroup"></asp:RequiredFieldValidator>
                    <br />
                    Interest Calculation Method:<br />
                    <asp:DropDownList ID="DropDownListInterestCalculationMethod" runat="server" AutoPostBack="true" CssClass="auto-style2" OnSelectedIndexChanged="DropDownListInterestCalculationMethod_SelectedIndexChanged">
                        <asp:ListItem>Quarterly</asp:ListItem>
                        <asp:ListItem>Half Yearly</asp:ListItem>
                        <asp:ListItem>Yearly</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Select one Interest Calculation Method!"  ControlToValidate="DropDownListInterestCalculationMethod" CssClass="text-danger" ValidationGroup="ComputeGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:button id="buttonclear" runat="server" text="clear" onclick="buttonclear_click" CausesValidation="false" />
                    <%--<input id="Reset1" type="reset" value="reset" />--%>
                </td> 
                <td>
                    <asp:Button ID="ButtonCompute" runat="server" Text="Compute" OnClick="ButtonCompute_Click" ValidationGroup="ComputeGroup"/>
                </td>
                <td>
                    <%--<asp:Label ID="LabelCalendarStatus" runat="server" ForeColor="Red"></asp:Label>--%>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <strong><u>Results</u></strong>
                </td>
            </tr>
        </table>
  </div>
        
     <script>
            $(document).ready(function () {
                //Attach change eventhandler
                $('#dtpDate').on('change.bfhdatepicker', function (e) {
                    //Assign the value to Hidden Variable
                    $('#hdnSelectedDate').val($('#dtpDate').val());
                });
            });
        </script>
        <div class="bfh-datepicker" data-format="d/m/y" data-date="today" id="dtpDate" runat="server">
        </div>
        <%--Add this control to your page--%>
        <input id="hdnSelectedDate" type="hidden" runat="server" />
        
    <div style="padding-left:20px">
        <asp:Table ID="TableDuration" runat="server" CssClass="table-bordered"></asp:Table>
        <asp:Table ID="MainTable" runat="server" CssClass="table-bordered" ></asp:Table>
        <asp:Table ID="TableMonths" runat="server" CssClass="table-bordered" ></asp:Table>
        <asp:Table ID="LastTable" runat="server" CssClass="table-bordered"></asp:Table>
    </div>
        
   
        </asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <%--    <style type="text/css">
        .auto-style1 {
            width: 182px;
        }
    </style>--%>
    <style type="text/css">
        .auto-style1 {
            position: relative;
            left: 0px;
            top: 1px;
        }
        .auto-style2 {
            position: relative;
            left: 3px;
            top: 0px;
        }
        .auto-style3 {
            height: 82px;
        }
    </style>
</asp:Content>
