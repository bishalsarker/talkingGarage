using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace talkingGarage.Models
{
    public class LogModel
    {
        public string log_id { get; set; }
        public string lot_id { get; set; }
        public string user_id { get; set; }
        public string day { get; set; }
        public string in_time { get; set; }
        public string out_time { get; set; }
        public string pay_status { get; set; }
        public string response { get; set; }

        SqlConnection conn;
        Utility ut = new Utility();

        public LogModel()
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

        public void addLog()
        {
            string query = "insert into parking_logs(lot_id, user_id, day, in_time, out_time, pay_status) values('" + ut.encode(this.lot_id) + "', '" + ut.encode(this.user_id) + "', '" + ut.encode(this.day) + "', '" + ut.encode(this.in_time) + "', '" + ut.encode(this.out_time) + "', '" + ut.encode(this.pay_status) + "')";
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
        public LogModel getLogbyId()
        {
            LogModel log = new LogModel();
            string query = "select * from parking_logs where log_id='" + this.log_id + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    log.lot_id = ut.decode(reader["lot_id"] + "");
                    log. user_id = ut.decode(reader["user_id"] + "");
                    log.day = ut.decode(reader["day"] + "");
                    log.in_time = ut.decode(reader["in_time"] + "");
                    log.out_time = ut.decode(reader["out_time"] + "");
                    log.pay_status = ut.decode(reader["pay_status"] + "");
                }

                reader.Close();
                closeConn();
                this.response = "200";
            }
            else
            {
                this.response = "500";
            }

            return log;
        }
        public List<LogModel> getAllLogsbyUserDayOutTime()
        {
            List<LogModel> logList = new List<LogModel>();
            string query = "select * from parking_logs where user_id='" + this.user_id + "' and day='" + this.day + "' and out_time='" + this.out_time + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    logList.Add(new LogModel
                    {
                        log_id = reader["log_id"] + "",
                        lot_id = ut.decode(reader["lot_id"] + ""),
                        user_id = ut.decode(reader["user_id"] + ""),
                        day = ut.decode(reader["day"] + ""),
                        in_time = ut.decode(reader["in_time"] + ""),
                        out_time = ut.decode(reader["out_time"] + ""),
                        pay_status = ut.decode(reader["pay_status"] + "")
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

            return logList;
        }
        public List<LogModel> getAllLogsbyLotUserDay()
        {
            List<LogModel> logList = new List<LogModel>();
            string query = "select * from parking_logs where lot_id='" + this.lot_id + "' and user_id='" + this.user_id + "' and day='" + this.day + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    logList.Add(new LogModel
                    {
                        log_id = reader["log_id"] + "",
                        lot_id = ut.decode(reader["lot_id"] + ""),
                        user_id = ut.decode(reader["user_id"] + ""),
                        day = ut.decode(reader["day"] + ""),
                        in_time = ut.decode(reader["in_time"] + ""),
                        out_time = ut.decode(reader["out_time"] + ""),
                        pay_status = ut.decode(reader["pay_status"] + "")
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

            return logList;
        }
        public List<LogModel> getAllLogsbyUserOutTime()
        {
            List<LogModel> logList = new List<LogModel>();
            string query = "select * from parking_logs where user_id='" + this.user_id + "' and out_time != '0000AMPM' order by log_id desc";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    logList.Add(new LogModel
                    {
                        log_id = reader["log_id"] + "",
                        lot_id = ut.decode(reader["lot_id"] + ""),
                        user_id = ut.decode(reader["user_id"] + ""),
                        day = ut.decode(reader["day"] + ""),
                        in_time = ut.decode(reader["in_time"] + ""),
                        out_time = ut.decode(reader["out_time"] + ""),
                        pay_status = ut.decode(reader["pay_status"] + "")
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

            return logList;
        }
        public List<LogModel> getAllLogsbyLotUserDayOutTime()
        {
            List<LogModel> logList = new List<LogModel>();
            string query = "select * from parking_logs where lot_id='" + this.lot_id + "' and user_id='" + this.user_id + "' and day='" + this.day + "' and out_time='" + this.out_time + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    logList.Add(new LogModel
                    {
                        log_id = reader["log_id"] + "",
                        lot_id = ut.decode(reader["lot_id"] + ""),
                        user_id = ut.decode(reader["user_id"] + ""),
                        day = ut.decode(reader["day"] + ""),
                        in_time = ut.decode(reader["in_time"] + ""),
                        out_time = ut.decode(reader["out_time"] + ""),
                        pay_status = ut.decode(reader["pay_status"] + "")
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

            return logList;
        }
        public List<LogModel> getAllLogsbyLotDayOutTime()
        {
            List<LogModel> logList = new List<LogModel>();
            string query = "select * from parking_logs where lot_id='" + this.lot_id + "' and day='" + this.day + "' and out_time='0000AMPM'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    logList.Add(new LogModel
                    {
                        log_id = reader["log_id"] + "",
                        lot_id = ut.decode(reader["lot_id"] + ""),
                        user_id = ut.decode(reader["user_id"] + ""),
                        day = ut.decode(reader["day"] + ""),
                        in_time = ut.decode(reader["in_time"] + ""),
                        out_time = ut.decode(reader["out_time"] + ""),
                        pay_status = ut.decode(reader["pay_status"] + "")
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

            return logList;
        }
        public void updateOutTime()
        {
            string query = "update parking_logs set out_time='" + ut.encode(this.out_time) + "' where log_id ='" + this.log_id + "'";
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
        public void updatePayStatus()
        {
            string query = "update parking_logs set pay_status='" + ut.encode(this.pay_status) + "' where log_id ='" + this.log_id + "'";
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