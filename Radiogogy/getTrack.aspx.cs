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
    public partial class getTrack : System.Web.UI.Page
    {
        public string myTable = "[";
        protected void Page_Load(object sender, EventArgs e)
        {
            var userKey = Request["userKey"];
            var id = Request["id"];
            var tid = Request["tid"];
            var reset = Request["reset"];

            getTrack_p(userKey, id, tid, reset);
            Response.Write(myTable);


        }
        public void getTrack_p(string userKey, string id, string tid, string reset)
        {
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DataConnect"].ConnectionString))
            {
                using (SqlCommand myCMD = new SqlCommand("radiogogyGetTrack_p", connection))
                {
                    myCMD.CommandType = CommandType.StoredProcedure;
                    myCMD.Parameters.Add("@userKey", SqlDbType.VarChar).Value = userKey;
                    myCMD.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                    myCMD.Parameters.Add("@tid", SqlDbType.VarChar).Value = tid;
                    myCMD.Parameters.Add("@reset", SqlDbType.VarChar).Value = reset;

                    connection.Open();
                    SqlDataReader reader;
                    reader = myCMD.ExecuteReader();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            var trackOrder = reader.GetInt32(0);
                            //var myRowCount = reader.GetInt32(15);
                            var myRowCount = reader.GetString(16);
                            myTable += "{";
                            myTable += "\"trackOrder\"    : \"" + Convert.ToString(reader.GetInt32(0)) + "\",";
                            myTable += "\"mlr_track_id\"  : \"" + reader.GetString(1) + "\",";
                            myTable += "\"cd_id\"         : \"" + reader.GetString(2) + "\",";
                            myTable += "\"mlr_track_url\" : \"" + reader.GetString(3) + "\",";
                            myTable += "\"track_length\" : \"" + reader.GetString(4) + "\",";
                            myTable += "\"track_name\" : \"" + reader.GetString(5) + "\",";
                            myTable += "\"title\" : \"" + reader.GetString(6) + "\", ";
                            myTable += "\"artist_id\" : \"" + Convert.ToString(reader.GetInt32(7)) + "\", ";
                            myTable += "\"artist_id2\" : \"" + Convert.ToString(reader.GetInt32(8)) + "\", ";
                            myTable += "\"artist_name\" : \"" + reader.GetString(9) + "\", ";
                            myTable += "\"artist_name2\" : \"" + reader.GetString(10) + "\", ";
                            myTable += "\"artist_img_url\" : \"" + reader.GetString(11) + "\", ";
                            myTable += "\"cd_img_url\" : \"" + reader.GetString(12) + "\", ";
                            myTable += "\"cd_notes\" : \"" + reader.GetString(13) + "\", ";
                            myTable += "\"notes\" : \"" + reader.GetString(14) + "\", ";
                            myTable += "\"buy_url\" : \"" + reader.GetString(15) + "\", ";
                            myTable += "\"myRowCount\" : \"" + Convert.ToString(myRowCount) + "\",";
                            myTable += "\"dl\" : \"" + Convert.ToString(reader.GetInt32(17)) + "\"";

                            myTable += "}";
                            if (trackOrder < Convert.ToInt32(myRowCount) )
                            {
                                myTable += ",";
                            }

                        }

                    }
                    myTable += "]";

                }
                connection.Close();
            }
        }
    }
}