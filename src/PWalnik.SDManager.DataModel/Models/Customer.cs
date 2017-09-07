using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PWalnik.SDManager.DataModel.Models
{
    public class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "You must provide a company name.")]
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"[0-9]{2}\-[0-9]{3}$", ErrorMessage = "Invalid postal code.")]
        public string PostalCode { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(?:\d{9}|00\d{10}|\+\d{2}\d{9})$", ErrorMessage = "Invalid phone")]
        public string Phone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
