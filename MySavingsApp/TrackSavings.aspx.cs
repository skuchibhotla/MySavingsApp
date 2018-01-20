using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

namespace MySavingsApp
{
    public partial class TrackSavings : System.Web.UI.Page
    {
        public string table_name = "tbl_savings";       // Table Name. 

        static string interestCalculationMethod, firstname, lastname, dataEnteredByFullName, getOwnerFullNameQuery, ownerFullName;

        static int loginUserID, dataEnteredByUserID, rowCount;
        static bool selectButtonWasClicked;

        protected void ButtonClearAll_Click(object sender, EventArgs e)
        {
            TextBoxCreditorName.Text = string.Empty;
            TextBoxPrincipalAmount.Text = string.Empty;
            TextBoxPrincipalAmount.Text = string.Empty;
            TextBoxInterestRate.Text = string.Empty;
            RadioButtonListInterestType.SelectedValue = "Simple Interest";
            DropDownListInterestCalculationMethod.Visible = false;
            DropDownListInterestCalculationMethod.Enabled = false;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //Retrieving session variables

            if ((Session["fullName"] != null) || (Session["userID"] != null))
            {
                dataEnteredByFullName = Session["fullName"].ToString();
                LabelDataEnteredBy.Text = Session["fullName"].ToString();
                loginUserID = Convert.ToInt32(Session["userID"]);
            }

            if(HttpContext.Current.Session.Count == 0)
            {
                Response.Redirect("Login.aspx");

            }

            ShowGridView();

            //Reading user ID from login
            
            if(IsPostBack)
            {
                if (RadioButtonListInterestType.SelectedValue == "Compound Interest")
                {
                    DropDownListInterestCalculationMethod.Enabled = true;
                    DropDownListInterestCalculationMethod.Visible = true;
                }

                else
                {
                    DropDownListInterestCalculationMethod.Enabled = false;
                    DropDownListInterestCalculationMethod.Visible = false;
                }
            }

            // Loading
            if (!IsPostBack)
            {
                LoadDropDownListOwner();
            }
        }

        // Defining Connection String and accessing it from Web.config
        public static string GetConnString()
        {
            return ConfigurationManager.ConnectionStrings["MySavingsAppDBConnectionString"].ConnectionString;
        }

        // Populating the data in the GridView()
        protected void ShowGridView()
        {
            try
            {
                string connectionString = GetConnString();
                OleDbConnection con = new OleDbConnection(connectionString);
                con.Open();

                //To Test out the value of loginUserID that is coming from the Session
                int tempID = loginUserID;
                string tempFullNameFromSession = dataEnteredByFullName;

                //string showGridViewQuery = "select * from tbl_savings";

                string showGridViewQuery = "select savingsID, creditorName, amount, interestRate, interestType, interestCalculationMethod, insertedDate, owner, dataEnteredBy, status from(select savingsID, creditorName, amount, interestRate, interestType, interestCalculationMethod, insertedDate, dataEnteredByID, o.fullName as owner, u.fullName as dataEnteredBy, status from((tbl_savings as s) left join tbl_users as o on s.ownerID = o.userID) left join tbl_users as u on s.dataEnteredByID = u.userID where dataEnteredByID = " + loginUserID + ") UNION select savingsID, creditorName, amount, interestRate, interestType, interestCalculationMethod, insertedDate, owner, dataEnteredBy, status from(select savingsID, creditorName, amount, interestRate, interestType, interestCalculationMethod, insertedDate, ownerID, o.fullName as owner, u.fullName as dataEnteredBy, status from((tbl_savings as s) left join tbl_users as o on s.ownerID = o.userID) left join tbl_users as u on s.dataEnteredByID = u.userID where ownerID = " + loginUserID + ")";



                OleDbCommand showGridViewCommand = new OleDbCommand(showGridViewQuery, con);
                showGridViewCommand.ExecuteNonQuery();
                
                DataTable dt = new DataTable();
                
                OleDbDataAdapter olda = new OleDbDataAdapter(showGridViewCommand);
                olda.Fill(dt);
                rowCount = dt.Rows.Count;
                if (dt.Rows.Count == 0)
                {
                    LabelStatus.Text = "No entries!";
                }
                GridViewSavingsTracker.DataSource = dt;
                GridViewSavingsTracker.DataBind();

                con.Close();

            }

            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            finally
            {

            }
        }

