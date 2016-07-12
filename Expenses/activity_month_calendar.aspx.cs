using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Expenses
{
    public partial class Activity : System.Web.UI.Page
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
                if (Session["UserID"] == null || Convert.ToInt32(Session["UserID"]) == 0 )
                {
                    Session["UserID"] = Expenses.LibCode.getExpensesUserID();
                }
            }

            ucMonthYear1.userID = Convert.ToInt32(Session["UserID"]);
            ucMonthYear1.formType = LibEnum.eFormTypes.CalendarForm;
            ucMonthYear1.reportType = LibEnum.eReportTypes.SumByMonth;
            ucMonthYear1.lblSumMonth = lblSumMonth;
            ucMonthYear1.lblSumYTD = lblSumYTD;
            ucMonthYear1.lblSumYear = lblSumYear;
            ucMonthYear1.HtmlTableControl = htmlCalendarTable;


            if (!Page.IsPostBack)
            {
                if (Session["FreeSpace"] != null)
                {
                    lblMsg.Text = "You are exceed account limit of " + Session["FreeSpace"] + " free records.";
                    //TODO add logic to check FreeSpace against Records count value
                }
            }
        }
    }
}