using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Expenses
{
    public partial class report_sum_by_dates_range : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || Convert.ToInt32(Session["UserID"]) == 0)
            {
                Session["UserID"] = Expenses.LibCode.getExpensesUserID();
            }

            string _stringType =  Convert.ToString(Request.QueryString["nType"]);
            LibEnum.eReportTypes _reportType = (LibEnum.eReportTypes) Enum.Parse(typeof(LibEnum.eReportTypes), _stringType,true);

            ucDatesRange1.userID = Convert.ToInt32(Session["UserID"]);
            ucDatesRange1.formType = LibEnum.eFormTypes.ReportForm;
            ucDatesRange1.reportType = _reportType;
            ucDatesRange1.HtmlTableControl = htmlReportTable;
            ucDatesRange1.reportMonth = Convert.ToString(Request.QueryString["nMonth"]);
            ucDatesRange1.reportYear = Convert.ToString(Request.QueryString["nYear"]);

        }
    }
}