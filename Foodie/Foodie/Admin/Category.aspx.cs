using EcommerceSite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Foodie.Admin
{
    public partial class Category : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["breakCumbTitle"] = "Manager Category";
                Session["breakCrum"] = "Category";
            }
            lblMsg.Visible = false;
            GetCategories();
        }
        void GetCategories()
        {
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_Category", con);
            cmd.Parameters.AddWithValue("@Action", "Select");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            rCategory.DataSource = dt;
            rCategory.DataBind();
        }
            void Clear()
        {
            txtCategoryName.Text = string.Empty;
            cbIsActive.Checked = false;
            hfCategoryID.Value = "0";
            btnAddOrUpdate.Text = "Add";
            imagePreview.ImageUrl = string.Empty;
            lblMsg.Text = string.Empty;
        }
        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            string ActionName = string.Empty, ImagePath = string.Empty, FileExtention = string.Empty;
            bool IsValidExecute = false;
            int CategoryID = Convert.ToInt32(hfCategoryID.Value);
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_Category", con);
            cmd.Parameters.AddWithValue("@Action", CategoryID == 0 ? "Insert" : "Update");
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            cmd.Parameters.AddWithValue("@CategoryName", txtCategoryName.Text.Trim());
            cmd.Parameters.AddWithValue("@IsActive", cbIsActive.Checked);
            if (fuCategoryImage.HasFile)
            {
                if (clsUtils.IsValidExtention(fuCategoryImage.FileName))
                {
                    string newImageName = clsUtils.GetUniqueID();
                    FileExtention = Path.GetExtension(fuCategoryImage.FileName);
                    ImagePath = "Images/Category/" + newImageName.ToString() + FileExtention;
                    fuCategoryImage.PostedFile.SaveAs(Server.MapPath("~/Images/Category/") + newImageName.ToString() + FileExtention);
                    cmd.Parameters.AddWithValue("@CategoryImageUrl", ImagePath);
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
                    ActionName = CategoryID == 0 ? "Inserted" : "Updated";
                    lblMsg.Visible = true;
                    lblMsg.Text = "Category " + ActionName + " Successfully";
                    lblMsg.CssClass = "alert alert-success";
                    //GetCategories();
                    Clear();
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}