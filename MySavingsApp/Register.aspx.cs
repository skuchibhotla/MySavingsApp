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
    public partial class Register : System.Web.UI.Page
    {
        //OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\shashank.kuchibhotla\Documents\MySavingsAppDB.mdb");
        static string tbl_name = "tbl_users", connectionString;
        static int countInputs;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }


        }

        // Defining Connection String and accessing it from Web.config
        public static string GetConnString()
        {
            return ConfigurationManager.ConnectionStrings["MySavingsAppDBConnectionString"].ConnectionString;
        }


        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
            try
            {
                connectionString = GetConnString();
                string checkUser = "select count(*) from tbl_users where emailID = ?";

                using (OleDbConnection con = new OleDbConnection(connectionString))
                {
                    con.Open();
                    using (OleDbCommand checkUserCommand = new OleDbCommand(checkUser, con))
                    {
                        checkUserCommand.CommandType = CommandType.Text;
                        checkUserCommand.Parameters.AddWithValue("emailID", TextBoxEmailID.Text);
                        countInputs = Convert.ToInt32(checkUserCommand.ExecuteScalar());
                        if (countInputs == 1)    // Another entry already exists.
                        {
                            LabelRegistrationStatus.Text = "Registration Failed! Email ID already exists!";
                            con.Close();
                            return;
                        }

                        else
                        {
                            connectionString = GetConnString();
                            string insertQuery = "insert into tbl_users (fullName, emailID, [password]) values(?, ?, ?)";
                            using (OleDbConnection conn = new OleDbConnection(connectionString))
                            {
                                conn.Open();
                                using (OleDbCommand insertCommand = new OleDbCommand(insertQuery, conn))
                                {
                                    insertCommand.CommandType = CommandType.Text;
                                    insertCommand.Parameters.AddWithValue("fullName", TextBoxFullName.Text);
                                    insertCommand.Parameters.AddWithValue("emailID", TextBoxEmailID.Text);
                                    insertCommand.Parameters.AddWithValue("password", TextBoxPassword.Text);

                                    insertCommand.ExecuteNonQuery();

                                    //TextBoxFullName.Text = string.Empty;
                                    string emailIDQS = TextBoxEmailID.Text;

                                    //string status = "Success";
                                    Response.Redirect("~/Login.aspx?EmailID=" + emailIDQS);
                                    //Response.Redirect("Login.aspx");


                                }
                                conn.Close();
                            }
                        }
                    }
                }   
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally
            {

            }
        }
    }
}

//                String connectionString = ConfigurationManager.ConnectionStrings["MySavingsAppDBConnectionString"].ConnectionString;
//                OleDbConnection con = new OleDbConnection(connectionString);

//                con.Open();

//                string checkUser = "select count(*) from tbl_users where emailID = '" + TextBoxEmailID.Text + "'";

//                OleDbCommand checkUser_cmd = new OleDbCommand(checkUser, con);

//                int countInputs = Convert.ToInt32(checkUser_cmd.ExecuteScalar().ToString());
//                if (countInputs == 1)    // Another entry already exists.
//                {
//                    LabelRegistrationStatus.Text = "Email ID already exists!";
//                    return;

//                }

//                OleDbCommand register_command = new OleDbCommand("insert into tbl_users (fullName, emailID, password) values('" + TextBoxFullName.Text + "','" + TextBoxEmailID.Text + "','" + TextBoxPassword.Text + "')", con);

//                register_command.ExecuteNonQuery();

//                LabelRegistrationStatus.Text = "User registered successfully!";

//                con.Close();

//                TextBoxFullName.Text = string.Empty;
//                //TextBoxLastName.Text = string.Empty;
//                TextBoxEmailID.Text = string.Empty;
//                TextBoxPassword.Text = string.Empty;
//                TextBoxConfirmPassword.Text = string.Empty;
//            }

//            catch (Exception ex)
//            {
//                LabelRegistrationStatus.Text = "Error! User could not be registered. Error message: " + ex.ToString();
//            }

//            finally
//            {

//            }
//        }

//        protected void TextBoxFirstName_TextChanged(object sender, EventArgs e)
//        {

//        }
//    }
//}