        // What happens when ButtonClear is clicked!
        //protected void ButtonClear_Click(object sender, EventArgs e)
        //{
        //    TextBoxCreditorName.Text = string.Empty;
        //    TextBoxPrincipalAmount.Text = string.Empty;
        //    TextBoxPrincipalAmount.Text = string.Empty;
        //    TextBoxInterestRate.Text = string.Empty;
        //}


        // Inserting new data into the database
        protected void ButtonSave_Click1(object sender, EventArgs e)
        {
            try
            {
                if (selectButtonWasClicked == false) //Inserting new data.
                {
                    string connectionString = GetConnString();
                    if (RadioButtonListInterestType.SelectedValue == "Simple Interest")
                    {
                        //string connString = GetConnString();
                        string insertQuery = "insert into tbl_savings (creditorName, amount, interestRate, interestType, insertedDate, ownerID, dataEnteredByID, status) values(?, ?, ?, ?, ?, ?, ?, ?)";
                        using (OleDbConnection con = new OleDbConnection(connectionString))
                        {
                            con.Open();
                            using (OleDbCommand insertCommand = new OleDbCommand(insertQuery, con))
                            {
                                insertCommand.CommandType = CommandType.Text;
                                insertCommand.Parameters.AddWithValue("creditorName", TextBoxCreditorName.Text);
                                insertCommand.Parameters.AddWithValue("amount", TextBoxPrincipalAmount.Text);
                                insertCommand.Parameters.AddWithValue("interestRate", TextBoxInterestRate.Text);
                                insertCommand.Parameters.AddWithValue("interestType", RadioButtonListInterestType.SelectedValue);
                                insertCommand.Parameters.AddWithValue("insertedDate", DateTime.Now.ToShortDateString());
                                insertCommand.Parameters.AddWithValue("ownerID", DropDownListOwner.SelectedValue);
                                insertCommand.Parameters.AddWithValue("dataEnteredByID",loginUserID);
                                insertCommand.Parameters.AddWithValue("status", DropDownListStatus.SelectedValue);
                                insertCommand.ExecuteNonQuery();

                                con.Close();
                                LabelStatus.Text = "Data successfully inserted!";
                            }
                        }
                    }

                    else if (RadioButtonListInterestType.SelectedValue == "Compound Interest")
                    {
                        string connString = GetConnString();
                        string insertQuery = "insert into " + table_name + " (creditorName, amount, interestRate, interestType, interestCalculationMethod, insertedDate, ownerID, dataEnteredByID, status) values(?, ?, ?, ?, ?, ?, ?, ?, ?)";
                        //string connectionString = GetConnString();
                        using (OleDbConnection con = new OleDbConnection(connectionString))
                        {
                            con.Open();
                            using (OleDbCommand insertCommand = new OleDbCommand(insertQuery, con))
                            {
                                insertCommand.CommandType = CommandType.Text;
                                insertCommand.Parameters.AddWithValue("creditorName", TextBoxCreditorName.Text);
                                insertCommand.Parameters.AddWithValue("amount", TextBoxPrincipalAmount.Text);
                                insertCommand.Parameters.AddWithValue("interestRate", TextBoxInterestRate.Text);
                                insertCommand.Parameters.AddWithValue("interestType", RadioButtonListInterestType.SelectedValue);
                                insertCommand.Parameters.AddWithValue("interestCalculationMethod", DropDownListInterestCalculationMethod.SelectedValue);
                                insertCommand.Parameters.AddWithValue("insertedDate", DateTime.Now.ToShortDateString());
                                insertCommand.Parameters.AddWithValue("ownerID", DropDownListOwner.SelectedValue);
                                insertCommand.Parameters.AddWithValue("dataEnteredByID", loginUserID);
                                insertCommand.Parameters.AddWithValue("status", DropDownListStatus.SelectedValue);
                                insertCommand.ExecuteNonQuery();

                                con.Close();
                                LabelStatus.Text = "Data successfully inserted!";
                            }
                        }
                    }

                    ShowGridView();
                }
                
                
                if (selectButtonWasClicked == true)
                {
                    int rowIndex = GridViewSavingsTracker.SelectedRow.RowIndex;
                    
                    int tupleID = Convert.ToInt32(GridViewSavingsTracker.DataKeys[rowIndex].Values[0]);

                    string connectionString = GetConnString();

                    if (RadioButtonListInterestType.SelectedValue == "Simple Interest")
                    {
                        string updateQuery = "UPDATE " + table_name + " SET creditorName = ?, amount = ?, interestRate = ?, interestType = ?, ownerID = ?, status = ? WHERE savingsID = ?";

                        using (OleDbConnection con = new OleDbConnection(connectionString))
                        {
                            con.Open();
                            using (OleDbCommand updateCommand = new OleDbCommand(updateQuery, con))
                            {
                                updateCommand.CommandType = CommandType.Text;
                                updateCommand.Parameters.AddWithValue("creditorName", TextBoxCreditorName.Text);
                                updateCommand.Parameters.AddWithValue("amount", TextBoxPrincipalAmount.Text);
                                updateCommand.Parameters.AddWithValue("interestRate", TextBoxInterestRate.Text);
                                updateCommand.Parameters.AddWithValue("interestType", RadioButtonListInterestType.SelectedValue);
                                updateCommand.Parameters.AddWithValue("interestCalculationMethod", DropDownListInterestCalculationMethod.SelectedValue);
                                updateCommand.Parameters.AddWithValue("ownerID", DropDownListOwner.SelectedValue);
                                updateCommand.Parameters.AddWithValue("status", DropDownListStatus.SelectedValue);
                                updateCommand.Parameters.AddWithValue("savingsID", tupleID);
                                //OleDbCommand updateCommand = new OleDbCommand(updateQuery, con);

                                updateCommand.ExecuteNonQuery();
                                con.Close();
                                LabelStatus.Text = "Data successfully updated!";
                            }
                        }
                    }

                    else if (RadioButtonListInterestType.SelectedValue == "Compound Interest")
                    {
                        string updateQuery = "UPDATE " + table_name + " SET creditorName = ?, amount = ?, interestRate = ?, interestType = ?, interestCalculationMethod = ?, ownerID = ?, status = ? WHERE savingsID = ?";
                        using (OleDbConnection con = new OleDbConnection(connectionString))
                        {
                            con.Open();
                            using (OleDbCommand updateCommand = new OleDbCommand(updateQuery, con))
                            {
                                updateCommand.CommandType = CommandType.Text;
                                updateCommand.Parameters.AddWithValue("creditorName", TextBoxCreditorName.Text);
                                updateCommand.Parameters.AddWithValue("amount", TextBoxPrincipalAmount.Text);
                                updateCommand.Parameters.AddWithValue("interestRate", TextBoxInterestRate.Text);
                                updateCommand.Parameters.AddWithValue("interestType", RadioButtonListInterestType.SelectedValue);
                                updateCommand.Parameters.AddWithValue("interestCalculationMethod", DropDownListInterestCalculationMethod.SelectedValue);
                                updateCommand.Parameters.AddWithValue("ownerID", DropDownListOwner.SelectedValue);
                                updateCommand.Parameters.AddWithValue("status", DropDownListStatus.SelectedValue);
                                updateCommand.Parameters.AddWithValue("savingsID", tupleID);
                                //OleDbCommand updateCommand = new OleDbCommand(updateQuery, con);

                                updateCommand.ExecuteNonQuery();
                                con.Close();
                                LabelStatus.Text = "Data successfully updated!";
                                selectButtonWasClicked = false;
                            }
                        }
                    }

                    ShowGridView();
                
                }
 
            }

            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

            finally
            {
                //con.Close();
            }


        }

