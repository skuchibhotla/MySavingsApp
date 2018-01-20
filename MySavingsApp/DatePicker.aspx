<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatePicker.aspx.cs" Inherits="MySavingsApp.DatePicker" %>

<link rel="stylesheet" href="Content/bootstrap.min.css" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.0/css/bootstrap-datepicker3.standalone.css" />
<script type="text/javascript" src="Scripts/jquery-1.9.1.min.js"></script>
<script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.1/js/bootstrap-datepicker.min.js"></script>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
    $(document).ready(function () {            
        $("#birthdate").datepicker({
            autoclose: true,
            format: 'yyyy-mm-dd',
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
            format: 'yyyy-mm-dd',
            todayHighlight: true,
            clearBtn: true,
            orientation: 'bottom'
        });
    });
        </script>
</head>
<body>
    <form id="form1" runat="server">
  <div class="container">
    <div class="row">               
        <div class="col-sm-6">
            <fieldset>
                <legend>Registration Form</legend>
                <div class="form-group">                        
                        <label for="usr">Name:</label>
                        <input type="text" class="form-control" id="name" />
                 </div>
                <div class="form-group">                        
                        <label for="usr">Age:</label>
                        <input type="text" class="form-control" id="age" />
                 </div>
                <div class="form-group">                        
                        <label for="usr">Address:</label>
                        <input type="text" class="form-control" id="address" />
                 </div>
                <div class="form-group">  
                        <label for="usr">Birthdate:</label>                      
                        <div class='input-group date' id='birthdate'>
                            <asp:TextBox ID="TextBoxBirthDate" runat="server" Class="form-control" />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>
                <div class="form-group">
                    <label for="usr">ToDate:</label>
                    <div class="input-group date" id="ToDate">
                        <asp:TextBox ID="TextBoxToDate" runat="server" CssClass="form-control"></asp:TextBox>
                        <span class="input-group-addon">
                        <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                    </div> 
                    <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" OnClick="ButtonSubmit_Click" />
                </div>
            </fieldset>                    
        </div>
    </div>
</div>
    </form>
</body>
</html>
