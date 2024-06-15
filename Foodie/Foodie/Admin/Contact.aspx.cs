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
    public partial class Contact : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "Contact User";
                if (Session["Admin"] == null)
                {
                    Response.Redirect("../Users/Login.aspx");
                }
                else
                {
                    GetContacts();
                }
            }
        }
        protected void rContacts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            
            
            if (e.CommandName == "delete")
            {
                con = new SqlConnection(clsUtils.GetConnection());
                cmd = new SqlCommand("sp_Contact", con);
                cmd.Parameters.AddWithValue("@Action", "Delete");
                cmd.Parameters.AddWithValue("@ContactID", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;


                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    lblMsg.Visible = true;
                    lblMsg.Text = "Record Deleted Successfully";
                    lblMsg.CssClass = "alert alert-success";
                    GetContacts();
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
        void GetContacts()
        {
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_Contact", con);
            cmd.Parameters.AddWithValue("@Action", "Select");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            rContacts.DataSource = dt;
            rContacts.DataBind();
        }

    }
}