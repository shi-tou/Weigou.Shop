using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weigou.Api.Base
{
    public class MyHashtable : Hashtable
    {
        public override void Add(object key, object value)
        {
            base.Add(key.ToString().ToLower(), value);
        }
        public override bool Contains(object key)
        {
            return base.Contains(key.ToString().ToLower());
        }
        public override bool ContainsKey(object key)
        {
            return base.ContainsKey(key.ToString().ToLower());
        }
    }
    
}