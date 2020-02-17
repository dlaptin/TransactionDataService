using System;

namespace WebUI.Models
{
    public class MainModel
    {
        public string ValidationMessages { get; set; }

        public string Transactions { get; set; }

        public string Code { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Status? Status { get; set; }
    }
}