using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PWalnik.SDManager.DataModel.Models
{
    public class Employee
    {
        public Employee()
        {
            Orders = new HashSet<Order>();
        }

        [ForeignKey("ApplicationUser")]
        public string EmployeeId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
