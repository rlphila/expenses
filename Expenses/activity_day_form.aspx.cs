using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data.EntityClient;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;
using System.Reflection;
using System.Web.Security;

namespace Expenses
{
    public partial class ActivityDayForm : System.Web.UI.Page
    {

        DateTime selectedDate;

        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    int i = 0;
        //    System.Threading.Thread.Sleep(5000);
        //}

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
      
            var ctg = Expenses.LibDB.getCtgList(Convert.ToInt32(Session["UserID"]));

            ddlCtg.DataSource = ctg;
            ddlCtg.DataTextField = "CtgName";
            ddlCtg.DataValueField = "CtgId";
            ddlCtg.DataBind();

            string selectedCtgID = ddlCtg.Value;
            string selectedSubCtgID = "0";

            if (Request.QueryString["selectedRecordID"] != null)
            {
                btnAdd.Visible = false;
                btnUpdate.Visible = true;
                hdnRecordId.Value = Request.QueryString["selectedRecordID"];

                Int32 iRecordID = Convert.ToInt32(Request.QueryString["selectedRecordID"]);
                var objRecord = LibDB.selectExpensesRecordByID(iRecordID);
                if (objRecord != null)
                {
                    selectedCtgID = Convert.ToString(objRecord.CtgId);
                    dpDate.Value = objRecord.YmdAdd.ToString("MM/dd/yyyy");
                    txtAmount.Text = String.Format("{0:#####0.00;#####0.00;0.00}", objRecord.AmtExp); // Convert.ToString(objRecord.AmtExp);
                   
                    if (objRecord.AmtExp < 0)
                    {
                        rbPlusMinus.SelectedIndex = 1;
                    }
                    else
                    {
                        rbPlusMinus.SelectedIndex = 0;
                    }

                    txtDesc.Text = objRecord.ExpDesc;
                    selectedSubCtgID = Convert.ToString(objRecord.SubCtgId);
                    //select category for update record
                    ListItem item = ddlCtg.Items.FindByValue(selectedCtgID);
                    if (item != null)
                    {
                        item.Selected = true;
                        ddlCtg.SelectedIndex = ddlCtg.Items.IndexOf(ddlCtg.Items.FindByValue(selectedCtgID));
                    };
                }
            }
            else
            {
                selectedCtgID = ddlCtg.Value;
                btnAdd.Visible = true;
                btnUpdate.Visible = false;
                hdnRecordId.Value = "0";
            }

            var subctg = LibDB.getSubCtgList(Convert.ToInt32(selectedCtgID));

            ddlSubCtg.DataSource = subctg;
            ddlSubCtg.DataTextField = "SubCtgName";
            ddlSubCtg.DataValueField = "SubCtgId";
            ddlSubCtg.DataBind();

            //select category for update record
            if (selectedSubCtgID != "0")
            {
                ListItem item2 = ddlSubCtg.Items.FindByValue(selectedSubCtgID);
                if (item2 != null)
                {
                    item2.Selected = true;
                    ddlSubCtg.SelectedIndex = ddlSubCtg.Items.IndexOf(ddlSubCtg.Items.FindByValue(selectedSubCtgID));
                };
            }
           
            selectedDate = new DateTime();
            hdnYmdAdd.Value = DateTime.Today.Date.ToString("yyyyMMdd");

            if (Request.QueryString["selectedDay"] != null)
            {
                hdnYmdAdd.Value = Request.QueryString["selectedDay"];
                selectedDate = DateTime.Parse(hdnYmdAdd.Value.Substring(4,2) +"/"+ hdnYmdAdd.Value.Substring(6,2) +"/" + hdnYmdAdd.Value.Substring(0,4)).Date;
                
            }
            else {
                hdnYmdAdd.Value = Convert.ToString(DateTime.Today);
                selectedDate = DateTime.Today.Date;
            }

            if (hdnRecordId.Value == "0")
            {
                dpDate.Value = selectedDate.ToString("MM/dd/yyyy");
            }

