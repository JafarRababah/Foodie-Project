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
                    GetPurchaseHistory();

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
        void GetPurchaseHistory()
        {
            int sr = 1;
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_Invoice", con);
            cmd.Parameters.AddWithValue("@Action", "History");
            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            dt.Columns.Add("SrNo",typeof(string));
            if(dt.Rows.Count > 1)
            {
                foreach(DataRow row in dt.Rows)
                {
                    row["SrNo"] = sr;
                    sr++;
                }
            }
            if (dt.Rows.Count == 0)
            {
                rPurchaseHistory.FooterTemplate = null;
                rPurchaseHistory.FooterTemplate = new CustomTemplate(ListItemType.Footer);
            }
            rPurchaseHistory.DataSource = dt;
            rPurchaseHistory.DataBind();
        }
        protected void rPurchaseHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                double grandTotal = 0;
                HiddenField paymentID = e.Item.FindControl("hdnPaymentID") as HiddenField;
                Repeater repOrders = e.Item.FindControl("rOrders") as Repeater;
                con = new SqlConnection(clsUtils.GetConnection());
                cmd = new SqlCommand("sp_Invoice", con);
                cmd.Parameters.AddWithValue("@Action", "InvoiceByID");
                cmd.Parameters.AddWithValue("@PaymentID", Convert.ToInt32(paymentID.Value));
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                cmd.CommandType = CommandType.StoredProcedure;
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                //dt.Columns.Add("TotalPrice");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        grandTotal += Convert.ToDouble(row["TotalPrice"]);
                        
                    }
                }
               DataRow dr = dt.NewRow();
                
                dr["TotalPrice"] = grandTotal;
                dt.Rows.Add(dr);
                repOrders.DataSource = dt;
                repOrders.DataBind();
            }
        }
        private sealed class CustomTemplate : ITemplate
        {
            private ListItemType ListItemType { get; set; }
            public CustomTemplate(ListItemType type)
            {
                ListItemType = type;
            }
            public void InstantiateIn(Control container)
            {
                if (ListItemType == ListItemType.Footer)
                {
                    var footer = new LiteralControl("<tr><td><b>Hungry!! Why not order food for you??</b><a href='Menu.aspx' class='badge badge-info ml-2'>Click to order</a></td></tr></tbody></table>");
                    container.Controls.Add(footer);
                }
            }
        }
    }
}