        // Delete Data
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = GetConnString();
                string deleteQuery = "DELETE from " + table_name + " where creditorName = ? and amount = ? and interestRate = ? and interestType = ?";

                using (OleDbConnection con = new OleDbConnection(connectionString))
                {
                    con.Open();

                    using (OleDbCommand deleteCommand = new OleDbCommand(deleteQuery, con))
                    {
                        deleteCommand.CommandType = CommandType.Text;
                        deleteCommand.Parameters.AddWithValue("creditorName", TextBoxCreditorName.Text);
                        deleteCommand.Parameters.AddWithValue("amount", TextBoxPrincipalAmount.Text);
                        deleteCommand.Parameters.AddWithValue("interestRate", TextBoxInterestRate.Text);
                        deleteCommand.Parameters.AddWithValue("interestType", RadioButtonListInterestType.SelectedValue);
                        //deleteCommand.Parameters.AddWithValue("interestCalculationMethod", DropDownListInterestCalculationMethod.SelectedValue);
                        //deleteCommand.Parameters.AddWithValue("interestCalculationMethod", interestCalculationMethod);

                        deleteCommand.ExecuteNonQuery();
                        selectButtonWasClicked = false;

                        TextBoxCreditorName.Text = string.Empty;
                        TextBoxPrincipalAmount.Text = string.Empty;
                        TextBoxPrincipalAmount.Text = string.Empty;
                        TextBoxInterestRate.Text = string.Empty;
                        RadioButtonListInterestType.SelectedValue = "Simple Interest";
                        DropDownListInterestCalculationMethod.Enabled = false;
                        DropDownListInterestCalculationMethod.Visible = false; 
                    }
                    con.Close();
                    LabelStatus.Text = "Data successfully deleted!";
                }

