using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MySavingsApp
{
    public partial class DatePicker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            DateTime fromDate = Convert.ToDateTime(TextBoxBirthDate.Text);
            DateTime toDate = Convert.ToDateTime(TextBoxToDate.Text);
            double numberOfDays = (toDate - fromDate).TotalDays;
            Response.Write(numberOfDays);
        }
    }
}