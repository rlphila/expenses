using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Expenses
{
    public partial class _ucMonthYear : System.Web.UI.UserControl
    {
        public Int32 userID { get; set; }
        public LibEnum.eFormTypes formType { get; set; }
        public LibEnum.eReportTypes reportType { get; set; }
        public Label lblSumMonth { get; set; }
        public Label lblSumYTD { get; set; }
        public Label lblSumYear { get; set; }
        public HtmlTable HtmlTableControl { get; set; }
        //public string reportMonth { get; set; }
        //public string reportYear { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlMonth.DataSource = LibCode.getMonthList();
                ddlMonth.DataTextField = "key";
                ddlMonth.DataValueField = "value";
                ddlMonth.DataBind();

                ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(Convert.ToString(DateTime.Now.Month)));

                ddlYear.DataSource = LibCode.getYearList();
                ddlYear.DataTextField = "key";
                ddlYear.DataValueField = "value";
                ddlYear.DataBind();

                ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(Convert.ToString(DateTime.Now.Year)));

                ddlCtg.DataSource = LibDB.getCtgList(userID);
                ddlCtg.DataTextField = "CtgName";
                ddlCtg.DataValueField = "CtgId";
                ddlCtg.DataBind();

                ListItem item1 = new ListItem();
                item1.Text = "[All Categories]";
                item1.Value = "0";
                ddlCtg.Items.Add(item1);

                ddlCtg.SelectedIndex = ddlCtg.Items.IndexOf(ddlCtg.Items.FindByValue("0"));

                ListItem item2 = new ListItem();
                item2.Text = "[All Sub-Categories]";
                item2.Value = "0";
                ddlSubCtg.Items.Add(item2);

                ddlSubCtg.SelectedIndex = ddlSubCtg.Items.IndexOf(ddlSubCtg.Items.FindByValue("0"));

                if (formType == LibEnum.eFormTypes.CalendarForm)
                {
                    LibCode.setExpensesMonthlyData(HtmlTableControl, Convert.ToInt16(ddlMonth.SelectedValue), 
                        Convert.ToInt16(ddlYear.SelectedValue), Convert.ToInt16(ddlCtg.SelectedValue),
                        Convert.ToInt16(ddlSubCtg.SelectedValue),
                        userID, lblSumMonth, lblSumYTD, lblSumYear);
                }
            }
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (formType == LibEnum.eFormTypes.CalendarForm)
            {
                LibCode.setExpensesMonthlyData(HtmlTableControl, Convert.ToInt16(ddlMonth.SelectedValue),
                        Convert.ToInt16(ddlYear.SelectedValue), Convert.ToInt16(ddlCtg.SelectedValue),
                        Convert.ToInt16(ddlSubCtg.SelectedValue),
                        userID, lblSumMonth, lblSumYTD, lblSumYear);
            }
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (formType == LibEnum.eFormTypes.CalendarForm)
            {
                LibCode.setExpensesMonthlyData(HtmlTableControl, Convert.ToInt16(ddlMonth.SelectedValue),
                       Convert.ToInt16(ddlYear.SelectedValue), Convert.ToInt16(ddlCtg.SelectedValue),
                       Convert.ToInt16(ddlSubCtg.SelectedValue),
                       userID, lblSumMonth, lblSumYTD, lblSumYear);
            }
          }

        protected void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            string sMonth = ddlMonth.SelectedValue;
            string sYear = ddlYear.SelectedValue;

            Int16 iMonth;
            Int16 iYear;

            if (sMonth == "12")
            {
                iMonth = 1;
                iYear = Convert.ToInt16(Convert.ToInt16(sYear) + 1);
            }
            else
            {
                iMonth = Convert.ToInt16(Convert.ToInt16(sMonth) + 1);
                iYear = Convert.ToInt16(sYear);
            }

            ListItem itemMonth = ddlMonth.Items.FindByValue(Convert.ToString(iMonth));
            if (itemMonth != null)
            {
                itemMonth.Selected = true;
                ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(Convert.ToString(iMonth)));
            }

            ListItem itemYear = ddlYear.Items.FindByValue(Convert.ToString(iYear));
            if (itemYear != null)
            {
                itemYear.Selected = true;
                ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(Convert.ToString(iYear)));
            };

            {
                if (formType == LibEnum.eFormTypes.CalendarForm)
                {
                    LibCode.setExpensesMonthlyData(HtmlTableControl,iMonth, iYear, Convert.ToInt16(ddlCtg.SelectedValue),
                                                   Convert.ToInt16(ddlSubCtg.SelectedValue),
                                                    userID, lblSumMonth, lblSumYTD, lblSumYear);
                }
             }
        }

        protected void btnPrevious_Click(object sender, ImageClickEventArgs e)
        {
            string sMonth = ddlMonth.SelectedValue;
            string sYear = ddlYear.SelectedValue;

            Int16 iMonth;
             Int16 iYear;

            if (sMonth == "1")
            {
                iMonth = 12;
                iYear = Convert.ToInt16(Convert.ToInt16(sYear) - 1);
            }
            else
            {
                iMonth = Convert.ToInt16(Convert.ToInt16(sMonth) - 1);
                iYear = Convert.ToInt16(sYear);
            }

            ListItem itemMonth = ddlMonth.Items.FindByValue(Convert.ToString(iMonth));
            if (itemMonth != null)
            {
                itemMonth.Selected = true;
                ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(Convert.ToString(iMonth)));
            }

            ListItem itemYear = ddlYear.Items.FindByValue(Convert.ToString(iYear));
            if (itemYear != null)
            {
                itemYear.Selected = true;
                ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(Convert.ToString(iYear)));
            };

            {
                if (formType == LibEnum.eFormTypes.CalendarForm)
                {
                    LibCode.setExpensesMonthlyData(HtmlTableControl, iMonth, iYear, Convert.ToInt16(ddlCtg.SelectedValue),
                                                    Convert.ToInt16(ddlSubCtg.SelectedValue),
                                                     userID, lblSumMonth, lblSumYTD, lblSumYear);
                }
            }

        }

        protected void ddlCtg_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSubCtg.Items.Clear();
            ddlSubCtg.DataSource = LibDB.getSubCtgList(Convert.ToInt32(ddlCtg.SelectedValue));
            ddlSubCtg.DataTextField = "SubCtgName";
            ddlSubCtg.DataValueField = "SubCtgId";
            ddlSubCtg.DataBind();

            ListItem item2 = new ListItem();
            item2.Text = "[All Sub-Categories]";
            item2.Value = "0";
            ddlSubCtg.Items.Add(item2);

            ddlSubCtg.SelectedIndex = ddlSubCtg.Items.IndexOf(ddlSubCtg.Items.FindByValue("0"));

            LibCode.setExpensesMonthlyData(HtmlTableControl, Convert.ToInt16(ddlMonth.SelectedValue),
                       Convert.ToInt16(ddlYear.SelectedValue), Convert.ToInt16(ddlCtg.SelectedValue),
                       Convert.ToInt16(ddlSubCtg.SelectedValue),
                       userID, lblSumMonth, lblSumYTD, lblSumYear);
        }

        protected void ddlSubCtg_SelectedIndexChanged(object sender, EventArgs e)
        {
            LibCode.setExpensesMonthlyData(HtmlTableControl, Convert.ToInt16(ddlMonth.SelectedValue),
                      Convert.ToInt16(ddlYear.SelectedValue), Convert.ToInt16(ddlCtg.SelectedValue),
                      Convert.ToInt16(ddlSubCtg.SelectedValue),
                      userID, lblSumMonth, lblSumYTD, lblSumYear);
        }

        protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
        {
            LibCode.setExpensesMonthlyData(HtmlTableControl, Convert.ToInt16(ddlMonth.SelectedValue),
          Convert.ToInt16(ddlYear.SelectedValue), Convert.ToInt16(ddlCtg.SelectedValue),
          Convert.ToInt16(ddlSubCtg.SelectedValue),
          userID, lblSumMonth, lblSumYTD, lblSumYear);
        }
    }
}