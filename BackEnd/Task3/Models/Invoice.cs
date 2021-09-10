using System;
using System.Collections.Generic;

#nullable disable

namespace Task3.Models
{
    public partial class Invoice
    {
        public int StatementID { get; set; }
        public DateTime StatementDate { get; set; }
        public double PreBalance { get; set; }
        public double CurCharges { get; set; }
        public double Total { get; set; }
        public double LateFees { get; set; }
        public string Username { get; set; }

        public virtual Member UsernameNavigation { get; set; }
    }
}