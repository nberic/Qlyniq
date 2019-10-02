using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qlyniq.Models
{
    [Table("deans")]
    public partial class Deans
    {
        public Deans()
        {
            Offices = new HashSet<Offices>();
        }

        [Key]
        public uint Id { get; set; }

        [Required]
        [Display(Name = "Office")]
        public uint OfficeId { get; set; }

        [Required]
        [Display(Name = "Employee")]
        public uint EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("Deans")]
        public virtual Employees Employee { get; set; }
        [ForeignKey("OfficeId")]
        [InverseProperty("Deans")]
        public virtual Offices Office { get; set; }
        [InverseProperty("Dean")]
        public virtual ICollection<Offices> Offices { get; set; }
    }
}
