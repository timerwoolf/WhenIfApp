using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class DegreeProgress : Page
    {

        private DataTable results;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated) // if the user is already logged in
            {
                Response.Redirect("~/Account/Login", false);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/Register", false);
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {

        }

        public void ClassSearch(object sender, EventArgs e)
        {
            //clean up the previous search just for safe measure
            results = new DataTable();

            SqlConnection MyConnection = new SqlConnection("Data Source=cpeake.asuscomm.com;Integrated Security=False;User ID=matthew;Password=matthew;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; Initial Catalog=WhenIf_Data;");
   
            string query = "SELECT * FROM test.course" +
            "WHERE courseID = '@SEARCH_STRING'";

            SqlCommand cmd = new SqlCommand(query, MyConnection);
            cmd.Parameters.Add("@SEARCH_STRING", System.Data.SqlDbType.VarChar);
            cmd.Parameters["@SEARCH_STRING"].Value = searchString.Text;
            MyConnection.Open();

            // create data adapter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // this will query your database and return the result to your datatable
            da.Fill(results);
            MyConnection.Close();
            da.Dispose();

            foreach (DataRow row in results.Rows)
            {
                resultBox.Text += row["CourseID"].ToString();
            }
        }
    }
}