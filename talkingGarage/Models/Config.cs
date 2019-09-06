using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace talkingGarage.Models
{
    public class Config
    {
        public string getConnStr()
        {
            return "Data Source=(local);Initial Catalog=tgdb;Integrated Security=SSPI";
            //return "Data Source=SQL2014;Initial Catalog=bishalsarkerdb;Persist Security Info=True;User ID=bishal;Password=a1b1c1d1";
        }
    }
}