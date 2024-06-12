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
    public partial class Default : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetCategories();
                //GetProducts();
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
        }
    }
}