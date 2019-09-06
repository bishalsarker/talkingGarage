using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace talkingGarage.Models
{
    public class BookingModel
    {
        public string book_id { get; set; }
        public string lot_id { get; set; }
        public string user_id { get; set; }
        public string day { get; set; }
        public string tm { get; set; }
        public string status { get; set; }
        public string response { get; set; }

        SqlConnection conn;
        Utility ut = new Utility();

        public BookingModel()
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

        public void addBooking()
        {
            string query = "insert into parking_bookings(lot_id, user_id, day, tm, status) values('" + ut.encode(this.lot_id) + "', '" + ut.encode(this.user_id) + "', '" + ut.encode(this.day) + "', '" + ut.encode(this.tm) + "', '" + ut.encode(this.status) + "')";
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
        public List<BookingModel> getAllBookingByUserDayStatus()
        {
            List<BookingModel> bookList = new List<BookingModel>();
            string query = "select * from parking_bookings where user_id='" + this.user_id + "' and day='" + this.day + "' and status='" + this.status + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bookList.Add(new BookingModel
                    {
                        book_id = reader["book_id"] + "",
                        lot_id = ut.decode(reader["lot_id"] + ""),
                        user_id = ut.decode(reader["user_id"] + ""),
                        day = ut.decode(reader["day"] + ""),
                        tm = ut.decode(reader["tm"] + ""),
                        status = ut.decode(reader["status"] + "")
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

            return bookList;
        }
        public List<BookingModel> getAllBookingByLotDayStatus()
        {
            List<BookingModel> bookList = new List<BookingModel>();
            string query = "select * from parking_bookings where lot_id='" + this.lot_id + "' and day='" + this.day + "' and status='" + this.status + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bookList.Add(new BookingModel
                    {
                        book_id = reader["book_id"] + "",
                        lot_id = ut.decode(reader["lot_id"] + ""),
                        user_id = ut.decode(reader["user_id"] + ""),
                        day = ut.decode(reader["day"] + ""),
                        tm = ut.decode(reader["tm"] + ""),
                        status = ut.decode(reader["status"] + "")
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

            return bookList;
        }
        public List<BookingModel> getAllBookingByLotUserDayStatus()
        {
            List<BookingModel> bookList = new List<BookingModel>();
            string query = "select * from parking_bookings where lot_id='" + this.lot_id + "' and user_id='" + this.user_id + "' and day='" + this.day + "' and status='" + this.status + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bookList.Add(new BookingModel
                    {
                        book_id = reader["book_id"] + "",
                        lot_id = ut.decode(reader["lot_id"] + ""),
                        user_id = ut.decode(reader["user_id"] + ""),
                        day = ut.decode(reader["day"] + ""),
                        tm = ut.decode(reader["tm"] + ""),
                        status = ut.decode(reader["status"] + "")
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

            return bookList;
        }
        public BookingModel getBookingByUserDayStatus()
        {
            BookingModel book = new BookingModel();
            string query = "select * from parking_bookings where user_id='" + this.user_id + "' and day='" + this.day + "' and status='" + this.status + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    book.book_id = reader["book_id"] + "";
                    book.lot_id = reader["lot_id"] + "";
                    book.user_id = reader["user_id"] + "";
                    book.day = reader["day"] + "";
                    book.tm = reader["tm"] + "";
                    book.status = reader["status"] + "";
                }

                reader.Close();
                closeConn();
                this.response = "200";
            }
            else
            {
                this.response = "500";
            }

            return book;
        }
        public BookingModel getBookingByLotUserDayTmStatus()
        {
            BookingModel book = new BookingModel();
            string query = "select * from parking_bookings where lot_id='" + this.lot_id + "' and user_id='" + this.user_id + "' and day='" + this.day + "' and tm='" + this.tm + "' and status='" + this.status + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    book.book_id = reader["book_id"] + "";
                }

                reader.Close();
                closeConn();
                this.response = "200";
            }
            else
            {
                this.response = "500";
            }

            return book;
        }
        public BookingModel getBookingByBookId()
        {
            BookingModel book = new BookingModel();
            string query = "select * from parking_bookings where book_id='" + this.book_id + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    book.lot_id = reader["lot_id"] + "";
                    book.user_id = reader["user_id"] + "";
                    book.day = reader["day"] + "";
                    book.tm = reader["tm"] + "";
                    book.status = reader["status"] + "";
                }

                reader.Close();
                closeConn();
                this.response = "200";
            }
            else
            {
                this.response = "500";
            }

            return book;
        }
        public void updateBooking()
        {
            string query = "update parking_bookings set status='" + ut.encode(this.status) + "' where book_id ='" + this.book_id + "'";
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
        public void deleteBooking()
        {
            string query = "delete from parking_bookings where book_id='" + this.book_id + "'";
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