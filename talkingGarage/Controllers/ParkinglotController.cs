using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using talkingGarage.Models;

namespace talkingGarage.Controllers
{
    public class ParkinglotController : Controller
    {
        //
        // GET: /Parkinglot/
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CheckIn(string lot_id, string card_number)
        {
            string response = "";
            UserModel user = new UserModel();
            user.card_number = card_number;
            if (user.checkIfCardExists() < 1)
            {
                response = "500";
            }
            else
            {
                user = user.getUserByCard();
                string user_id = user.user_id;
                DateTime now = DateTime.Now;
                BookingModel bkm = new BookingModel();
                bkm.lot_id = lot_id;
                bkm.user_id = user_id;
                bkm.day = now.ToString("dd/MM/yyyy");
                bkm.status = "0";
                int countBookings = bkm.getAllBookingByLotUserDayStatus().Count();

                if (!(countBookings < 1))
                {
                    LogModel lgm = new LogModel();
                    lgm.lot_id = lot_id;
                    lgm.user_id = user_id;
                    lgm.day = now.ToString("dd/MM/yyyy");
                    lgm.out_time = "0000AMPM";
                    lgm.pay_status = "0";

                    if (!(lgm.getAllLogsbyLotUserDayOutTime().Count() > 0))
                    {
                        lgm.in_time = now.ToString("hh:mm:ss tt");
                        lgm.addLog();
                        bkm = bkm.getAllBookingByLotUserDayStatus()[0];
                        bkm.deleteBooking();
                        response = "200";
                    }
                    else
                    {
                        response = "500";
                    }

                }
                else
                {
                    response = "500";
                }
            }      
            return Json(response, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult CheckOut(string lot_id, string card_number)
        {
            string response = "";
            UserModel user = new UserModel();
            user.card_number = card_number;
            if (user.checkIfCardExists() < 1)
            {
                response = "500";
            }
            else
            {
                user = user.getUserByCard();
                string user_id = user.user_id;
                DateTime now = DateTime.Now;
                LogModel lgm = new LogModel();
                List<LogModel> logList = new List<LogModel>();
                lgm.lot_id = lot_id;
                lgm.user_id = user_id;
                lgm.day = now.ToString("dd/MM/yyyy");
                foreach (LogModel log in lgm.getAllLogsbyLotUserDay())
                {
                    logList.Add(log);
                }

                List<LogModel> list = new List<LogModel>();
                if (logList.Count() > 0)
                {
                    foreach (LogModel l in logList.OrderByDescending(x => x.log_id))
                    {
                        list.Add(l);
                    }

                    LogModel lg = new LogModel();
                    lg = list[0];

                    if (lg.out_time == "0000AMPM")
                    {
                        lgm.log_id = lg.log_id;
                        lgm.out_time = now.ToString("hh:mm:ss tt");
                        lgm.updateOutTime();
                        response = "200";
                    }
                    else
                    {
                        response = "500";
                    }

                }
                else
                {
                    response = "500";
                }
            }

            return Json(response, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult getLots()
        {
            List<pLotModel> lots = new List<pLotModel>();
            ParkinglotModel plm = new ParkinglotModel();
            foreach (ParkinglotModel lot in plm.getAllLots())
            {
                DateTime dt = DateTime.Now;
                LogModel lgm = new LogModel();
                lgm.lot_id = lot.lot_id;
                lgm.day = dt.ToString("dd/MM/yyyy");
                int engagedCount = lgm.getAllLogsbyLotDayOutTime().Count();
                int freeCount = int.Parse(lot.max_vehicle) - engagedCount;
                if (freeCount > 1)
                {
                    lots.Add(new pLotModel
                    {
                        lot_id = lot.lot_id,
                        lot_name = lot.lot_name,
                        lat = double.Parse(lot.latitude),
                        lon = double.Parse(lot.longitude),
                        desc = freeCount + "/" + lot.max_vehicle
                    });
                }
            }

            return Json(lots, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult getLot(string lot_id)
        {
            pLotModel plot = new pLotModel();
            ParkinglotModel plm = new ParkinglotModel();
            plm.lot_id = lot_id;
            plm = plm.getLot();

            plot.lot_id = lot_id;
            plot.lot_name = plm.lot_name;
            plot.lat = double.Parse(plm.latitude);
            plot.lon = double.Parse(plm.longitude);
            plot.desc = getFreeSpaces(lot_id, plm.max_vehicle) + "/" + plm.max_vehicle;

            return Json(plot, JsonRequestBehavior.AllowGet);

        }


        private int getFreeSpaces(string lot_id, string max_vehicle)
        {
            DateTime dt = DateTime.Now;
            BookingModel bkm = new BookingModel();
            bkm.lot_id = lot_id;
            bkm.day = dt.ToString("dd/MM/yyyy");
            bkm.status = "0";
            int countBookings = bkm.getAllBookingByLotDayStatus().Count();

            LogModel lgm = new LogModel();
            lgm.lot_id = lot_id;
            lgm.day = dt.ToString("dd/MM/yyyy");
            int engagedCount = lgm.getAllLogsbyLotDayOutTime().Count();

            int freeCount = int.Parse(max_vehicle) - (engagedCount + countBookings);

            return freeCount;

        }
	}
}