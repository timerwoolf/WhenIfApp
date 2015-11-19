using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Faculty : Page
    {
   
        SqlConnection MyConnection = new SqlConnection("Data Source=cpeake.asuscomm.com;Integrated Security=False;User ID=matthew;Password=matthew;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; Initial Catalog=WhenIf_Data;");

        protected void Page_Load(object sender, EventArgs e)
        {

            String userName = User.Identity.Name;

            if (!User.Identity.IsAuthenticated) // if the user is already logged in
            {
                Response.Redirect("~/Account/Login", false);
            }

            if (!isFaculty())
            {
                Response.Redirect("~/", false);   
            }
        }
        public void StudentSearch(object sender, EventArgs e)
        {
            //clean up the previous search
            //test
            savedBox.Items.Clear();

            SqlDataReader rdr;

            string query = "SELECT UserName, DEPAULID " +
                           "FROM dbo.AspNetUsers " +
                           "WHERE UserName like @SEARCH_STRING+'%'";

            SqlCommand cmd = new SqlCommand(query, MyConnection);
            cmd.Parameters.Add("@SEARCH_STRING", System.Data.SqlDbType.VarChar);
            cmd.Parameters["@SEARCH_STRING"].Value = searchString.Text;
            MyConnection.Open();

            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                ListItem Item = new ListItem(rdr.GetString(0), rdr.GetString(1));
                if (!savedBox.Items.Contains(Item))
                {
                    savedBox.Items.Add(Item);
                }
            }

            MyConnection.Close();

        }
        
        public void StudentSelect(object sender, EventArgs e)
        {
            studentDegreeBox.Items.Clear();
            SqlDataReader studentRdr;
            string queryGetSelectecStudent = "SELECT * " +
                   "FROM dbo.UserDegree " +
                   "WHERE DEPAULID = @DEPAULID";

            var selectedStudent = savedBox.SelectedItem;

            string selection = String.Join(",", selectedStudent.Value).TrimEnd();

            SqlCommand readClassCmd = new SqlCommand(queryGetSelectecStudent, MyConnection);
            readClassCmd.Parameters.Add("@DEPAULID", System.Data.SqlDbType.VarChar);
            readClassCmd.Parameters["@DEPAULID"].Value = selection;
            MyConnection.Open();
            studentRdr = readClassCmd.ExecuteReader();

            while (studentRdr.Read())
            {
                studentDegreeBox.Items.Add(new ListItem(studentRdr.GetString(2), studentRdr.GetString(2)));
            }
            MyConnection.Close();
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
            MyConnection.Close();
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