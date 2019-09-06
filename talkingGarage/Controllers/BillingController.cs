using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using talkingGarage.Models;

namespace talkingGarage.Controllers
{
    public class BillingController : Controller
    {
        //
        // GET: /Billing/
        public ActionResult Index()
        {
            if (Request.Cookies["user"] != null)
            {
                double totalPay = 0.00;
                string transactions = "";

                LogModel lgm = new LogModel();
                lgm.user_id = Request.Cookies["user"].Value;
                foreach (LogModel log in lgm.getAllLogsbyUserOutTime())
                {
                    transactions = transactions + "<tr>";
                    ParkinglotModel plm = new ParkinglotModel();
                    plm.lot_id = log.lot_id;
                    plm = plm.getLot();
                    transactions = transactions + "<td>" + plm.lot_name + "</td>" + "<td>" + log.day + " " + log.out_time + "</td><td>18 Tk</td>";

                    string duration = getDuration(log.in_time, log.out_time);
                    transactions = transactions + "<td>" + duration + "</td>";

                    double charge = getPrice(log.day, log.in_time, log.out_time, 18.00);
                    transactions = transactions + "<td>" + String.Format("{0: 0.00}", charge) + " Tk</td>";

                    if (log.pay_status.Trim() == "0")
                    {
                        transactions = transactions + "<td>Pending</td></tr>";
                        totalPay = totalPay + charge;
                    }
                    else
                    {
                        transactions = transactions + "<td>Paid</td></tr>";
                    }
                }
                ViewBag.Transactions = transactions;
                ViewBag.TotalPayable = String.Format("{0: 0.00}", totalPay);

                if (totalPay < 1.00)
                {
                    ViewBag.DueDate = "N/A";
                }
                else
                {
                    ViewBag.DueDate = DateTime.Now.ToString("MMM") + " " + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString() + ", " + DateTime.Now.ToString("yyyy");
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            
        }
        public ActionResult Payments()
        {
            if (Request.Cookies["user"] != null)
            {
                string user = Request.Cookies["user"].Value;
                LogModel lgm = new LogModel();
                lgm.user_id = user;
                foreach (LogModel log in lgm.getAllLogsbyUserOutTime())
                {
                    double charge = getPrice(log.day, log.in_time, log.out_time, 18.00);
                    LogModel pay = new LogModel();
                    pay.log_id = log.log_id;
                    if (log.pay_status == "0")
                    {
                        pay.pay_status = "1";
                        pay.updatePayStatus();
                    }
                
                }
                return RedirectToAction("Index", "Billing");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            

        }

        private string getDuration(string inTime, string outTime)
        {
            bool validTime = false;
            DateTime dtIn;
            DateTime dtOut;
            if (DateTime.TryParse(inTime, out dtIn))
            {
                validTime = true;
            }
            else
            {
                dtIn = DateTime.Now;
            }

            if (DateTime.TryParse(outTime, out dtOut))
            {
                validTime = true;
            }
            else
            {
                dtOut = DateTime.Now;
            }

            TimeSpan span = dtOut - dtIn;

            return String.Format("{0:00}:{1:00}:{2:00}", span.Hours, span.Minutes, span.Seconds);
        }
        private double getPrice(string day, string inTime, string outTime, double rate)
        {
            DateTime dtIn = parseTime(day + " " + inTime);
            DateTime dtOut = parseTime(day + " " + outTime); ;
           
            TimeSpan span = dtOut - dtIn;
            double charge = span.TotalMinutes * (rate/60);

            return charge;
        }
        private DateTime parseTime(string time)
        {
            DateTime dt;
            if(DateTime.TryParseExact(time, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                return dt;
            }
            else
            {
                dt = DateTime.Now;
                return dt;
            }
            
        }
	}

}
