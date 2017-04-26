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
    public partial class getCollections : System.Web.UI.Page
    {
        public string myTable = "[";
        protected void Page_Load(object sender, EventArgs e)
        {
            getCollections_p();
            Response.Write(myTable);
        }
        public void getCollections_p()
        {
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DataConnect"].ConnectionString))
            {
                using (SqlCommand myCMD = new SqlCommand("RadiogogyGetCollections_p", connection))
                {
                    myCMD.CommandType = CommandType.StoredProcedure;





                    connection.Open();
                    SqlDataReader reader;
                    reader = myCMD.ExecuteReader();
                    if (reader.HasRows)
                    {
                        var counter = 1;
                        while (reader.Read())
                        {
                            var myRowCount = reader.GetString(5);
                            myTable += "{";
                            myTable += "\"cd_id\"        : \"" + reader.GetString(0) + "\",";
                            myTable += "\"title\"        : \"" + reader.GetString(1) + "\",";
                            myTable += "\"artist_id\"    : \"" + Convert.ToString(reader.GetInt32(2)) + "\",";
                            myTable += "\"artist_name\"  : \"" + reader.GetString(3) + "\",";
                            myTable += "\"cd_img_url\"   : \"" + reader.GetString(4) + "\",";
                            myTable += "\"rowcount\"     : \"" + reader.GetString(5) + "\"";
                            myTable += "}";
                            if (counter < Convert.ToInt32(myRowCount))
                            {
                                myTable += ",";
                            }
                            counter++;
                        }

                    }
                    myTable += "]";
                }
                connection.Close();

            }
        }
    }
}