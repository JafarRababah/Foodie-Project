using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Foodie.Users;

namespace Foodie.Admin
{
    public partial class Report : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "Selling Report";
                if (Session["Admin"] == null)
                {
                    Response.Redirect("../Users/Login.aspx");
                }
                else
                {
                    GetSellingReports();
                }
            }
        }
        private void GetSellingReports()
        {
            double grandTotal = 0;
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_SellingReport", con);
            cmd.Parameters.AddWithValue("@FromDate", txtFromDate.Text.Trim());
            cmd.Parameters.AddWithValue("@ToDate", txtToDate.Text.Trim());
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            if(dt.Rows.Count> 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    grandTotal += Convert.ToDouble(dr["TotalPrice"]);
                }
                lblTotal.Text ="Sold Cost: $"+ grandTotal.ToString();
                lblTotal.CssClass = "badge badge-primary";
            }
            rReport.DataSource = dt;
            rReport.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime fromDate=Convert.ToDateTime(txtFromDate.Text);
            DateTime toDate =Convert.ToDateTime(txtToDate.Text);
            if (toDate > DateTime.Now)
            {
                Response.Write("<script>alert('ToDate cannot be greater than current date!);</script>");
            }
            else if(fromDate>toDate)
            {
                Response.Write("<script>alert('FromDate cannot be greater than current Todate!);</script>");
            }
            else
            {
                GetSellingReports();
            }
        }
    }
}