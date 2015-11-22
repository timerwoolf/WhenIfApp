using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class Calculator : Page
    {
        static Boolean reload = true;
        SqlConnection MyConnection = new SqlConnection("Data Source=cpeake.asuscomm.com;Integrated Security=False;User ID=matthew;Password=matthew;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; Initial Catalog=WhenIf_Data;");
        DataTable degreeTable = new DataTable();
        DataTable optionTable = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                reload = false;
            }
            else
            {
                reload = true;
            }
            if (!User.Identity.IsAuthenticated) // if the user is already logged in
            {
                Response.Redirect("~/Account/Login", false);
            }

            if (reload)
            {
                using (MyConnection)
                {
                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter("SELECT DegreeID, DegreeDescription FROM wi.tbl_degree", MyConnection);
                        adapter.Fill(degreeTable);

                        ddlDegree.DataSource = degreeTable;
                        ddlDegree.DataTextField = "DegreeDescription";
                        ddlDegree.DataValueField = "DegreeID";
                        ddlDegree.DataBind();
                        adapter.Dispose();
                    }
                    catch (Exception ex)
                    {
                        // Handle the error
                    }
                }
            }

        }

        //loads ddlOption items once ddlDegree is chosen
        protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
        {
            //A postback occurred - use the selection to bind the values to your other drop-down

            ddlOption.ClearSelection();
            ddlOption.Items.Clear();
            ddlOption.Items.Add("-");
            using (MyConnection)
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT D1.OptionID, OptionDescription " +
                                                                    "FROM wi.tbl_deg_opt_relationship D1 INNER JOIN wi.tbl_option D2 " +
                                                                        "ON D1.OptionID = D2.OptionID " +
                                                                    "WHERE DegreeID = " + ddlDegree.SelectedValue, MyConnection);
                    optionTable.Clear();
                    adapter.Fill(optionTable);
                    ddlOption.DataSource = optionTable;
                    ddlOption.DataTextField = "OptionDescription";
                    ddlOption.DataValueField = "OptionID";
                    ddlOption.DataBind();

                    if (ddlOption.Items.Count > 1)
                    {
                        ddlOption.Enabled = true;

                    }
                    else
                    {
                        ddlOption.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    // Handle the error
                }

            }
        }

        protected void SearchTab_Click(object sender, EventArgs e)
        {
            SearchTab.CssClass = "Clicked";
            ResultTab.CssClass = "Initial";
            SavedTab.CssClass = "Initial";
            MainView.ActiveViewIndex = 0;
        }

        protected void ResultTab_Click(object sender, EventArgs e)
        {
            SearchTab.CssClass = "Initial";
            ResultTab.CssClass = "Clicked";
            SavedTab.CssClass = "Initial";
            MainView.ActiveViewIndex = 1;
        }

        protected void SavedTab_Click(object sender, EventArgs e)
        {
            SearchTab.CssClass = "Initial";
            ResultTab.CssClass = "Initial";
            SavedTab.CssClass = "Clicked";
            MainView.ActiveViewIndex = 2;
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/Register", false);
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {

        }

        protected void RunSearch(object sender, EventArgs e)
        {
        }
    }
}