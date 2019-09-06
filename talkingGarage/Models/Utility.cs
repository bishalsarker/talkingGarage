﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace talkingGarage.Models
{
    public class Utility
    {
        public string encode(string text)
        {
            return WebUtility.HtmlEncode(text);
        }

        public string decode(string text)
        {
            return WebUtility.HtmlDecode(text);
        }
    }
}