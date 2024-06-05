using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Foodie.Users
{
   
    public partial class Profile : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                 if (Session["UserID"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    getUserDetails();
                }
            }
        }
        void getUserDetails()
        {
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_User", con);
            cmd.Parameters.AddWithValue("@Action", "Select4Profile");
            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            rUserProfile.DataSource = dt;
            rUserProfile.DataBind();
            if (dt.Rows.Count == 1)
            {
                Session["Name"] = dt.Rows[0]["FullName"].ToString();
                Session["Email"] = dt.Rows[0]["Email"].ToString();
                //Session["Mobile"] = dt.Rows[0]["Mobile"].ToString();
                Session["CreatedDate"] = dt.Rows[0]["CreatedDate"].ToString();
                //Session["Name"] = dt.Rows[0]["Address"].ToString();
                //Session["Name"] = dt.Rows[0]["PostCode"].ToString();
                Session["UserImage"] = string.IsNullOrEmpty(dt.Rows[0]["UserImage"].ToString())
                    ? "../Images/No_image.png" : "../" + dt.Rows[0]["UserImage"].ToString();
                //imagePreview.Height = 200;
                //imagePreview.Width = 200;
                //txtPassword.TextMode = TextBoxMode.SingleLine;
                //txtPassword.ReadOnly = true;
                //txtPassword.Text = dt.Rows[0]["Password"].ToString();
                //lblHeaderMsg.Text = "<h2>Edit Profile</h2>";
                //btnRegister.Text = "Update";
                //lblAlreadyUser.Text = "";
            }
        }
    }
}