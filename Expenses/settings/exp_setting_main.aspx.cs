using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Expenses.settings
{
    public partial class setting_main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Session.Abandon();
                Session.Clear();
                FormsAuthentication.SignOut();
                Server.Transfer("~/account/login.aspx", false);
            }
            else
            {
                if (Session["UserID"] == null || Convert.ToInt32(Session["UserID"]) == 0)
                {
                    Session["UserID"] = Expenses.LibCode.getExpensesUserID();
                }
            }
        }
    }
}