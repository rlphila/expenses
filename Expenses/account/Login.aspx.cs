using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.Objects.SqlClient;
using System.Net.Mail;

namespace Expenses.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            //System.Web.HttpContext.Current.User.Identity.IsAuthenticated = true;
            if (Membership.ValidateUser(LoginUser.UserName, LoginUser.Password))
            {
                Session["UserID"] = Expenses.LibCode.getExpensesUserID();
                CheckBox chkRememberMe = LoginUser.FindControl("RememberMe") as CheckBox;
                FormsAuthentication.RedirectFromLoginPage(LoginUser.UserName, chkRememberMe.Checked);
            }
        }

        void PasswordRecovery1_UserLookupError(object sender, EventArgs e)
        {
            PasswordRecovery1.UserNameTitleText = "Try again";
            PasswordRecovery1.TitleTextStyle.ForeColor = System.Drawing.Color.Red;
        }

        protected void btnForgotPsw_Click(object sender, EventArgs e)
        {
            using (Expenses.data.expensesEntities db = new Expenses.data.expensesEntities())
            {
                try
                {
                    db.Connection.Open();

                    string mypsw = string.Empty;
                    string myemail = string.Empty;

                    var myinput = PasswordRecovery1.UserName;
                    //check user by user name
                    var accounts = (from x in db.aspnet_Users where x.LoweredUserName == myinput.ToLower() select x).ToList();
                    if (accounts != null && accounts.Count > 0)
                    {
                        var myuserid = accounts[0].UserId;

                        var myuser = (from x in db.aspnet_Membership where x.UserId == myuserid select x).ToList();

                        if (myuser != null && myuser.Count > 0)
                        {
                            mypsw = myuser[0].Password.ToString();
                            myemail = myuser[0].Email.ToString();
                        }
                    }
                    else
                    {
                        var myuser2 = (from x in db.aspnet_Membership where x.LoweredEmail == myinput.ToLower() select x).ToList();
                     
                        if (myuser2 != null)
                        {
                            mypsw = myuser2[0].Password.ToString();
                            myemail = myuser2[0].Email.ToString();
                        }
                    }

                    if (!string.IsNullOrEmpty(mypsw) && !string.IsNullOrEmpty(myemail))
                    {

                        //var passwordManager = new LibDB.NetFourMembershipProvider();
                        //var clearPWd = passwordManager.GetClearTextPassword(mypsw);

                        MailMessage mailObj = new MailMessage("support@totalexpenses.com", myemail,
                                                              "Total Expenses - password recovery",
                                                               mypsw);

                        System.Net.Mail.SmtpClient SMTP = new System.Net.Mail.SmtpClient();

                        //SMTP.Credentials = new System.Net.NetworkCredential("support@totalexpenses.com", "Dynamo85$");
                        //SMTP.DeliveryMethod = SmtpDeliveryMethod.Network;
                        //SMTP.Host = "localhost";
                        SMTP.Send(mailObj);
                    }
                }
                catch (Exception ex)
                {
                    //do nothing

                   // LibDB.saveError(ex, 0);
                }
            }
        }

       
    }
 }
