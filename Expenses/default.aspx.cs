using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Expenses
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["return"] == null || Request.QueryString["return"] != "true")
            {
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                  Response.Redirect("activity_month_calendar.aspx");
                 //Server.Transfer("activity_month_calendar.aspx", false);
                }
            }
           
        }
    }
}
