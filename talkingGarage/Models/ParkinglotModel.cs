using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace talkingGarage.Models
{
    public class ParkinglotModel
    {
        public string lot_id { get; set; }
        public string lot_name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string max_vehicle { get; set; }
        public string response { get; set; }

        SqlConnection conn;
        Utility ut = new Utility();

        public ParkinglotModel()
        {
            Config db = new Config();
            conn = new SqlConnection(db.getConnStr());
        }
        private bool openConn()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }
        private bool closeConn()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }

        public void addLot()
        {
            string query = "insert into parking_lots(lot_name, latitude, longitude, max_vehicle) values('" + ut.encode(this.lot_name) + "', '" + ut.encode(this.latitude) + "', '" + ut.encode(this.longitude) + "', '" + ut.encode(this.max_vehicle) + "')";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                closeConn();
                this.response = "200";
            }
            else
            {
                this.response = "500";
            }
        }
        public ParkinglotModel getLot()
        {
            ParkinglotModel plot = new ParkinglotModel();
            string query = "select * from parking_lots where lot_id='" + this.lot_id + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    plot.lot_name = ut.decode(reader["lot_name"] + "");
                    plot.latitude = ut.decode(reader["latitude"] + "");
                    plot.longitude= ut.decode(reader["longitude"] + "");
                    plot.max_vehicle = ut.decode(reader["max_vehicle"] + "");
                }

                reader.Close();
                closeConn();
                plot.response = "200";
            }
            else
            {
                plot.response = "500";
            }

            return plot;
        }
        public List<ParkinglotModel> getAllLots()
        {
            List<ParkinglotModel> pageList = new List<ParkinglotModel>();
            string query = "select * from parking_lots";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pageList.Add(new ParkinglotModel
                    {
                        lot_id = reader["lot_id"] + "",
                        lot_name = ut.decode(reader["lot_name"] + ""),
                        latitude = ut.decode(reader["latitude"] + ""),
                        longitude = ut.decode(reader["longitude"] + ""),
                        max_vehicle = ut.decode(reader["max_vehicle"] + "")
                    });
                }

                reader.Close();
                closeConn();
                this.response = "200";
            }
            else
            {
                this.response = "500";
            }

            return pageList;
        }
        public void updateLot()
        {
            string query = "update parking_lots set lot_name='" + ut.encode(this.lot_name) + "', latitude='" + ut.encode(this.latitude) + "', longitude='" + ut.encode(this.longitude) + "', max_vehicle='" + ut.encode(this.max_vehicle) + "' where lot_id ='" + this.lot_id + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                closeConn();
                this.response = "200";
            }
            else
            {
                this.response = "500";
            }
        }
        public void deleteLot()
        {
            string query = "delete from parking_lots where lot_id='" + this.lot_id + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                closeConn();
                this.response = "200";
            }
            else
            {
                this.response = "500";
            }
        }
    }
}