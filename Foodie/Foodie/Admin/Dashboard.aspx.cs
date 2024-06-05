﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Foodie.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Session["breadCrum"] = " ";
                if (Session["Admin"] == null)
                {
                    Response.Redirect("../Users/Login.aspx");
                }
            }
        }
    }
}