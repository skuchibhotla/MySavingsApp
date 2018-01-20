using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MySavingsApp
{
    public partial class Home : System.Web.UI.Page
    {
        static string fullName;
        static int userID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["fullName"] != null) || (Session["userID"] != null))
            {
                fullName = Session["fullName"].ToString();
                userID = Convert.ToInt32(Session["userID"]);
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void ButtonInterestCalculator_Click(object sender, EventArgs e)
        {
            Response.Redirect("InterestCalculator.aspx");
        }

        protected void ButtonSavingsTracker_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrackSavings.aspx");
        }
    }
}