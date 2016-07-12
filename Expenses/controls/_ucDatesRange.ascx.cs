using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Expenses
{
    public partial class _ucDatesRange : System.Web.UI.UserControl
    {
        public Int32 userID { get; set; }
        public LibEnum.eFormTypes formType { get; set; }
        public LibEnum.eReportTypes reportType { get; set; }
        public HtmlTable HtmlTableControl { get; set; }
        public string reportMonth { get; set; }
        public string reportYear { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DateTime dtFrom;
                DateTime dtTo;

                if (reportType == LibEnum.eReportTypes.SumByMonth)
                {
                     dtFrom = Convert.ToDateTime(reportMonth + "/1/" + reportYear);
                     dtTo = dtFrom.AddMonths(1).AddDays(-1);
                }
                else 
                {
                     dtFrom = Convert.ToDateTime("1/1/" + reportYear);

                     if (reportType == LibEnum.eReportTypes.SumByYTD)
                     {
                         dtTo = Convert.ToDateTime(reportMonth + "/1/" + reportYear).AddMonths(1).AddDays(-1);
                     }
                     else
                     {
                         dtTo = Convert.ToDateTime("12/31/" + reportYear);
                     }
                }
                
               
                dpDateFrom.Value = string.Format("{0:MM/dd/yyyy}", dtFrom);
                dpDateTo.Value = string.Format("{0:MM/dd/yyyy}", dtTo);

                LibCode.setExpensesMonthlyReport(HtmlTableControl, Convert.ToDateTime(dpDateFrom.Value),Convert.ToDateTime(dpDateTo.Value), userID);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            LibCode.setExpensesMonthlyReport(HtmlTableControl, Convert.ToDateTime(dpDateFrom.Value), Convert.ToDateTime(dpDateTo.Value), userID);
        }
    }
}