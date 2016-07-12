using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Expenses.report
{
    public partial class report_sum_by_month : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || Convert.ToInt32(Session["UserID"]) == 0)
            {
                Session["UserID"] = Expenses.LibCode.getExpensesUserID();
            }

            ucMonthYear1.userID = Convert.ToInt32(Session["UserID"]);
            ucMonthYear1.formType = LibEnum.eFormTypes.ReportForm;
            ucMonthYear1.reportType = LibEnum.eReportTypes.SumByMonth;
            //ucMonthYear1.lblSumMonth = lblSumMonth;
            //ucMonthYear1.lblSumYTD = lblSumYTD;
            //ucMonthYear1.lblSumYear = lblSumYear;
            ucMonthYear1.HtmlTableControl = htmlReportTable;           
        }
    }
}