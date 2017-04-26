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
    public partial class getArtists : System.Web.UI.Page
    {
        public string myTable = "[";
        protected void Page_Load(object sender, EventArgs e)
        {
            getArtists_p();
            Response.Write(myTable);
        }







        public void getArtists_p()
        {
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DataConnect"].ConnectionString))
            {
                using (SqlCommand myCMD = new SqlCommand("RadiogogyGetArtists_p", connection))
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
                            var myRowCount = reader.GetString(3);
                            myTable += "{";
                            myTable += "\"artist_id\"    : \"" + Convert.ToString(reader.GetInt32(0)) + "\",";
                            myTable += "\"artist_name\"    : \"" + reader.GetString(1) + "\",";
                            myTable += "\"rowcount\"          : \"" + Convert.ToString(reader.GetString(3)) + "\",";
                            myTable += "\"artist_img_url\"    : \"" + reader.GetString(2) + "\"";

                            myTable += "}";
                            if (counter < Convert.ToInt32(myRowCount)  )
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