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
        // gets a course's prereqs from the server
        // string => list<string>
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
        // list<string>, date => list<string> 
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
                Console.WriteLine("Classes Offered" + course);

                //checks if offered, qtr=1 -> fall, qtr=3 -> winter
                while (rdr.Read())
                    {  if (rdr.GetBoolean(qtr)) ret.Add(course); }

                if (cmd != null) cmd.Dispose();
                if (MyConnection != null) MyConnection.Close();
                if (rdr != null) rdr.Dispose();
            }

            return ret;
        }

        // determines which classes are still needed from a given list of classes, 
        // checks studentHistory for the required prerequisites
        // list<string>, list<string> => list<string>
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

        // increments date by quarter, currently doesn't account for intersessions
        // list<int> => list<int>
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
        
        //global variables that store degree data to minimize queries
        int electiveN = new int();
        int areaN     = new int();
        List<string> studentHistory  = new List<string>();
        List<string> degreePrereqs   = new List<string>();
        List<string> degreeReqs      = new List<string>();
        List<string> degreeArea      = new List<string>();
        List<string> degreeElectives = new List<string>();

        // accesses server to fill global variables with given degree info
        // string => void
        protected void fillDegreeInfo (string DegreeID)
        {
            //clean lists
            degreePrereqs   = new List<string>();
            degreeReqs      = new List<string>();
            degreeArea      = new List<string>();
            degreeElectives = new List<string>();

            SqlConnection MyConnection = new SqlConnection("Data Source=cpeake.asuscomm.com;Integrated Security=False;User ID=matthew;Password=matthew;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; Initial Catalog=WhenIf_Data;");
            SqlDataReader rdr;

            //get areaN & electiveN values from test.Degree
            string sql = "SELECT ElectiveNumber, AreaNumber FROM [test].[Degree] WHERE DegreeID = @DegreeID;";
            MyConnection.Open();
            SqlCommand cmd = new SqlCommand(sql, MyConnection);
            cmd.Parameters.Add("@DegreeID", System.Data.SqlDbType.VarChar);
            cmd.Parameters["@DegreeID"].Value = DegreeID;
            rdr = cmd.ExecuteReader();
            // add numbers to electiveN & areaN
            if (rdr.Read())
            {
                electiveN = Convert.ToInt32(rdr.GetValue(0));
                areaN = Convert.ToInt32(rdr.GetValue(1));
            }
            if (MyConnection != null) MyConnection.Close();

            //get degree prereqs
            sql = "SELECT prereqID FROM [test].[DegreePrereqs] WHERE DegreeID = @DegreeID;";
            MyConnection.Open();
            cmd = new SqlCommand(sql, MyConnection);
            cmd.Parameters.Add("@DegreeID", System.Data.SqlDbType.VarChar);
            cmd.Parameters["@DegreeID"].Value = DegreeID;
            rdr = cmd.ExecuteReader();
            // add prereqs to degreePrereqs
            while (rdr.Read()) { degreePrereqs.Add(rdr.GetString(0)); }
            if (MyConnection != null) MyConnection.Close();

            //get degree requirements
            sql = "SELECT ReqID FROM [test].[DegreeReqs] WHERE DegreeID = @DegreeID;";
            MyConnection.Open();
            cmd = new SqlCommand(sql, MyConnection);
            cmd.Parameters.Add("@DegreeID", System.Data.SqlDbType.VarChar);
            cmd.Parameters["@DegreeID"].Value = DegreeID;
            rdr = cmd.ExecuteReader();
            
            while (rdr.Read()) { degreeReqs.Add(rdr.GetString(0)); }
            if (MyConnection != null) MyConnection.Close();

            //get degree area classes
            sql = "SELECT AreaID FROM [test].[DegreeArea] WHERE DegreeID = @DegreeID;";
            MyConnection.Open();
            cmd = new SqlCommand(sql, MyConnection);
            cmd.Parameters.Add("@DegreeID", System.Data.SqlDbType.VarChar);
            cmd.Parameters["@DegreeID"].Value = DegreeID;
            rdr = cmd.ExecuteReader();

            while (rdr.Read()) { degreeArea.Add(rdr.GetString(0)); }
            if (MyConnection != null) MyConnection.Close();

            //get degree electives
            sql = "SELECT AreaID FROM [test].[DegreeArea] WHERE DegreeID = @DegreeID;";
            MyConnection.Open();
            cmd = new SqlCommand(sql, MyConnection);
            cmd.Parameters.Add("@DegreeID", System.Data.SqlDbType.VarChar);
            cmd.Parameters["@DegreeID"].Value = DegreeID;
            rdr = cmd.ExecuteReader();

            while (rdr.Read()) { degreeElectives.Add(rdr.GetString(0)); }
            if (MyConnection != null) MyConnection.Close();
        }

        // given a date and N number of classes, finds classes to take for a given quarter
        // checks for classes by need and availability, following path:
        //      prereqs --> main requirements --> area requirements --> electives
        // returns a single quarter's worth of classes
        // string, list<int>, int => list<string>
        protected List<string> search(string degree, List<int> date, int N)
        {
            List<string> currentClasses = new List<string>();
            List<string> needed = new List<string>();
            int count = N;

            //prereqs
            needed = ClassesOffered(classesNeeded(studentHistory, degreePrereqs), date);
            while (needed.Count() > 0 && count > 0)
            {
                string course = needed[0];
                needed.Remove(course);
                currentClasses.Add(course);
                count = count - 1;
            }

            //core classes
            needed = ClassesOffered(classesNeeded(studentHistory, degreeReqs), date);
            while (needed.Count() > 0 && count > 0)
            {
                string course = needed[0];
                needed.Remove(course);
                currentClasses.Add(course);
                count = count - 1;
            }

            //area classes
            needed = ClassesOffered(classesNeeded(studentHistory, degreeArea), date);
            while (needed.Count() > 0 && count > 0 && areaN > 0)
            {
                string course = needed[0];
                needed.Remove(course);
                currentClasses.Add(course);
                count = count - 1;
                areaN = areaN - 1;
            }

            //elective classes
            needed = ClassesOffered(classesNeeded(studentHistory, degreeElectives), date);
            while (needed.Count() > 0 && count > 0 && electiveN > 0)
            {
                string course = needed[0];
                needed.Remove(course);
                currentClasses.Add(course);
                count = count - 1;
                electiveN = electiveN - 1;
            }
            //foreach (string cl in currentClasses) { studentHistory.Add(cl); }

            return currentClasses;
        }

        List<int> date = new List<int>() { 2014, 1};

        protected void run_search(object sender, EventArgs e)
        {
            List<string> needed = new List<string>();
            List<string> qtrClasses = new List<string>();
            int i = 0;
            string txt = "";

            // query db for degree requirements
            fillDegreeInfo("CS w/ Data Science");

            // build list of required classes
            foreach (string cl in classesNeeded(studentHistory, degreePrereqs)) { needed.Add(cl); }
            foreach (string cl in classesNeeded(studentHistory, degreeReqs)) { needed.Add(cl); }

            while (needed.Count() > 0 || electiveN > 0 || areaN > 0)
            {
                i = i + 1;
                txt = txt + "\n\n" + "Qtr#" + i + "  ";
                qtrClasses = search("degreeArea", date, 4);

                foreach (string qtr in qtrClasses)
                {
                    txt = txt + qtr + "  ";
                    studentHistory.Add(qtr);
                }

                incrementDate(date);

                // rebuild list of required classes
                needed = new List<string>();
                foreach (string cl in classesNeeded(studentHistory, degreePrereqs)) { needed.Add(cl); }
                foreach (string cl in classesNeeded(studentHistory, degreeReqs)) { needed.Add(cl); }
            }

            ErrorMessage.Text = txt;
        }

        // old shit (single qtr search)
        protected void run_search2(object sender, EventArgs e)
        {
            List<string> qtrClasses = new List<string>();
            int i = 0;
            string txt = "";
            qtrClasses = search("degree1", date, 4);

            foreach (string qtr in qtrClasses)
            {
                txt = txt + "   " + qtr;
            }

            ErrorMessage.Text = txt;
        }

        // older shit
        protected void test_run(object sender, EventArgs e)
        {
            List<string> have = new List<string>()
                                { "csc400", "csc401", "csc402", "csc403" };
            List<string> need = new List<string>()
                                { "csc400", "csc401", "csc402", "csc406", "csc407", "csc421" };
            Console.WriteLine("before needed 1");
            string txt = "";
            foreach (string cl in classesNeeded(have, need))
            {
                txt = txt + "  " + cl;
            }
            ErrorMessage.Text = txt;
        }

        // slightly less older shit
        protected void test_run2(object sender, EventArgs e)
        {
            List<string> have = new List<string>()
                                { "csc400", "csc401", "csc402", "csc403" };
            List<string> need = new List<string>()
                                { "csc400", "csc401", "csc402", "csc406", "csc407", "csc421" };

            Console.WriteLine("before needed 1");
            string txt = "";
            foreach (string cl in ClassesOffered(need, date))
            {
                txt = txt + "  " + cl;
            }
            ErrorMessage.Text = txt;
        }

        // oldest shit
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = "";
            ErrorMessage.Text = "PAge load pre";
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