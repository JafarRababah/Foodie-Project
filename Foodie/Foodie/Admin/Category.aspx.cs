using Foodie;
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
                    GetCategories();
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void rCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;
            if (e.CommandName == "edit")
            {
                con = new SqlConnection(clsUtils.GetConnection());
                cmd = new SqlCommand("sp_Category", con);
                cmd.Parameters.AddWithValue("@Action", "GetByID");
                cmd.Parameters.AddWithValue("@CategoryID", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                txtCategoryName.Text = dt.Rows[0]["CategoryName"].ToString();
                cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                imagePreview.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["CategoryImage"].ToString()) ? "../Images/No_image.png" : "../" + dt.Rows[0]["CategoryImage"].ToString();
                imagePreview.Height = 200;
                imagePreview.Width = 200;
                hfCategoryID.Value = dt.Rows[0]["CategoryID"].ToString();
                LinkButton btn =e.Item.FindControl("lbEdit") as LinkButton;
                btn.Enabled = false;
                btnAddOrUpdate.Text = "Update";
                
               // btnAddOrUpdate.CssClass = "badge badge-warning";
            }
            else if (e.CommandName == "delete")
            {
                con = new SqlConnection(clsUtils.GetConnection());
                cmd = new SqlCommand("sp_Category", con);
                cmd.Parameters.AddWithValue("@Action", "Delete");
                cmd.Parameters.AddWithValue("@CategoryID", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;


                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    lblMsg.Visible = true;
                    lblMsg.Text = "Category Deleted Successfully";
                    lblMsg.CssClass = "alert alert-success";
                    GetCategories();
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
        protected void rCategory_ItemDataBount(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl=e.Item.FindControl("lblIsActive") as Label;
                if (lbl.Text == "True")
                {
                    lbl.Text = "Active";
                    lbl.CssClass = "badge badge-success";

                }
                else
                {
                    lbl.Text = "In-Active";
                    lbl.CssClass = "badge badge-danger";
                }
            }
        }
      }
}