using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Foodie
{

    public class clsUtils
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        SqlDataReader reader;
        DataTable dt;

        public static string GetConnection()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }
        public static bool IsValidExtention(string FileName)
        {
            bool IsValid = false;
            string[] FileExtention = { ".jpg",".png",".jpeg" };
            foreach(string file in FileExtention)
            {
                if (FileName.Contains(file))
                {
                    IsValid = true;
                    break;
                }
            }
            return IsValid;
        }
       
        public static string GetImageUrl(Object url)
        {
            string rul1 = string.Empty;
            if (string.IsNullOrEmpty(url.ToString()) || url == DBNull.Value)
            {
                rul1 = "../Images/No_image.png";
            }
            else
            {
                rul1 = string.Format("../{0}", url);
            }
            return rul1;
        }
        public  bool UpdateCartQuantity(int Quantity, int ProductID,int UserID)
        {
           
            bool isUpdated = false;
            con = new SqlConnection(GetConnection());
            cmd = new SqlCommand("sp_Cart", con);
            cmd.Parameters.AddWithValue("@Action", "Update");
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            cmd.Parameters.AddWithValue("@Quantity", Quantity);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                isUpdated = true;
            }
            catch (Exception ex)
            {
                isUpdated = false;
                System.Web.HttpContext.Current.Response.Write("<script>alert('Error -" + ex.Message + "');</script>");
            }
            finally
            {
                con.Close();
            }
            return isUpdated;
        }
        public int cartCount(int UserID)
        {
            con = new SqlConnection(GetConnection());
            cmd = new SqlCommand("sp_Cart", con);
            cmd.Parameters.AddWithValue("@Action", "Select");
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt.Rows.Count;
        }
        public static string GetUniqueID()
        {
            Guid guid= Guid.NewGuid();
            string uniqueid= guid.ToString();
            return uniqueid;
        }
    }

}