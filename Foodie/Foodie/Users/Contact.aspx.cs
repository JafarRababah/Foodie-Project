using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Foodie.Users
{
    public partial class Contact : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(clsUtils.GetConnection());
                cmd = new SqlCommand("sp_Contact", con);
                cmd.Parameters.AddWithValue("@Action", "Insert");
                cmd.Parameters.AddWithValue("@FullName", txtName.Text.Trim()) ;
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@Subject", txtSubject.Text.Trim());
                cmd.Parameters.AddWithValue("@Message", txtMessage.Text.Trim());
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                lblMsg.Visible = true;
                lblMsg.Text = "Thanks for reaching out will look into your query";
                lblMsg.CssClass = "alert alert-success";
                Clear();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error -" + ex.Message + "');</script>");
            }
            finally
            {
                con.Close();
            }
            void Clear()
            {
                txtEmail.Text = string.Empty;
                txtSubject.Text = string.Empty;
                txtMessage.Text = string.Empty;
                txtName.Text = string.Empty;
            }
        }
    }
}