using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Foodie.Admin;
using System.Diagnostics.Eventing.Reader;

namespace Foodie.Users
{
    public partial class Registiration : System.Web.UI.Page
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["UserID"] != null)/*!=null && Session["UserID"]!= null)*/{
                    getUserDetails();
                }
                else if (Session["UserID"] != null)
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
        void Clear()
        {
            txtUsername.Text = string.Empty;
            txtPostCode.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtName.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            

        }
        void getUserDetails()
        {
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_User", con);
            cmd.Parameters.AddWithValue("@Action", "Select4Profile");
            cmd.Parameters.AddWithValue("@UserID",Request.QueryString["UserID"]);
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                txtName.Text = dt.Rows[0]["FullName"].ToString();
                txtUsername.Text = dt.Rows[0]["Username"].ToString();
                txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                txtPostCode.Text = dt.Rows[0]["PostCode"].ToString();
                imagePreview.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["UserImage"].ToString())
                    ? "../Images/No_image.png" : "../" + dt.Rows[0]["UserImage"].ToString();
                imagePreview.Height = 200;
                imagePreview.Width=200;
                txtPassword.TextMode = TextBoxMode.SingleLine;
                txtPassword.ReadOnly= true;
                txtPassword.Text = dt.Rows[0]["Password"].ToString();
                lblHeaderMsg.Text = "<h2>Edit Profile</h2>";
                btnRegister.Text = "Update";
                lblAlreadyUser.Text = "";
            }
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string ActionName = string.Empty, ImagePath = string.Empty, FileExtention = string.Empty;
            bool IsValidExecute = false;
            int UserID = Convert.ToInt32(Request.QueryString["UserID"]);
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_User", con);
            cmd.Parameters.AddWithValue("@Action", UserID == 0 ? "Insert" : "Update");
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@FullName", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
            cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("@PostCode", txtPostCode.Text.Trim());
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
            if (fuUserImage.HasFile)
            {
                if (clsUtils.IsValidExtention(fuUserImage.FileName))
                {
                    string newImageName = clsUtils.GetUniqueID();
                    FileExtention = Path.GetExtension(fuUserImage.FileName);
                    ImagePath = "Images/User/" + newImageName.ToString() + FileExtention;
                    fuUserImage.PostedFile.SaveAs(Server.MapPath("~/Images/User/") + newImageName.ToString() + FileExtention);
                    cmd.Parameters.AddWithValue("@UserImage", ImagePath);
                    IsValidExecute = true;
                }
                else
                {
                    lblMsg.Visible = false;
                    lblMsg.Text = "Please Select .jpg, .png or .jpeg images";
                    lblMsg.CssClass = "alert alert-danger";
                    IsValidExecute = false;
                }

            }
            else
            {
                IsValidExecute = true;
            }
            if (IsValidExecute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ActionName = UserID == 0 ? "  Resgistration is successfully <b><a href='Login.aspx'>Cleck here</a></b> to do Login" :
                        "  Details Updated Successfully <b><a href='Profile.aspx'>Can check here</a></b>";
                    lblMsg.Visible = true;
                    lblMsg.Text = "<b> " + txtUsername.Text.Trim() + "</b>" + ActionName;
                    lblMsg.CssClass = "alert alert-success";
                    if(UserID!=0)
                    {
                        Response.AddHeader("REFRESH", "1,URL=Profile.aspx");
                    }
                    //GetUsers();
                    Clear();
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "<b> " + txtUsername.Text.Trim() + "</b> Username already exist,try other one";
                        lblMsg.CssClass = "alert alert-danger";
                    }
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