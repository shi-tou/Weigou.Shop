﻿using System;
using System.Collections.Generic;
using System.Web;

namespace Weigou.Pay.Weixin.Pay
{
    public class WxPayException : Exception 
    {
        public WxPayException(string msg) : base(msg) 
        {

        }
     }
}