using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ClassSearch : System.Web.UI.Page
    {
        static Boolean reload = true;
        SqlConnection MyConnection = new SqlConnection("Data Source=cpeake.asuscomm.com;Integrated Security=False;User ID=matthew;Password=matthew;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; Initial Catalog=WhenIf_Data;");

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!User.Identity.IsAuthenticated) // if the user is already logged in
            {
                Response.Redirect("~/Account/Login", false);
            }

        }

        public void ClassSearchUrl(object sender, EventArgs e)
        {
            reload = false;
            //clean up the previous search
            resultsBox.Items.Clear();

            SqlDataReader rdr;

            string query = "SELECT CourseDescription, CourseNumber, url FROM wi.tbl_course " +
            "WHERE CourseNumber like @SEARCH_STRING + '%'";

            SqlCommand cmd = new SqlCommand(query, MyConnection);
            cmd.Parameters.Add("@SEARCH_STRING", System.Data.SqlDbType.VarChar);
            cmd.Parameters["@SEARCH_STRING"].Value = searchString.Text;
            MyConnection.Open();

            rdr = cmd.ExecuteReader();

            

            while (rdr.Read())
            {
                String courseDesc = rdr.GetString(0);
                String courseNumber = rdr.GetString(1);

                ListItem Item = new ListItem(courseNumber + " - " + courseDesc, rdr.GetString(2));
              
                 resultsBox.Items.Add(Item);
                
            }


            MyConnection.Close();

        }

        public void RedirectUrl(object sender, EventArgs e)
        {

            // Response.Redirect(resultsBox.SelectedItem.Value, false);
            Response.Write("<script language='javascript'> window.open('" + resultsBox.SelectedItem.Value + "', 'window');</script>");
        }
    }
}