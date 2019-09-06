using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace talkingGarage.Models
{
    public class UserModel
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string card_number { get; set; }
        public string response { get; set; }

        SqlConnection conn;
        Utility ut = new Utility();

        public UserModel()
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

        public void addUser()
        {
            string query = "insert into parking_users(user_name, email, password, card_number) values('" + ut.encode(this.user_name) + "', '" + ut.encode(this.email) + "', '" + ut.encode(this.password) + "', '" + ut.encode(this.card_number) + "')";
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
        public UserModel getUserById()
        {
            UserModel user = new UserModel();
            string query = "select * from parking_users where user_id='" + this.user_id + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.user_name = ut.decode(reader["user_name"] + "");
                    user.email = ut.decode(reader["email"] + "");
                    user.password = ut.decode(reader["password"] + "");
                    user.card_number = ut.decode(reader["card_number"] + "");
                }

                reader.Close();
                closeConn();
                user.response = "200";
            }
            else
            {
                user.response = "500";
            }

            return user;
        }
        public UserModel getUserByUnamePass()
        {
            UserModel user = new UserModel();
            string query = "select * from parking_users where user_name='" + this.user_name + "' and password='" + this.password + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.user_id = ut.decode(reader["user_id"] + "");
                    user.user_name = ut.decode(reader["user_name"] + "");
                    user.email = ut.decode(reader["email"] + "");
                    user.password = ut.decode(reader["password"] + "");
                    user.card_number = ut.decode(reader["card_number"] + "");
                }

                reader.Close();
                closeConn();
                user.response = "200";
            }
            else
            {
                user.response = "500";
            }

            return user;
        }
        public UserModel getUserByCard()
        {
            UserModel user = new UserModel();
            string query = "select * from parking_users where card_number='" + this.card_number + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.user_id = ut.decode(reader["user_id"] + "");
                    user.user_name = ut.decode(reader["user_name"] + "");
                    user.email = ut.decode(reader["email"] + "");
                    user.password = ut.decode(reader["password"] + "");
                    user.card_number = ut.decode(reader["card_number"] + "");
                }

                reader.Close();
                closeConn();
                user.response = "200";
            }
            else
            {
                user.response = "500";
            }

            return user;
        }
        public int checkUserByUnamePass()
        {
            int count = 0;
            string query = "select count(*) from parking_users where user_name='" + this.user_name + "' and password='" + this.password + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                count = int.Parse(cmd.ExecuteScalar() + "");
                closeConn();
            }

            return count;
        }
        public int checkIfCardExists()
        {
            int count = 0;
            string query = "select count(*) from parking_users where card_number='" + this.card_number + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                count = int.Parse(cmd.ExecuteScalar() + "");
                closeConn();
            }

            return count;
        }
        /*
        public List<ParkinglotModel> getAllUsers()
        {
            List<ParkinglotModel> pageList = new List<ParkinglotModel>();
            string query = "select * from parking_users";
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
        public void updateBills()
        {
            string query = "update parking_users set billDate='" + ut.encode(this.billDate) + "', payDay='" + ut.encode(this.payDay) + "' where user_id ='" + this.user_id + "'";
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
        public void deleteUser()
        {
            string query = "delete from parking_users where user_id='" + this.user_id + "'";
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
         * */
    }
}