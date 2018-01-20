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
    public partial class AdminPage : System.Web.UI.Page
    {
        static string fullName;
        static int userID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                Response.Redirect("Login.aspx");

            }

            String connectionString = ConfigurationManager.ConnectionStrings["MySavingsAppDBConnectionString"].ConnectionString;
            OleDbConnection con = new OleDbConnection(connectionString);

            OleDbCommand view_cmd = new OleDbCommand("select * from tbl_users", con);
            OleDbDataAdapter olda = new OleDbDataAdapter(view_cmd);

            DataTable dt = new DataTable();
            olda.Fill(dt);

            GridViewUsersAdmin.DataSource = dt;
            GridViewUsersAdmin.DataBind();

            //string emailID = Request.QueryString["emailID"];

            //LabelEmailID.Text = emailID;

            if((Session["fullName"] != null) || (Session["userID"] != null))
            {
                fullName = Session["fullName"].ToString();
                userID = Convert.ToInt32(Session["userID"]);
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            
        }

        protected void ButtonLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void ButtonTrackSavings_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrackSavings.aspx");
        }

        protected void ButtonInterestCalculator_Click(object sender, EventArgs e)
        {
            Response.Redirect("InterestCalculator.aspx");
        }

        protected void GridViewUsersAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}