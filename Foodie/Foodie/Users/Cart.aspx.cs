using Foodie.Admin;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Foodie.Users
{
    public partial class Cart : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        decimal grandTotal = 0;
        clsUtils utils = new clsUtils();
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
                    GetCarts();
                }
            }
        }
        void GetCarts()
        {
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_Cart", con);
            cmd.Parameters.AddWithValue("@Action", "Select");
            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            rCartItem.DataSource= dt;
            if(dt.Rows.Count == 0)
            {
                rCartItem.FooterTemplate = null;
                rCartItem.FooterTemplate = new CustomTemplate(ListItemType.Footer);
            }
            rCartItem.DataBind();
        }
        protected void rCartItem_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            clsUtils utils=new clsUtils();
            if (e.CommandName == "remove")
            {
                con = new SqlConnection(clsUtils.GetConnection());
                cmd = new SqlCommand("sp_Cart", con);
                cmd.Parameters.AddWithValue("@Action", "Delete");
                cmd.Parameters.AddWithValue("@ProductID", e.CommandArgument);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    GetCarts();
                    //cart count
                    Session["cartCount"] = utils.cartCount(Convert.ToInt32(Session["UserID"]));
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
            if (e.CommandName == "updateCart")
            {
                UpdateQty();
            }
            if (e.CommandName == "checkout")
            {
                UpdateQty();
                bool isTrue = false;
                string pName = string.Empty;
                for (int item = 0; item < rCartItem.Items.Count; item++)
                {
                    if (rCartItem.Items[item].ItemType == ListItemType.Item || rCartItem.Items[item].ItemType == ListItemType.AlternatingItem)
                    {
                        HiddenField _productid = rCartItem.Items[item].FindControl("hdnProductID") as HiddenField;
                        HiddenField _cartQuantity = rCartItem.Items[item].FindControl("hdnQuantity") as HiddenField;
                        HiddenField _productQuantity = rCartItem.Items[item].FindControl("hdnPrdQuantity") as HiddenField;
                        Label productName = rCartItem.Items[item].FindControl("lblProductName") as Label;
                        int productID = Convert.ToInt32(_productid.Value);
                        int cartQauantity = Convert.ToInt32(_cartQuantity.Value);
                        int productQuantity = Convert.ToInt32(_productQuantity.Value);

                        if (productQuantity > cartQauantity && productQuantity > 2)
                        {
                            isTrue = true;
                        }
                        else
                        {
                            isTrue = false;
                            pName = productName.Text.ToString(); break;
                        }
                    }
                }
                if (isTrue)
                {
                    Response.Redirect("Payment.aspx");
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Item <b>'" + pName + "' is out of stock:(";
                    lblMsg.CssClass = "alert alert-warning";
                }
            }
                    
        }
        protected void rCartItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label totalPrice = e.Item.FindControl("lblTotalPrice") as Label;
                Label productPrice = e.Item.FindControl("lblPrice") as Label;
                TextBox quantity = e.Item.FindControl("txtQuantity") as TextBox;
                decimal calTotalPrice = Convert.ToDecimal(productPrice.Text) * Convert.ToDecimal(quantity.Text);
                totalPrice.Text=calTotalPrice.ToString();
                grandTotal += calTotalPrice;
            }
            Session["GrandTotalPrice"] = grandTotal;
        }
        private sealed class CustomTemplate : ITemplate
        {
            private ListItemType ListItemType { get; set; }
            public CustomTemplate (ListItemType type)
            {
                ListItemType = type;
            }
            public void InstantiateIn(Control container)
            {
                if (ListItemType == ListItemType.Footer)
                {
                    var footer = new LiteralControl("<tr><td colspan='5'><b>Your Cart is empty.</b><a href='Menu.aspx' class='badge badge-info ml-2'>Continue Shopping</a></td></tr></tbody></table>");
                    container.Controls.Add(footer);
                }
            }
        }
        protected void UpdateQty()
        {
            bool isCartUpdated = false;
            for (int item = 0; item < rCartItem.Items.Count; item++)
            {
                if (rCartItem.Items[item].ItemType == ListItemType.Item || rCartItem.Items[item].ItemType == ListItemType.AlternatingItem)
                {
                    TextBox quantity = rCartItem.Items[item].FindControl("txtQuantity") as TextBox;
                    HiddenField _productid = rCartItem.Items[item].FindControl("hdnProductID") as HiddenField;
                    HiddenField _quantity = rCartItem.Items[item].FindControl("hdnQuantity") as HiddenField;
                    int quantityFromCart = Convert.ToInt32(quantity.Text);
                    int productID = Convert.ToInt32(_productid.Value);
                    int quantityFromDB = Convert.ToInt32(_quantity.Value);
                    bool isTrue = false;
                    int updateedQuantity = 1;
                    if (quantityFromCart > quantityFromDB)
                    {
                        updateedQuantity = quantityFromCart;
                        isTrue = true;
                    }
                    else if (quantityFromCart < quantityFromDB)
                    {
                        updateedQuantity = quantityFromCart;
                        isTrue = true;
                    }
                    if (isTrue)
                    {
                        isCartUpdated = utils.UpdateCartQuantity(updateedQuantity, productID, Convert.ToInt32(Session["UserID"]));
                    }
                }
            }
            GetCarts();
        }

    }
}