using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using talkingGarage.Models;

namespace talkingGarage.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Request.Cookies["user"] != null)
            {
                return RedirectToAction("Map", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Athorize(string username, string password)
        {
            UserModel user = new UserModel();
            user.user_name = username;
            user.password = password;
            int count = user.checkUserByUnamePass();
            if (count < 1 )
            {
                TempData["response"] = "Wrong username or password!";
                return RedirectToAction("Login", "Home");
            }
            else
            {
                user = user.getUserByUnamePass();
                Response.Cookies["user"].Value = user.user_id;
                Response.Cookies["user"].Expires = DateTime.Now.AddDays(15);
                return RedirectToAction("Map", "Home");
            }
        }
        public ActionResult Map()
        {
            if (Request.Cookies["user"] != null)
            {
                DateTime dt = DateTime.Now;
                BookingModel bkm = new BookingModel();
                bkm.user_id = Request.Cookies["user"].Value;
                bkm.day = dt.ToString("dd/MM/yyyy");
                bkm.status = "0";

                LogModel lgm = new LogModel();
                lgm.user_id = Request.Cookies["user"].Value;
                lgm.day = dt.ToString("dd/MM/yyyy");
                lgm.out_time = "0000AMPM";

                if (bkm.getAllBookingByUserDayStatus().Count() > 0)
                {
                    bkm = bkm.getBookingByUserDayStatus();
                    return RedirectToAction("Direction", "Home", new { book_id = bkm.book_id });
                }
                else if (lgm.getAllLogsbyUserDayOutTime().Count() > 0)
                {
                    lgm = lgm.getAllLogsbyUserDayOutTime()[0];
                    return RedirectToAction("Checkpoint", "Home", new { log_id = lgm.log_id });
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult Direction(string book_id)
        {
            if (Request.Cookies["user"] != null)
            {
                BookingModel bkm = new BookingModel();
                bkm.book_id = book_id;
                bkm = bkm.getBookingByBookId();

                if (bkm.lot_id == "" || bkm.lot_id == null)
                {
                    return RedirectToAction("Map", "Home");
                }
                else
                {
                    if (getTimeDifference(bkm.tm) > 0.00 && getTimeDifference(bkm.tm) < 2.00)
                    {
                        ViewBag.LotId = bkm.lot_id;
                        ViewBag.BookId = book_id;
                        return View();
                    }
                    else
                    {
                        bkm.book_id = book_id;
                        bkm.deleteBooking();
                        return RedirectToAction("Map", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }      
        }
        public ActionResult Checkpoint(string log_id)
        {
            if (Request.Cookies["user"] != null)
            {
                LogModel lgm = new LogModel();
                lgm.log_id = log_id;
                lgm = lgm.getLogbyId();
                if (lgm.out_time == "0000AMPM")
                {
                    ViewBag.LotId = lgm.lot_id;
                    ViewBag.LogId = log_id;
                    return View();
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
        public ActionResult Logout()
        {
            Response.Cookies["user"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Login", "Home");
        }

        private double getTimeDifference(string time)
        {
            DateTime dtNow = DateTime.Now;
            DateTime dtTarget;
            if (DateTime.TryParse(time, out dtTarget))
            {
                return (dtNow.Subtract(dtTarget).TotalMinutes);
            }
            else
            {
                return 0.00;
            }
        }
	}
}