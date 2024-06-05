using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Foodie.Admin
{
    public partial class Users : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Session["breadCrum"] = "User";
                if (Session["Admin"] == null)
                {
                    Response.Redirect("../Users/Login.aspx");
                }
                else
                {
                    GetUsers(); 
                }
            }
            GetUsers();
            lblMsg.Visible = false;
        }
        void GetUsers()
        {
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_User", con);
            cmd.Parameters.AddWithValue("@Action", "Select4Admin");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            rUser.DataSource = dt;
            rUser.DataBind();
        }
        protected void rUser_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
           // lblMsg.Visible = false;
            
             if (e.CommandName == "delete")
            {
                con = new SqlConnection(clsUtils.GetConnection());
                cmd = new SqlCommand("sp_User", con);
                cmd.Parameters.AddWithValue("@Action", "Delete");
                cmd.Parameters.AddWithValue("@User5ID", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;


                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    lblMsg.Visible = true;
                    lblMsg.Text = "User Deleted Successfully";
                    lblMsg.CssClass = "alert alert-success";
                    GetUsers();
                    //Clear();
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Error: " + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally { con.Close(); }
            }

        }
    }
}