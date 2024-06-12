using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Foodie.Admin;

namespace Foodie.Users
{
    public partial class Menu : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GetCategories();
                GetProducts();
            }
        }
        void GetProducts()
        {
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_Product", con);
            cmd.Parameters.AddWithValue("@Action", "ActiveProd");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            rProduct.DataSource = dt;
            rProduct.DataBind();
        }
        void GetCategories()
        {
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_Category", con);
            cmd.Parameters.AddWithValue("@Action", "ActiveCat");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            rCategory.DataSource = dt;
            rCategory.DataBind();
        }
        //public string LowerCase(object obj)
        //{
        //    return obj.ToString().ToLower();
        //}
        protected void rProduct_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            
            if (Session["UserID"] != null)
            {
                lblMsg.Text = "Hello";
                bool isCartItemUpdated = false;
                int i = isItemExistInCart(Convert.ToInt32(e.CommandArgument));
                if (i == 0)
                {
                    con = new SqlConnection(clsUtils.GetConnection());
                    cmd = new SqlCommand("sp_Cart", con);
                    cmd.Parameters.AddWithValue("@Action", "Insert");
                    cmd.Parameters.AddWithValue("@ProductID", e.CommandArgument);
                    cmd.Parameters.AddWithValue("@Quantity", 1);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error -" + ex.Message + "');</script>");
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else
                {
                    clsUtils utils = new clsUtils();
                    isCartItemUpdated = utils.UpdateCartQuantity(i + 1, Convert.ToInt32(e.CommandArgument),
                        Convert.ToInt32(Session["UserID"]));
                    
                }
                lblMsg.Visible = true;
                lblMsg.Text = "Item added successfully in your cart";
                lblMsg.CssClass = "alert alert-success";
                Response.AddHeader("REFRESH", "1;URL=Cart.aspx");


            }
            else
            {
                Response.Redirect("Login.aspx");
            }

        }
        int isItemExistInCart(int ProductID)
        {
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_Cart", con);
            cmd.Parameters.AddWithValue("@Action", "GetByID");
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            int Quantity = 0;
            if(dt.Rows.Count > 0)
            {
                Quantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);
            }
            return Quantity;
        }
    }

}