                ShowGridView();
            }

            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

            finally
            {

            }
        }

        // To fill the fields with data when select option is clicked from GridView.
        protected void GridViewSavingsTracker_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                selectButtonWasClicked = true;
                int rowID = Convert.ToInt32(e.CommandArgument);
                var row = GridViewSavingsTracker.Rows[rowID];

                string principalAmountWithSymbol = ((Literal)row.FindControl("PrincipalAmount")).Text;
                char dollarSymbol = '$';
                string amount = principalAmountWithSymbol.TrimStart(dollarSymbol);
                TextBoxPrincipalAmount.Text = amount;

                TextBoxCreditorName.Text = GridViewSavingsTracker.Rows[rowID].Cells[1].Text;
                
                
                //TextBoxPrincipalAmount.Text = GridViewSavingsTracker.Rows[rowID].Cells[2].Text;
                //TextBoxInterestRate.Text = GridViewSavingsTracker.Rows[rowID].Cells[3].Text;


               
                string intRateWithSymbol = ((Literal)row.FindControl("IntRate")).Text;
                char percentSymbol = '%';
                string intRate = intRateWithSymbol.TrimEnd(percentSymbol);
                TextBoxInterestRate.Text = intRate;

                //string interestRateTextBoxContents = (row.FindControl("Interest Rate") as Literal).Text;
                //TextBoxInterestRate.Text = interestRateTextBoxContents;

                var temp = (row.FindControl("interestRate"));
                //TextBoxInterestRate.Text = (Literal)(row.FindControl("interestRate")).
                RadioButtonListInterestType.SelectedValue = GridViewSavingsTracker.Rows[rowID].Cells[4].Text;
                if (RadioButtonListInterestType.SelectedValue == "Simple Interest")
                {
                    DropDownListInterestCalculationMethod.Enabled = false;
                    DropDownListInterestCalculationMethod.Visible = false;
                }
                else if (RadioButtonListInterestType.SelectedValue == "Compound Interest")
                {
                    DropDownListInterestCalculationMethod.SelectedValue = GridViewSavingsTracker.Rows[rowID].Cells[5].Text;
                    DropDownListInterestCalculationMethod.Enabled = true;
                    DropDownListInterestCalculationMethod.Visible = true;
                }
                DropDownListOwner.SelectedItem.Text = GridViewSavingsTracker.Rows[rowID].Cells[7].Text;
                DropDownListStatus.SelectedValue = GridViewSavingsTracker.Rows[rowID].Cells[9].Text;
                //DropDownListInterestCalculationMethod.SelectedValue = GridViewSavingsTracker.Rows[rowID].Cells[5].Text;
                LabelStatus.Text = "Data successfully selected!";
            }

            if(e.CommandName=="Compute")
            {
                int rowID = Convert.ToInt32(e.CommandArgument);

                //DateTime shortDateQS = Convert.ToDateTime(GridViewSavingsTracker.Rows[rowID].Cells[6].Text);
                //string shortDateString = shortDateQS.ToShortDateString();
                //string shortDateString = shortDateQS.ToShortDateString();
                //string shortDateString = DateTime.ParseExact("shortDateQS", "mm/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("mm-dd-yyyy");
                //string shortDateString = DateTime.ParseExact(Request.QueryString["FromDate"], "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("MM-dd-yyyy");
                //String date = DateTime.ParseExact(Request.QueryString["FromDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MM-dd-yyyy");
                //string fromDateString = DateTime.ParseExact("shortDateQS", "mm/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("mm-dd-yyyy");

                var row = GridViewSavingsTracker.Rows[rowID];
                string dateString = GridViewSavingsTracker.Rows[rowID].Cells[6].Text;


                //string amountQS = GridViewSavingsTracker.Rows[rowID].Cells[2].Text;
                string principalAmountWithSymbol = ((Literal)row.FindControl("PrincipalAmount")).Text;
                char dollarSymbol = '$';
                string amount = principalAmountWithSymbol.TrimStart(dollarSymbol);
                string amountQS = amount;



                //string interestRateQS = GridViewSavingsTracker.Rows[rowID].Cells[3].Text;
                
                string intRateWithSymbol = ((Literal)row.FindControl("IntRate")).Text;
                char percentSymbol = '%';
                string intRate = intRateWithSymbol.TrimEnd(percentSymbol);
                string interestRateQS = intRate;



                string interestTypeQS = GridViewSavingsTracker.Rows[rowID].Cells[4].Text;
                string calcTypeQS = GridViewSavingsTracker.Rows[rowID].Cells[5].Text;

                DateTime dateTimeString = Convert.ToDateTime(dateString);
                string shortDateTime = dateTimeString.ToShortDateString();

                Response.Redirect("~/InterestCalculator.aspx?FromDate=" + shortDateTime + "&Amount=" + amountQS + "&intRate=" + interestRateQS + "&intType=" + interestTypeQS + "&calcType=" + calcTypeQS);




                //HttpCookie cookie = new HttpCookie("ComputeCookie");
                //cookie["FromDate"] = GridViewSavingsTracker.Rows[rowID].Cells[6].Text;
                //cookie["PrincipalAmount"] = GridViewSavingsTracker.Rows[rowID].Cells[2].Text;
                //cookie["InterestRate"] = GridViewSavingsTracker.Rows[rowID].Cells[3].Text;
                //cookie["InterestType"] = GridViewSavingsTracker.Rows[rowID].Cells[4].Text;
                //if(GridViewSavingsTracker.Rows[rowID].Cells[4].Text != null)
                //{
                //    cookie["InterestCalculationMethod"] = GridViewSavingsTracker.Rows[rowID].Cells[5].Text;
                //}

                //Response.Cookies.Add(cookie);

                //Response.Redirect("InterestCalculator.aspx");
            }
        }

        protected void TextBoxCreditorName_TextChanged(object sender, EventArgs e)
        {
            e.ToString();
        }

        //protected void GridViewSavingsTracker_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if(e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Label LabelPercentageValue = ((Label)e.Row.FindControl("LabelPercentage"));
        //    }
        //}

        protected void GridViewSavingsTracker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridViewSavingsTracker_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewSavingsTracker.PageIndex = e.NewPageIndex;
            GridViewSavingsTracker.DataBind();
        }

        protected void DropDownListOwner_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ButtonUpdate_Click()
        {

        }


        protected void LoadDropDownListOwner()
        {
            DataTable dt = new DataTable();

            String connectionString = ConfigurationManager.ConnectionStrings["MySavingsAppDBConnectionString"].ConnectionString;
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();
                try
                {
                    OleDbDataAdapter olda = new OleDbDataAdapter("select userID, fullName from tbl_users", con);
                    olda.Fill(dt);

                    
                    DropDownListOwner.DataSource = dt;
                    DropDownListOwner.DataTextField = "fullName";
                    DropDownListOwner.DataValueField = "userID";
                    DropDownListOwner.DataBind();
                }

                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }

                finally
                {

                }

                con.Close();
            }
        }

        //To read the ID from form and get Owner FullName from Database. 
        protected string GetOwnerFullNameFromID()
        {

            String connectionString = ConfigurationManager.ConnectionStrings["MySavingsAppDBConnectionString"].ConnectionString;
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();

                try
                {
                    getOwnerFullNameQuery = "select s.ownerID,u.fullName from tbl_savings s, tbl_users u where s.ownerID = u.userID";

                    OleDbCommand getOwnerFullNameCommand = new OleDbCommand(getOwnerFullNameQuery, con);
                    ownerFullName = getOwnerFullNameCommand.ExecuteScalar().ToString();



                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
                finally
                {

                }
                con.Close();
            }

            return ownerFullName;

        }
    }
}