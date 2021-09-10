using System;
using System.Collections.Generic;

#nullable disable

namespace Task3.Models
{
    public partial class Member
    {
        public Member()
        {
            Invoices = new HashSet<Invoice>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}