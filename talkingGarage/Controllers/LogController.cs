using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using talkingGarage.Models;

namespace talkingGarage.Controllers
{
    public class LogController : Controller
    {
        //
        // GET: /Log/
        public ActionResult checkIfOut(string log_id)
        {
            LogDetails ldm = new LogDetails();
            LogModel lgm = new LogModel();
            lgm.log_id = log_id;
            lgm = lgm.getLogbyId();

            if (lgm.out_time != "0000AMPM")
            {
                ldm.time = "0:0:0";
                ldm.checkOut = "1";
            }
            else
            {
                DateTime dtNow = DateTime.Now;
                DateTime dtTarget;
                if (DateTime.TryParse(lgm.in_time, out dtTarget))
                {
                    TimeSpan span = dtNow - dtTarget;
                    ldm.time = String.Format("{0:00}:{1:00}:{2:00}", span.Hours, span.Minutes, span.Seconds);
                    ldm.checkOut = "0";
                }
                else
                {
                    ldm.time = "0:0:0";
                    ldm.checkOut = "0";
                }
            }
            return Json(ldm, JsonRequestBehavior.AllowGet);
        }
	}
}
public class LogDetails
{
    public string time { get; set; }
    public string checkOut { get; set; }
}