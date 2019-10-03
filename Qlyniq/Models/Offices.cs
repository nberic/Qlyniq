using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qlyniq.Models
{
    [Table("offices")]
    public partial class Offices
    {
        public Offices()
        {
            EmployeesDeanOffice = new HashSet<Employees>();
            EmployeesOffice = new HashSet<Employees>();
        }

        [Key]
        public uint Id { get; set; }

        [Display(Name = "Office Name")]
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        public uint? OfficeNumber { get; set; }

        [Column(TypeName = "decimal(10,0)")]
        public decimal Budget { get; set; } = 0.0M;


        [InverseProperty("DeanOffice")]
        public virtual ICollection<Employees> EmployeesDeanOffice { get; set; }
        [InverseProperty("Office")]
        public virtual ICollection<Employees> EmployeesOffice { get; set; }
    }
}
