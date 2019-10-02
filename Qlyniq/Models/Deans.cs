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

        public uint Id { get; set; }
        public uint OfficeId { get; set; }
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
