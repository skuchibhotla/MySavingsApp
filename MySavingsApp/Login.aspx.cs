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
    public partial class Login : System.Web.UI.Page
    {
        static int userID; 

        protected void Page_Load(object sender, EventArgs e)
        {
          if(Request.QueryString.Count > 0)
            {
                TextBoxEmailID.Text = Request.QueryString["EmailID"];
            }
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["MySavingsAppDBConnectionString"].ConnectionString;
            OleDbConnection con = new OleDbConnection(connectionString);

            con.Open();

            string check_User = "select count(*) from tbl_users where emailID = '" + TextBoxEmailID.Text + "'";
            OleDbCommand cmd = new OleDbCommand(check_User, con);

            int count_Users = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            if(count_Users == 1)        // User already exists!
            {
                string checkPasswordQuery = "select password from tbl_users where emailID = '" + TextBoxEmailID.Text + "'";
                OleDbCommand checkPasswordCommand = new OleDbCommand(checkPasswordQuery, con);

                string dbPassword = checkPasswordCommand.ExecuteScalar().ToString();
                if(dbPassword == TextBoxPassword.Text)
                {
                    //Response.Write("Login Successful!");
                    //LabelLoginStatus.Text = "Login Successful!";


                    string fullNameQuery = "select fullName from tbl_users where emailID = '" + TextBoxEmailID.Text + "'";
                    OleDbCommand fullNameCommand = new OleDbCommand(fullNameQuery, con);
                    string fullName = fullNameCommand.ExecuteScalar().ToString();

                    string userIDQuery = "select userID from tbl_users where emailID = '" + TextBoxEmailID.Text + "'";
                    OleDbCommand userIDCommand = new OleDbCommand(userIDQuery, con);
                    userID = Convert.ToInt32(userIDCommand.ExecuteScalar());

                    //Session variables
                    Session["fullName"] = fullName;
                    Session["userID"] = userID;

                    //// To redirect ADMIN to admin.aspx page.
                    if (TextBoxEmailID.Text == "shashank.kuchibhotla@gmail.com" && TextBoxPassword.Text == "123")
                    {

                        Response.Redirect("AdminPage.aspx");
                        //Response.Redirect("AdminPage.aspx ? emailID = " + TextBoxEmailID.Text);

                    }

                    else
                    {
                        Response.Redirect("Home.aspx");
                    }
                   
                }

                else
                {
                    //Response.Write("Login Failed!");
                    LabelLoginStatus.Text = "Login Failed: Password incorrect!";
                }
            }

            else
            {
                //Response.Write("User/Email ID does not exist!");
                LabelLoginStatus.Text = "Login Failed: User/Email ID does not exist/is incorrect!";
            }

            con.Close();
        }
    }
}