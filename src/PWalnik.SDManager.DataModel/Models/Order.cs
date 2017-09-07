using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PWalnik.SDManager.DataModel.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int CustomerId { get; set; }
        public string EmployeeId { get; set; }

        [Column(TypeName = "Date")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        [Required(ErrorMessage = "You must provide quantity")]
        [Range(0,Int32.MaxValue, ErrorMessage = "Value should be greater than 0")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "You must provide an unit price")]
        [Range(0, double.MaxValue, ErrorMessage = "Value should be greater than or equal to 0")]
        public double UnitPrice { get; set; }
        [Range(0,100, ErrorMessage = "Valu must be between 0 and 100%")]
        public double Discount { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
