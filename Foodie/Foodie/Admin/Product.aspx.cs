using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Foodie.Admin
{
    public partial class Product : System.Web.UI.Page
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
                Session["breakCrum"] = "Product";
            }
            lblMsg.Visible = false;
            GetProducts();
        }
        void GetProducts()
        {
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_Product", con);
            cmd.Parameters.AddWithValue("@Action", "Select");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            rProduct.DataSource = dt;
            rProduct.DataBind();
        }
        void Clear()
        {
            txtProductName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtProductPrice.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            ddlCategories.ClearSelection();
            cbIsActive.Checked = false;
            hfProductID.Value = "0";
            btnAddOrUpdate.Text = "Add";
            imagePreview.ImageUrl = string.Empty;
            lblMsg.Text = string.Empty;

        }
        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            string ActionName = string.Empty, ImagePath = string.Empty, FileExtention = string.Empty;
            bool IsValidExecute = false;
            int ProductID = Convert.ToInt32(hfProductID.Value);
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_Product", con);
            cmd.Parameters.AddWithValue("@Action", ProductID == 0 ? "Insert" : "Update");
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text.Trim());
            cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
            cmd.Parameters.AddWithValue("@Price", txtProductPrice.Text.Trim());
            cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text.Trim());
            cmd.Parameters.AddWithValue("@CategoryID", ddlCategories.SelectedValue);
            
            cmd.Parameters.AddWithValue("@IsActive", cbIsActive.Checked);
            if (fuProductImage.HasFile)
            {
                if (clsUtils.IsValidExtention(fuProductImage.FileName))
                {
                    string newImageName = clsUtils.GetUniqueID();
                    FileExtention = Path.GetExtension(fuProductImage.FileName);
                    ImagePath = "Images/Product/" + newImageName.ToString() + FileExtention;
                    fuProductImage.PostedFile.SaveAs(Server.MapPath("~/Images/Product/") + newImageName.ToString() + FileExtention);
                    cmd.Parameters.AddWithValue("@ProductImage", ImagePath);
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
                    ActionName = ProductID == 0 ? "Inserted" : "Updated";
                    lblMsg.Visible = true;
                    lblMsg.Text = "Product " + ActionName + " Successfully";
                    lblMsg.CssClass = "alert alert-success";
                    GetProducts();
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
        protected void rProduct_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_Product", con);
            cmd.Parameters.AddWithValue("@Action", "GetByID");
            cmd.Parameters.AddWithValue("@ProductID", e.CommandArgument);
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            txtProductName.Text = dt.Rows[0]["ProductName"].ToString();
            txtDescription.Text = dt.Rows[0]["Description"].ToString();
            txtProductPrice.Text = dt.Rows[0]["Price"].ToString();
            txtQuantity.Text = dt.Rows[0]["Quantity"].ToString();
            ddlCategories.SelectedValue = dt.Rows[0]["CategoryID"].ToString();
            cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
            imagePreview.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["ProductImage"].ToString()) ? "../Images/No_image.png" : "../" + dt.Rows[0]["CategoryImage"].ToString();
            imagePreview.Height = 200;
            imagePreview.Width = 200;
            hfProductID.Value = dt.Rows[0]["ProductID"].ToString();
            LinkButton btn = e.Item.FindControl("lbEdit") as LinkButton;
            btn.Enabled = false;
            btnAddOrUpdate.Text = "Update";
        }
      }
}