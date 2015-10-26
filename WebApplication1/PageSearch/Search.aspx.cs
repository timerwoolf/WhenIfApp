using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1.PageSearch
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // gets given course's prereqs from server as a list
        protected List<string> getClassPrereqs(string courseID)
        {
            List<string> ret = new List<string>();

            SqlConnection MyConnection = new SqlConnection("Data Source=cpeake.asuscomm.com;Integrated Security=False;User ID=matthew;Password=matthew;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; Initial Catalog=WhenIf_Data;");
            SqlDataReader rdr;

            string sql = "SELECT PrereqID FROM [test].[CoursePrereqs] WHERE CourseID = @courseID;";
            MyConnection.Open();
            SqlCommand cmd = new SqlCommand(sql, MyConnection);
            cmd.Parameters.Add("@courseID", System.Data.SqlDbType.VarChar);
            cmd.Parameters["@courseID"].Value = courseID;
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                ret.Add(rdr.GetString(0));
            }

            if (cmd != null) cmd.Dispose();
            if (MyConnection != null) MyConnection.Close();
            if (rdr != null) rdr.Dispose();

            return ret;
        }

        // determines which classes in the given list are offered for a given term
        protected List<string> ClassesOffered(List<string> courses, List<int> date)
        {
            List<string> ret = new List<string>();
            int qtr = date[1];

            SqlConnection MyConnection = new SqlConnection("Data Source=cpeake.asuscomm.com;Integrated Security=False;User ID=matthew;Password=matthew;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; Initial Catalog=WhenIf_Data;");
            SqlDataReader rdr;

            foreach (string course in courses)
            {
                string sql = "SELECT * FROM [test].[Course] WHERE CourseID = @course;";
                MyConnection.Open();
                SqlCommand cmd = new SqlCommand(sql, MyConnection);
                cmd.Parameters.Add("@course", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@course"].Value = course;
                rdr = cmd.ExecuteReader();
                
                if (rdr.HasRows )
                    { if (rdr.GetBoolean(qtr)) ret.Add(course); }

                if (cmd != null) cmd.Dispose();
                if (MyConnection != null) MyConnection.Close();
                if (rdr != null) rdr.Dispose();
            }

            return ret;
        }
        protected List<string> classesNeeded(List<string> have, List<string> need)
        {
            List<string> ret = new List<string>();
            bool meets = true;

            foreach (string n in need)
            {
                meets = true;
                if (!have.Contains(n))
                {
                    foreach (string prereq in getClassPrereqs(n))
                    {
                        if (!have.Contains(prereq)) meets = false;
                    }
                    if (meets) ret.Add(n);
                }
            }
            return ret;
        }

        protected List<int> incrementDate(List<int> date)
        {
            if (date[1] >= 3)
            {
                date[0] = date[0] + 1;
                date[1] = 1;
            }
            else date[1] = date[1] + 1;
            return date;
        }
        protected int areaN = 4;
        protected int electiveN = 4;
        protected List<string> studentHistory = new List<string>();

        protected List<string> dataScience = new List<string>(){ "CSC423", "CSC424", "CSC425", "CSC433", "CSC465", "CSC478",
               "CSC481", "CSC482", "CSC495", "CSC529", "CSC555", "CSC575", "CSC578"};

        protected List<string> electives = new List<string>(){ "CSC423", "CSC424", "CSC425", "CSC433", "CSC465",
               "CSC481", "CSC482", "CSC495", "CSC529", "CSC555", "CSC575", "CSC578","CSC452","CSC454","CSC478",
             "CSC553","CSC554" };

        protected List<string> introCourses = new List<string>() { "CSC400", "CSC401", "CSC402", "CSC406", "CSC403", "CSC407" };
        protected List<string> mainCourses = new List<string>() { "SE450", "CSC453", "CSC421", "CSC435", "CSC447" };
        protected List<string> dbSystems = new List<string>() {"CSC433","CSC452","CSC454","CSC478","CSC529","CSC549",
             "CSC553","CSC554","CSC555","CSC575" };

        protected List<string> search(string degree, List<int> date, int N)
        {
            int count = new int();
            count = N;
            List<string> currentClasses = new List<string>();

            List<string> needed = new List<string>();





            //prereqs
            needed = ClassesOffered(classesNeeded(studentHistory, introCourses), date);
            while (needed.Count() > 0 && count > 0)
            {
                string course = needed[0];
                needed.Remove(course);
                currentClasses.Add(course);
                count = count - 1;
            }

            //core classes
            needed = ClassesOffered(classesNeeded(studentHistory, mainCourses), date);
            while (needed.Count() > 0 && count > 0)
            {
                string course = needed[0];
                needed.Remove(course);
                currentClasses.Add(course);
                count = count - 1;
            }

            //area classes
            needed = ClassesOffered(classesNeeded(studentHistory, dataScience), date);
            while (needed.Count() > 0 && count > 0 && areaN > 0)
            {
                string course = needed[0];
                needed.Remove(course);
                currentClasses.Add(course);
                count = count - 1;
                areaN = areaN - 1;
            }

            //elective classes
            needed = ClassesOffered(classesNeeded(studentHistory, electives), date);
            while (needed.Count() > 0 && count > 0 && electiveN > 0)
            {
                string course = needed[0];
                needed.Remove(course);
                currentClasses.Add(course);
                count = count - 1;
                electiveN = electiveN - 1;
            }

            foreach (string cl in currentClasses)
            {
                studentHistory.Add(cl);
            }

            return currentClasses;
        }
        List<int> date = new List<int>() { 2014, 1};

        protected void run_search(object sender, EventArgs e)
        {
            List<string> needed = new List<string>();
            List<string> qtrClasses = new List<string>();
            int i = 0;
            string txt = "";
            foreach (string cl in classesNeeded(studentHistory, introCourses))
            {
                needed.Add(cl);
            }
            foreach (string cl in classesNeeded(studentHistory, mainCourses))
            {
                needed.Add(cl);
            }

            while (needed.Count() > 0 || electiveN > 0 || areaN > 0)
            {
                i = i + 1;
                txt = txt + "Qtr#" + i + "  ";
                qtrClasses = search("DataScience", date, 3);

                foreach (string qtr in qtrClasses)
                {
                    txt = txt + qtr + "  ";
                }

                incrementDate(date);
                needed = new List<string>();
                foreach (string cl in classesNeeded(studentHistory, introCourses))
                {
                    needed.Add(cl);
                }
                foreach (string cl in classesNeeded(studentHistory, mainCourses))
                {
                    needed.Add(cl);
                }
            }

            ErrorMessage.Text = txt;
        }

        protected void test_run(object sender, EventArgs e)
        {
            List<string> have = new List<string>()
                                { "csc400", "csc401", "csc402", "csc403" };
            List<string> need = new List<string>()
                                { "csc400", "csc401", "csc402", "csc406", "csc407", "csc421" };
            string txt = "";
            foreach (string cl in classesNeeded(have, need))
            {
                txt = txt + "  " + cl;
            }
            ErrorMessage.Text = txt;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = "";
            try
            {
                SqlConnection MyConnection = new SqlConnection("Data Source=cpeake.asuscomm.com;Integrated Security=False;User ID=matthew;Password=matthew;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; Initial Catalog=WhenIf_Data;");
                SqlDataReader rdr;

                string sql = "SELECT * FROM [test].[Course];";
                MyConnection.Open();
                SqlCommand cmd = new SqlCommand(sql, MyConnection);
                rdr = cmd.ExecuteReader();

                do
                {
                    str = str + "\n" + rdr.GetString(0);
                }
                while (rdr.Read());

                if (cmd != null) cmd.Dispose();
                if (MyConnection != null) MyConnection.Close();
                if (rdr != null) rdr.Dispose();


            }
            catch (Exception exception)
            {
                ErrorMessage.Text = exception.ToString();
            }

            ErrorMessage.Text = str;

        }
    }
}