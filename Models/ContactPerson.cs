using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EverBill.Models
{
    public class ContactPerson
    {
        public int ContactPersonId { get; set; }
        public string? ContactFullName { get; set; }
        public int ContactPhoneNumber { get; set; }
        public string? ContactCompany { get; set; }
    }
}