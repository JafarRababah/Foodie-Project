using System;
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
                else
                {
                    DashboardCount dashboard = new DashboardCount();
                    Session["Category"] = dashboard.Count("Category");
                    Session["Product"] = dashboard.Count("Product");
                    Session["Order"] = dashboard.Count("Order");
                    Session["Delivered"] = dashboard.Count("Delivered");
                    Session["Pending"] = dashboard.Count("Pending");
                    Session["User"] = dashboard.Count("User");
                    Session["SoldAmount"] = dashboard.Count("SoldAmount");
                    Session["Contact"] = dashboard.Count("Contact");
                }
            }
        }
    }
}