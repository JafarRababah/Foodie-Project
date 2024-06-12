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
    public partial class Payment : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlCommand newcmd;
        SqlDataAdapter adapter;
        SqlDataReader reader;
        SqlDataReader rdr;
        DataTable dt;
        SqlTransaction transaction=null;
        string _name=string.Empty;string _cardNo=string.Empty;string _expiryDate=string.Empty;string _cvv=string.Empty;
        string _address=string.Empty;string _paymentMode=string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                
            }
        }

        protected void lbCardSubmit_Click(object sender, EventArgs e)
        {
            
            _name = txtName.Text.Trim();
            _cardNo = txtCardNo.Text.Trim();
            _cardNo=string.Format("************{0}*",txtCardNo.Text.Trim().Substring(12,4));
            _expiryDate=txtExpMonth.Text.Trim()+"/"+txtExpYear.Text.Trim();
            _cvv=txtCvv.Text.Trim();
            _address=txtAddress.Text.Trim();
            _paymentMode = "card";
            if (Session["UserID"] != null)
            {

                OrderPayment(_name,_cardNo,_expiryDate,_cvv,_address,_paymentMode);
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void lbCodSubmit_Click(object sender, EventArgs e)
        {
            _address=txtAddress.Text.Trim();
            _paymentMode = "cod";
            if (Session["UserID"] != null)
            {
                OrderPayment(_name, _cardNo, _expiryDate, _cvv, _address, _paymentMode);

            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        void OrderPayment(string name,string cardNo,string expiryDate,string cvv,string address,string paymentMode)
        {
            int paymentID=0;int productID=0;int quantity = 0;
            dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[7]
            {
                new DataColumn("OrderNo",typeof(string)),
                new DataColumn("ProductID",typeof(int)),
                new DataColumn("Quantity",typeof(int)),
                new DataColumn("UserID",typeof(int)),
                new DataColumn("Status",typeof(string)),
                new DataColumn("PaymentID",typeof(int)),
                new DataColumn("OrderDate",typeof(DateTime)),
            });
            con = new SqlConnection(clsUtils.GetConnection());
            con.Open();
            #region Sql Transaction
            transaction = con.BeginTransaction();
            cmd = new SqlCommand("sp_SavePayment", con,transaction);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@CardNo", cardNo);
            cmd.Parameters.AddWithValue("@ExpiryDate", expiryDate);
            cmd.Parameters.AddWithValue("@Cvv", cvv);
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@PaymentMode", paymentMode);
            cmd.Parameters.Add("@InsertedID", SqlDbType.Int);
            cmd.Parameters["@InsertedID"].Direction = ParameterDirection.Output;
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.ExecuteNonQuery();
                paymentID = Convert.ToInt32(cmd.Parameters["@InsertedID"].Value);
                #region Getting Cart Item's
                cmd = new SqlCommand("sp_Cart", con,transaction);
                cmd.Parameters.AddWithValue("@Action", "Select");
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                cmd.CommandType = CommandType.StoredProcedure;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    productID = (int)reader["ProductID"];
                    quantity = (int)reader["Quantity"];
                    //update product qty
                    UpdateQuantity(productID,quantity,transaction,con);
                    // Delete cart item
                    DeleteCartItem(productID, transaction, con);
                    dt.Rows.Add(clsUtils.GetUniqueID(), productID, quantity, (int)Session["UserID"],
                        "Pending",paymentID,Convert.ToDateTime(DateTime.Now));
                }
                reader.Close();
                #endregion Getting Cart Item's
                #region Order Details
                if(dt.Rows.Count > 0)
                {
                    cmd = new SqlCommand("sp_SaveOrders", con,transaction);
                    cmd.Parameters.AddWithValue("@Orders", dt);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                #endregion Order Details
                transaction.Commit();
                lblMsg.Visible = true;
                lblMsg.Text = "Your items ordered successful!!";
                lblMsg.CssClass = "alert alert-success";
                Response.AddHeader("REFRESH", "1;URL=Invoice.aspx?PaymentID=" +paymentID);
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch(Exception e)
                {
                    Response.Write("<script>alert('" + e.Message + "');</script>");
                }
            }
            #endregion Sql Transaction
            finally
            {
                con.Close();
            }
        }
        void UpdateQuantity(int _productID,int _quantity,SqlTransaction sqlTransaction,SqlConnection sqlConnection)
        {
            
            
            int dbQuantity;
            cmd = new SqlCommand("sp_Product",sqlConnection,sqlTransaction);
            cmd.Parameters.AddWithValue("@Action", "GetQtyByID");
            cmd.Parameters.AddWithValue("@ProductID", _productID);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dbQuantity = (int)rdr["Quantity"];
                    if (dbQuantity > _quantity && dbQuantity > 2)
                    {
                        dbQuantity = dbQuantity - _quantity;
                        //cmd = new SqlCommand("sp_QtyUpdate", sqlConnection, sqlTransaction);
                        cmd = new SqlCommand("sp_Product", sqlConnection, sqlTransaction);
                        cmd.Parameters.AddWithValue("@Action", "QtyUpdate");
                        cmd.Parameters.AddWithValue("@Quantity", dbQuantity);
                        cmd.Parameters.AddWithValue("@ProductID", _productID);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Update Error :" + ex.Message + "');</script>");
            }
            
        }
        void DeleteCartItem(int _productID,  SqlTransaction sqlTransaction, SqlConnection sqlConnection)
        {
            cmd = new SqlCommand("sp_Cart", sqlConnection, sqlTransaction);
            cmd.Parameters.AddWithValue("@Action", "Delete");
            cmd.Parameters.AddWithValue("@ProductID", _productID);
            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error Delete:" + ex.Message + "');</script>");
            }
        }
    }
}