            var lstRec = LibDB.selectExpensesRecordByDate(selectedDate, Convert.ToInt32(Session["UserID"]));

            if (lstRec != null && lstRec.Count > 0)
            {
                grvUpdate.DataSource=lstRec;
                grvUpdate.DataBind();
            }

        }

        [WebMethod()]
        public static string getJsonSubCtgString(string ctgID)  //object sender, EventArgs e
        {
            var lstSubCtg = LibDB.getSubCtgList(Convert.ToInt16(ctgID));

            StringBuilder sb = new StringBuilder();
            Dictionary<string, string> myDict = new Dictionary<string, string>();

            for(int i=0; i<=(lstSubCtg.Count-1);i++)
            {
                myDict.Add(Convert.ToString(lstSubCtg[i].SubCtgId), lstSubCtg[i].SubCtgName);
            }

            string json_str = JsonConvert.SerializeObject(myDict);
            return json_str;
       }

        [WebMethod()]
        public static string recordAction(string recordID, string dtDate,string plusMinus, string amountNbr, string monthNbr, 
                                          string ctgID, string subctgID, string recordDesc)  
        {
            bool result;

            DateTime ymdADD;
            Decimal amtEXP;

            //verify date value
            try  {
                ymdADD = DateTime.Parse(dtDate);
            }
            catch (Exception)  {
                ymdADD = DateTime.Now.Date;
            }

            //verify amount value
            try
            {
                amtEXP = Decimal.Parse(amountNbr);
            }
            catch (FormatException)
            {
                amtEXP = 0;
            }

            int i = 1;

            if (plusMinus == "0")
            {
                i = -1;
            }


            //verify if record new or exist
            if (string.IsNullOrEmpty(recordID) || Convert.ToInt32(recordID) == 0)
            {
                result = LibDB.addnewExpensesRecord(LibCode.getExpensesUserID(), amtEXP*i,
                                                    ymdADD, Convert.ToInt32(ctgID), 
                                                    Convert.ToInt16(subctgID), recordDesc.Trim(), 
                                                    Convert.ToInt16(monthNbr));
            }
            else
            {
               result = LibDB.updateExpensesRecord(Convert.ToInt32(recordID), LibCode.getExpensesUserID(), 
                                                   amtEXP*i,ymdADD, Convert.ToInt32(ctgID), Convert.ToInt16(subctgID), 
                                                   recordDesc.Trim(), Convert.ToInt16(monthNbr));
            }

            if (result)
            {
                if (string.IsNullOrEmpty(recordID) || Convert.ToInt32(recordID) == 0)
                {
                    return "A new record successfully added.";
                }
                else
                {
                    return "An existing record successfully updated.";
                }
            }
            else
            {
                return "Something wrong! Error during transaction!";
            }

        }

        [WebMethod()]
        public static string recordDelete(string recordID)
        {
            int myRecordID = Convert.ToInt32(recordID);

            if (Expenses.LibDB.deleteExpensesRecordByID(myRecordID))
            {
                return "Record #" + recordID + " has been successfully deleted.";
            }
            else 
            { 
              return "Something Wrong! - Record #" + recordID + " was not deleted.";
            }

        }

        protected void grvUpdate_Sorting(object sender, GridViewSortEventArgs e)
        {

            hdnYmdAdd.Value = Request.QueryString["selectedDay"];
            selectedDate = DateTime.Parse(hdnYmdAdd.Value.Substring(4, 2) + "/" + hdnYmdAdd.Value.Substring(6, 2) + "/" + hdnYmdAdd.Value.Substring(0, 4)).Date;

            var lstRec = LibDB.selectExpensesRecordByDate(selectedDate, Convert.ToInt32(Session["UserID"]));

            if (lstRec != null && lstRec.Count > 0)
            {
                grvUpdate.Attributes["CurrentSortField"] = e.SortExpression;
                grvUpdate.Attributes["CurrentSortDir"] = (e.SortDirection == SortDirection.Ascending ? "DESC" : "ASC");
                grvUpdate.DataSource = lstRec;
                grvUpdate.DataBind();

            }
        }
    }
}