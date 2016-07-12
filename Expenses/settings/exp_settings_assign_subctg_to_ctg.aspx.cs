using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Expenses.settings
{
    public partial class exp_settings_assign_subctg_to_ctg : System.Web.UI.Page
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

                if (!Page.IsPostBack)
                {
                    ddlCtg.DataSource = LibDB.getCtgList(Convert.ToInt32(Session["UserID"]));
                    ddlCtg.DataTextField = "CtgName";
                    ddlCtg.DataValueField = "CtgId";
                    ddlCtg.DataBind();

                    lstSubCtg1.Items.Clear();
                    lstSubCtg1.DataSource = LibDB.getSubCtgList(Convert.ToInt32(ddlCtg.SelectedValue), true);
                    lstSubCtg1.DataTextField = "SubCtgName";
                    lstSubCtg1.DataValueField = "SubCtgId";
                    lstSubCtg1.DataBind();

                    lstSubCtg2.Items.Clear();
                    lstSubCtg2.DataSource = LibDB.getSubCtgList(Convert.ToInt32(ddlCtg.SelectedValue));
                    lstSubCtg2.DataTextField = "SubCtgName";
                    lstSubCtg2.DataValueField = "SubCtgId";
                    lstSubCtg2.DataBind();
                }

            }
        }

        protected void ddlCtg_SelectedIndexChanged(object sender, EventArgs e)
        {

            lstSubCtg1.Items.Clear();
            lstSubCtg1.DataSource = LibDB.getSubCtgList(Convert.ToInt32(ddlCtg.SelectedValue), true);
            lstSubCtg1.DataTextField = "SubCtgName";
            lstSubCtg1.DataValueField = "SubCtgId";
            lstSubCtg1.DataBind();

            lstSubCtg2.Items.Clear();
            lstSubCtg2.DataSource = LibDB.getSubCtgList(Convert.ToInt32(ddlCtg.SelectedValue));
            lstSubCtg2.DataTextField = "SubCtgName";
            lstSubCtg2.DataValueField = "SubCtgId";
            lstSubCtg2.DataBind();
        }

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            var values = lstSubCtg1.Items.Cast<ListItem>().Where(n => n.Selected).Select(n => n.Value).ToList();

            if (values != null && values.Count > 0)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    LibDB.addnewExpensesCtgLink(Convert.ToInt32(ddlCtg.SelectedValue),
                                       Convert.ToInt32(values[i]), Convert.ToInt32(Session["UserID"]));
                }

                lstSubCtg1.Items.Clear();
                lstSubCtg1.DataSource = LibDB.getSubCtgList(Convert.ToInt32(ddlCtg.SelectedValue), true);
                lstSubCtg1.DataTextField = "SubCtgName";
                lstSubCtg1.DataValueField = "SubCtgId";
                lstSubCtg1.DataBind();

                lstSubCtg2.Items.Clear();
                lstSubCtg2.DataSource = LibDB.getSubCtgList(Convert.ToInt32(ddlCtg.SelectedValue));
                lstSubCtg2.DataTextField = "SubCtgName";
                lstSubCtg2.DataValueField = "SubCtgId";
                lstSubCtg2.DataBind();

            }
        }

        protected void btnRemove_Click(object sender, ImageClickEventArgs e)
        {
            var values = lstSubCtg2.Items.Cast<ListItem>().Where(n => n.Selected).Select(n => n.Value).ToList();

            if (values != null && values.Count > 0)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    LibDB.deleteExpensesCtgLink(Convert.ToInt32(ddlCtg.SelectedValue),
                                       Convert.ToInt32(values[i]), Convert.ToInt32(Session["UserID"]));
                }

                lstSubCtg1.Items.Clear();
                lstSubCtg1.DataSource = LibDB.getSubCtgList(Convert.ToInt32(ddlCtg.SelectedValue), true);
                lstSubCtg1.DataTextField = "SubCtgName";
                lstSubCtg1.DataValueField = "SubCtgId";
                lstSubCtg1.DataBind();

                lstSubCtg2.Items.Clear();
                lstSubCtg2.DataSource = LibDB.getSubCtgList(Convert.ToInt32(ddlCtg.SelectedValue));
                lstSubCtg2.DataTextField = "SubCtgName";
                lstSubCtg2.DataValueField = "SubCtgId";
                lstSubCtg2.DataBind();

            }
        }
    }
}