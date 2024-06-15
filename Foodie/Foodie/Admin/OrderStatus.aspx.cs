using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Foodie.Admin
{
    public partial class OrderStatus : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["breadCumbTitle"] = "Manager Category";
                Session["breadCrum"] = "Order Status";
                if (Session["Admin"] == null)
                {
                    Response.Redirect("../Users/Login.aspx");
                }
                else
                {
                    GetOrderStatus();
                }
            }
            lblMsg.Visible = false;
            pUpdateOrderStatus.Visible = false;
        }
        void GetOrderStatus()
        {
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_Invoice", con);
            cmd.Parameters.AddWithValue("@Action", "GetStatus");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            rOrderStatus.DataSource = dt;
            rOrderStatus.DataBind();
        }
        protected void rOrderStatus_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "edit")
            {
                con = new SqlConnection(clsUtils.GetConnection());
                cmd = new SqlCommand("sp_Invoice", con);
                cmd.Parameters.AddWithValue("@Action", "StatusByID");
                cmd.Parameters.AddWithValue("@OrderDetailsID", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                ddlOrderStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                hdnID.Value = dt.Rows[0]["OrderDetailsID"].ToString();
                pUpdateOrderStatus.Visible = true;
                LinkButton btn = e.Item.FindControl("lbEdit") as LinkButton;
                btn.CssClass = "badge badge-warning";
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int OrderDetailsID = Convert.ToInt32(hdnID.Value);
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_Invoice", con);
            cmd.Parameters.AddWithValue("@Action", "UpdateStatus");
            cmd.Parameters.AddWithValue("@OrderDetailsID", OrderDetailsID);
            cmd.Parameters.AddWithValue("@Status", ddlOrderStatus.SelectedValue);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                lblMsg.Visible = true;
                lblMsg.Text = "Category updated Successfully";
                lblMsg.CssClass = "alert alert-success";
                GetOrderStatus();
                
            }
            catch (Exception ex)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Error: " + ex.Message;
                lblMsg.CssClass = "alert alert-danger";
            }
            finally { con.Close(); }
        }
    

        protected void btnCancel_Click(object sender, EventArgs e)
        {
              pUpdateOrderStatus.Visible= false;
        }
    }
}