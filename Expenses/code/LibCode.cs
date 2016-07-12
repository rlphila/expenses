using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Data.EntityClient;
using System.Web.Services;
using System.Text;
using System.Web.Security;

namespace Expenses
{
    public class LibCode
    {

        public static Int32 getExpensesUserID() 
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var currentUser = Membership.GetUser(System.Web.HttpContext.Current.User.Identity.Name);
                using (Expenses.data.expensesEntities db = new Expenses.data.expensesEntities())
                {
                    db.Connection.Open();

                    string aspUserId = currentUser.ProviderUserKey.ToString();

                    var expUser = (from x in db.exp_user.AsEnumerable()
                                   where x.AspUserId.ToString() == aspUserId
                                   select x).ToList();

                    if (expUser != null && expUser.Count > 0)
                    {
                        return expUser[0].UserId;
                    }
                    else
                    {
                        return 0;
                    }
                   
                }
            }
            else
            {
                return 0;
            }
        }

        public static Dictionary<string, int> getYearList()
        { 
         Dictionary<string, int> lstYear = new Dictionary<string, int>();
            int curYear = DateTime.Now.Year;

            for (int i = 2003; i <= curYear+5; i++){
                lstYear.Add(Convert.ToString(i), i);
            }

            return lstYear;
        }

        public static Dictionary<string, Int16> getMonthList()
        {
            Dictionary<string, Int16> lstMonth = new Dictionary<string, Int16>();

            string m;

            for (int i = 1; i <= 12; i++){
                m  = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                lstMonth.Add(m, Convert.ToInt16(i));
            }

            return lstMonth;


        }

        public static void setExpensesMonthlyData(HtmlTable t, Int16 m, Int16 y, Int32 ctgID, Int32 subCtgID, Int32 UserID,
            System.Web.UI.WebControls.Label lblSumTotal, System.Web.UI.WebControls.Label lblSumYTD,
            System.Web.UI.WebControls.Label lblSumYear)
        {

            DateTime d = new DateTime(y, m, 1);
            Boolean isNextRow = false;
            int priorDay;

            //get month expenses records
            List<Expenses.LibStruc.strucExpensesViewData> monthlyData = 
                                        Expenses.LibDB.getExpensesMonthlyData(d, ctgID, subCtgID, UserID);

            do 
            {
            isNextRow = false;
            HtmlTableRow r = new HtmlTableRow();
            t.Rows.Add(r);
            int i;
            //add empty cells before month starts
            if (d.DayOfWeek != DayOfWeek.Sunday && d.Day == 1)
              { 
                int dow = (int)d.DayOfWeek;
                priorDay = dow * (-1);
                for (i=0; i<dow; i++)
                {
                   HtmlTableCell emptycell = new HtmlTableCell();
                   emptycell.Attributes.Add("class", "exp-cal-table-cell-dif-month");
                    var q1 = (from x in monthlyData where x.YmdAdd == d.AddDays(priorDay) select x).ToList();
                    StringBuilder sb1 = new StringBuilder();
                    sb1.Append("<a onclick='fnOpenCalDialog(" + d.AddDays(priorDay).ToString("yyyyMMdd") + ",1)' href='#' title='Click to Add New or Update'>" + Convert.ToString(d.AddDays(priorDay).Day) + "</a>");

                   if (q1 != null && q1.Count > 0)
                   {
                       StringBuilder sbTemp1 = new StringBuilder();
                       decimal iTotalByDay = 0;
                       sbTemp1.Append("<div class='exp-cal-div-day-detail'>");
                       for (int i1 = 0; i1 < q1.Count; i1++)
                       {
                           sbTemp1.Append(String.Format("{0:+$#,##0.00;-$#,##0.00;$0.00}", q1[i1].AmtExp) + " - " + q1[i1].CtgName + "-" + q1[i1].SubCtgName + "<br />");
                           iTotalByDay += q1[i1].AmtExp;
                       }
                       sbTemp1.Append("</div>");
                       sb1.Append("<div class='exp-amount-total'>" + String.Format("{0:+$#,##0.00;-$#,##0.00;$0.00}", iTotalByDay) + "</div>");
                       sb1.Append(sbTemp1);

                   }

                   emptycell.InnerHtml = Convert.ToString(sb1);
                   r.Cells.Add(emptycell);
                     priorDay = priorDay+1;
               }
              }
                    do
                    {
                        HtmlTableCell c = new HtmlTableCell();
                        c.InnerText = Convert.ToString(d.Day);
                        if (d.Month != m)
                        {
                            c.Attributes.Add("class", "exp-cal-table-cell-dif-month");
                        }

                        var q2 = (from x in monthlyData where x.YmdAdd == d select x).ToList();
                        StringBuilder sb2 = new StringBuilder();
                        sb2.Append("<a onclick='fnOpenCalDialog(" + d.ToString("yyyyMMdd") + ",1)' href='#' title='Click to Add New or Update'>" + Convert.ToString(d.Day) + "</a>");

                        if (q2 != null && q2.Count > 0)
                        {
                            StringBuilder sbTemp2 = new StringBuilder();
                            decimal iTotalByDay=0;
                            sbTemp2.Append("<div class='exp-cal-div-day-detail'>");
                            for (int i2 = 0; i2 < q2.Count; i2++)
                            {
                                sbTemp2.Append(String.Format("{0:+$#,##0.00;-$#,##0.00;$0.00}", q2[i2].AmtExp) + " - " + q2[i2].CtgName + "-" + q2[i2].SubCtgName + "<br />");
                                iTotalByDay += q2[i2].AmtExp;
                            }
                            sbTemp2.Append("</div>");
                            sb2.Append("<div class='exp-amount-total'>" + String.Format("{0:+$#,##0.00;-$#,##0.00;$0.00}", iTotalByDay) + "</div>");
                            sb2.Append(sbTemp2);
                        }
                        c.InnerHtml = Convert.ToString(sb2);
                        r.Cells.Add(c);
                        if (d.DayOfWeek == DayOfWeek.Saturday)
                        {
                            isNextRow = true;
                            d = d.AddDays(1);
                        }
                        else
                        {
                            d = d.AddDays(1);
                        }

                    } while (isNextRow == false);

            }while (d.Month == m);

           var mySum = LibDB.selectExpensesSummary(m, y,ctgID, subCtgID, UserID);

            lblSumTotal.Text = mySum.amtTotal;
            lblSumYTD.Text = mySum.amtYTD;
            lblSumYear.Text =  mySum.amtYearEnd;

        }
       
        public static void setExpensesMonthlyReport(HtmlTable t, DateTime dtFrom,  DateTime dtTo, Int32 UserID)
        {
            //get month expenses records
            List<Expenses.LibStruc.strucExpensesReportData> reportData = 
                Expenses.LibDB.getExpensesReportByDatesRange(dtFrom, dtTo, UserID);

            foreach (LibStruc.strucExpensesReportData record in reportData)
            {
                string divStart = string.Empty, divEnd = "</div>";
                string ctgName = string.Empty, subctgName = string.Empty;
                string sumamtExp = string.Empty;

                if (record.SortOrder == 0) //show just ctg name on top of each group
                {
                    divStart = "<div class='exp-rpt-ctg'>";
                    ctgName = record.CtgName;
                }
                else if (record.SortOrder == 1) //details level
                {
                    divStart = "<div class='exp-rpt-detail-text'>";
                    sumamtExp = "<div class='exp-rpt-detail-amount'>" + String.Format("{0:+$#,##0.00;-$#,##0.00;$0.00}", record.SumAmtExp) + "</div>";
                }
                else
                {
                    if (record.SumAmtExp > 0)
                    {
                        sumamtExp = "<div class='exp-rpt-total-amount-plus'>";
                    }
                    else
                    {
                        sumamtExp = "<div class='exp-rpt-total-amount-minus'>";
                    }

                    sumamtExp = sumamtExp + String.Format("{0:+$#,##0.00;-$#,##0.00;$0.00}", record.SumAmtExp) + divEnd;

                    divStart = "<div class='exp-rpt-sub-total-text'>";

                }
                HtmlTableRow r = new HtmlTableRow();
                t.Rows.Add(r);
                HtmlTableCell c1 = new HtmlTableCell();
                c1.InnerHtml = divStart + ctgName + divEnd;
                r.Cells.Add(c1);
                HtmlTableCell c2 = new HtmlTableCell();
                c2.InnerHtml = divStart + record.SubCtgName + divEnd;
                r.Cells.Add(c2);
                HtmlTableCell c3 = new HtmlTableCell();
                c3.InnerHtml = Convert.ToString(sumamtExp);
                r.Cells.Add(c3);
            }

            //sbTemp2.Append("</div>");
            //sb2.Append("<div class='exp-amount-total'>" + String.Format("{0:+$#,##0.00;-$#,##0.00;$0.00}", iTotalByDay) + "</div>");
        }

        public static string MyDictionaryToJson(Dictionary<string, string> dict)
        {
            return string.Join(",", dict.Select(
                d => string.Format("{{ {0} : [ {1} ] }}", d.Key,
                    string.Join(",", d.Value.Select(i => i.ToString()).ToArray())
                )).ToArray());
        }

        public class jsonIdValueSet
        {
           public string FieldId {get;set;}
           public string FieldValue { get; set; }

           public jsonIdValueSet(string fieldId, string fieldValue)
           {
               FieldId = fieldId;
               FieldValue = fieldValue;
           }
        }

    }
}