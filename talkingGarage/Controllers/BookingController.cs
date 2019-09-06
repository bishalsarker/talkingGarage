using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using talkingGarage.Models;

namespace talkingGarage.Controllers
{
    public class BookingController : Controller
    {
        //
        // GET: /Booking/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Hold(string lot_id)
        {
            if (Request.Cookies["user"] != null)
            {
                string day, tm;
                DateTime dt = DateTime.Now;
                BookingModel bkm = new BookingModel();
                bkm.lot_id = lot_id;
                bkm.user_id = Request.Cookies["user"].Value;
                bkm.day = dt.ToString("dd/MM/yyyy");
                bkm.status = "0";
                int count = bkm.getAllBookingByUserDayStatus().Count();

                if (count < 1)
                {
                    bkm = new BookingModel();
                    bkm.lot_id = lot_id;
                    bkm.user_id = Request.Cookies["user"].Value;
                    bkm.day = dt.ToString("dd/MM/yyyy");
                    day = bkm.day;
                    bkm.tm = dt.ToString("hh:mm:ss tt");
                    tm = bkm.tm;
                    bkm.status = "0";
                    bkm.addBooking();

                    bkm = new BookingModel();
                    bkm.lot_id = lot_id;
                    bkm.user_id = Request.Cookies["user"].Value;
                    bkm.day = day;
                    bkm.tm = tm;
                    bkm.status = "0";
                    bkm = bkm.getBookingByUserDayStatus();
                    return RedirectToAction("Direction", "Home", new { book_id = bkm.book_id });
                }
                else
                {
                    return RedirectToAction("Map", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult getTimeRemaining(string book_id)
        {
            BookingDetails book = new BookingDetails();
            BookingModel bkm = new BookingModel();
            bkm.book_id = book_id;
            bkm = bkm.getBookingByBookId();

            DateTime dtNow = DateTime.Now;
            DateTime dtTarget;
            if (DateTime.TryParse(bkm.tm, out dtTarget))
            {
                dtTarget = dtTarget.AddMinutes(2.00);
                TimeSpan span = dtTarget - dtNow;
                book.time = String.Format("{0:00}:{1:00}", span.Minutes, span.Seconds);
            }
            else
            {
                book.time = "00:00";
            }

            return Json(book, JsonRequestBehavior.AllowGet);
        }
	}
}

public class BookingDetails
{
    public string time { get; set; }
}