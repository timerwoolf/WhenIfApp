﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated) // if the user is already logged in
            {
                Response.Redirect("/Account/Login");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Account/Register");
        }
    }
}