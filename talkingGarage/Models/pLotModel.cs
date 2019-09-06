using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace talkingGarage.Models
{
    public class pLotModel
    {
        public string lot_id { get; set; }
        public string lot_name { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string desc { get; set; }
    }
}