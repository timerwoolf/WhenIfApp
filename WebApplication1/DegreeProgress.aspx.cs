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
    public partial class DegreeProgress : Page
    {
        static Boolean reload = true;
        SqlConnection MyConnection = new SqlConnection("Data Source=cpeake.asuscomm.com;Integrated Security=False;User ID=matthew;Password=matthew;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; Initial Catalog=WhenIf_Data;");

        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                reload = false;
            } else
            {
                reload = true;
            }
            SqlDataReader rdr;
            SqlDataReader classRdr;
            String userName = User.Identity.Name;
            String depaulID = "";

            if (!User.Identity.IsAuthenticated) // if the user is already logged in
            {
                Response.Redirect("~/Account/Login", false);
            }

            if (savedBox.Items.Count == 0 && reload)
            {
                savedBox.Items.Clear();
                string queryGetDepaulId = "SELECT DEPAULID " +
                                             "FROM dbo.AspNetUsers " +
                                             "WHERE UserName = @USER_NAME";


                SqlCommand readCmd = new SqlCommand(queryGetDepaulId, MyConnection);
                readCmd.Parameters.Add("@USER_NAME", System.Data.SqlDbType.VarChar);
                readCmd.Parameters["@USER_NAME"].Value = userName;
                MyConnection.Open();


                rdr = readCmd.ExecuteReader();

                while (rdr.Read())
                {
                    depaulID = rdr.GetString(0);
                }

                rdr.Close();


                //populate Saved degree section on page load

                string queryGetSavedDegree = "SELECT * " +
                    "FROM dbo.UserDegree " +
                    "WHERE DEPAULID = @DEPAULID";


                SqlCommand readClassCmd = new SqlCommand(queryGetSavedDegree, MyConnection);
                readClassCmd.Parameters.Add("@DEPAULID", System.Data.SqlDbType.VarChar);
                readClassCmd.Parameters["@DEPAULID"].Value = depaulID;
                classRdr = readClassCmd.ExecuteReader();

                while (classRdr.Read())
                {
                    savedBox.Items.Add(new ListItem(classRdr.GetString(2), classRdr.GetString(2)));
                }
                MyConnection.Close();
            }
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/Register", false);
        }


        public void addClass(object sender, EventArgs e)
        {
            reload = false;
            var selectedClass = resultsBox.SelectedItem;

            string selectionName = String.Join(",", selectedClass.Text).TrimEnd();
            string selectionValue = String.Join(",", selectedClass.Value).TrimEnd();
            if (selectedClass != null)
            {
                savedBox.Items.Add(new ListItem(selectionName, selectionValue));
                resultsBox.Items.Remove(resultsBox.SelectedItem);
            }
            
        }

        public void deleteClass(object sender, EventArgs e)
        {
            reload = false;
            var selectedClass = savedBox.SelectedItem;


            string selectionName = String.Join(",", selectedClass.Text).TrimEnd();
            string selectionValue = String.Join(",", selectedClass.Value).TrimEnd();
            if (selectedClass != null)
            {
                resultsBox.Items.Add(new ListItem(selectionName, selectionValue));
                savedBox.Items.Remove(savedBox.SelectedItem);
            }

        }


        public void saveClasses(object sender, EventArgs e)
        {
            reload = true;
            String userName = User.Identity.Name;
            String depaulID = "";
            SqlDataReader rdr;

            string queryGetDepaulId = "SELECT DEPAULID " +
                "FROM dbo.AspNetUsers " +
                "WHERE UserName = @USER_NAME";


            SqlCommand readCmd = new SqlCommand(queryGetDepaulId, MyConnection);
            readCmd.Parameters.Add("@USER_NAME", System.Data.SqlDbType.VarChar);
            readCmd.Parameters["@USER_NAME"].Value = userName;
            MyConnection.Open();


            rdr = readCmd.ExecuteReader();

            while (rdr.Read())
            {
                depaulID = rdr.GetString(0);
            }

            rdr.Close();


            string deleteSql = "DELETE FROM dbo.UserDegree WHERE DEPAULID = @DEPAULID AND DEGREEID = @DEGREEID";
            SqlCommand deleteCmd = new SqlCommand(deleteSql, MyConnection);
            deleteCmd.Parameters.Add("@DEGREEID", System.Data.SqlDbType.VarChar);
            deleteCmd.Parameters.Add("@DEPAULID", System.Data.SqlDbType.VarChar);

            deleteCmd.Parameters["@DEGREEID"].Value = userName + depaulID;
            deleteCmd.Parameters["@DEPAULID"].Value = depaulID;

            deleteCmd.ExecuteNonQuery();

            for (int i = savedBox.Items.Count - 1; i >= 0; i--)
            {

                string sql = "INSERT INTO dbo.UserDegree VALUES (@DEGREEID, @DEPAULID, @COURSEID)";
                SqlCommand insertCmd = new SqlCommand(sql, MyConnection);
                insertCmd.Parameters.Add("@DEGREEID", System.Data.SqlDbType.VarChar);
                insertCmd.Parameters.Add("@DEPAULID", System.Data.SqlDbType.VarChar);
                insertCmd.Parameters.Add("@COURSEID", System.Data.SqlDbType.VarChar);

                insertCmd.Parameters["@DEGREEID"].Value = userName + depaulID;
                insertCmd.Parameters["@DEPAULID"].Value = depaulID;
                insertCmd.Parameters["@COURSEID"].Value = savedBox.Items[i].Value;

                insertCmd.ExecuteNonQuery();


            }
            MyConnection.Close();
        }

        public void ClassSearch(object sender, EventArgs e)
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

                ListItem Item = new ListItem(courseNumber + " - " + courseDesc, courseNumber);
                if (!savedBox.Items.Contains(Item))
                {
                    resultsBox.Items.Add(Item);
                }
            }

            MyConnection.Close();

        }
    }
}