using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Expenses.Account
{
    public partial class Register : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }

        protected void btnRegisterUser_OnClick(object sender, EventArgs e)
        {
           // FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false /* createPersistentCookie */);

           // MembershipCreateStatus result;

             try
             {
                 MembershipUser newAspUser = Membership.CreateUser(
                        txtUserName.Text, 
                        txtUserPsw.Text, 
                        txtUserEmail.Text);

                 Roles.AddUserToRole(newAspUser.UserName, "silver");
                 Membership.UpdateUser(newAspUser);

                 string aspUserId = newAspUser.ProviderUserKey.ToString();

                 Expenses.data.exp_user newUser = new Expenses.data.exp_user();
                 newUser.YmdEff = DateTime.Now.Date;
                 newUser.YmdEnd = DateTime.Now.Date.AddDays(7);
                 newUser.FreeSpace = 500;
                 newUser.ActiveStatus = true;
                 newUser.AspUserId = Guid.Parse(aspUserId);

                 using (Expenses.data.expensesEntities db = new Expenses.data.expensesEntities())
                 {
                     db.Connection.Open();
                     db.exp_user.AddObject(newUser);
                     db.SaveChanges();

                 }
                 Response.Redirect("../activity_month_calendar.aspx");
                  }

             catch (MembershipCreateUserException e1)
             {
                 ErrorMessage.Text = GetErrorMessage(e1.StatusCode);
             }
             catch (HttpException e2)
             {
                 ErrorMessage.Text = e2.Message;
             }
        }


        public string GetErrorMessage(MembershipCreateStatus status)
        {
            switch (status)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

    }

}
