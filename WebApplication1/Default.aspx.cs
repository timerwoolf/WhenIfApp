using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class _Default : Page
    {
        SqlConnection MyConnection = new SqlConnection("Data Source=cpeake.asuscomm.com;Integrated Security=False;User ID=matthew;Password=matthew;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; Initial Catalog=WhenIf_Data;");

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated) // if the user is already logged in
            {
                Response.Redirect("~/Account/Login", false);
            }
            if (isFaculty()){
                faculty.Visible = true;
            } else{
                faculty.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/Register", false);
        }

        protected Boolean isFaculty()
        {
            String userName = User.Identity.Name;
            int facultyState = 0;
            SqlDataReader rdr;

            string queryGetIsFaculty = "SELECT isFaculty " +
              "FROM dbo.AspNetUsers " +
              "WHERE UserName = @USER_NAME";


            SqlCommand readCmd = new SqlCommand(queryGetIsFaculty, MyConnection);
            readCmd.Parameters.Add("@USER_NAME", System.Data.SqlDbType.VarChar);
            readCmd.Parameters["@USER_NAME"].Value = userName;
            MyConnection.Open();


            rdr = readCmd.ExecuteReader();

            while (rdr.Read())
            {
                facultyState = rdr.GetInt32(0);
            }

            rdr.Close();
            if (facultyState == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}