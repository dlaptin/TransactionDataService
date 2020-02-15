using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class Transaction
    {
        public string Id { get; set; }

        public string Payment { get; set; }

        public Status Status { get; set; }

        public override string ToString()
        {
            return $"id:{Id}, payment:{Payment}, Status: {Status}";
        }
    }
}