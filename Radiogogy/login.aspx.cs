using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataConnect.Radiogogy
{
    public partial class login : System.Web.UI.Page
    {
        public string myTable = "[";
        protected void Page_Load(object sender, EventArgs e)
        {
            var fbEmail = Request["fbEmail"];
            var fbUsername = Request["FBUsername"];
            var fbID = Request["fbid"];

            login_p(fbEmail, fbUsername, fbID);
            Response.Write(myTable);

        }
        public void login_p(string fbEmail, string fbUsername, string fbID)
        {
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DataConnect"].ConnectionString))
            {
                using (SqlCommand myCMD = new SqlCommand("radiogogyLogin_p", connection))
                {
                    myCMD.CommandType = CommandType.StoredProcedure;
                    myCMD.Parameters.Add("@fbEmail", SqlDbType.VarChar).Value = fbEmail;
                    myCMD.Parameters.Add("@fbUsername", SqlDbType.VarChar).Value = fbUsername;
                    myCMD.Parameters.Add("@fbID", SqlDbType.VarChar).Value = fbID;


                    connection.Open();
                    SqlDataReader reader;
                    reader = myCMD.ExecuteReader();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {

                            myTable += "{";
                            myTable += "\"customer_id\"    : \"" + Convert.ToString(reader.GetInt32(0)) + "\",";
                            myTable += "\"package\"  : \"" + Convert.ToString(reader.GetInt32(1)) + "\",";
                            myTable += "\"packageName\"         : \"" + reader.GetString(2) + "\",";
                            myTable += "\"mediaClass\" : \"" + reader.GetString(3) + "\",";
                            myTable += "\"fbEmail\" : \"" + reader.GetString(4) + "\",";
                            myTable += "\"fbUsername\" : \"" + reader.GetString(5) + "\"";

                            myTable += "}]";


                        }

                    }

                }
                connection.Close();
            }
        }
    }
}