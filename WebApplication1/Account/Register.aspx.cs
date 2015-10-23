using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using WebApplication1.Models;
using System.Data.SqlClient;

namespace WebApplication1.Account
{
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = UserName.Text, Email = Email.Text };
            IdentityResult result = manager.Create(user, Password.Text);


            try
            {
                SqlConnection MyConnection = new SqlConnection("Data Source=cpeake.asuscomm.com;Integrated Security=False;User ID=matthew;Password=matthew;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; Initial Catalog=WhenIf_Data;");

                string sql = "UPDATE [dbo].[AspNetUsers] SET DEPAULID = @DEPAULID WHERE USERNAME = @USERNAME";
                MyConnection.Open();
                SqlCommand cmd = new SqlCommand(sql, MyConnection);
                cmd.Parameters.Add("@DEPAULID", System.Data.SqlDbType.VarChar);
                cmd.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@DEPAULID"].Value = DepaulId.Text;
                cmd.Parameters["@USERNAME"].Value = UserName.Text;
                cmd.ExecuteNonQuery();


            } catch (Exception exception) {
                ErrorMessage.Text = exception.ToString();
            }
            

            if (result.Succeeded)
            {
            
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}