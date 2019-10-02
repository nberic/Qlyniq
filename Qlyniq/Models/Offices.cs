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
            Deans = new HashSet<Deans>();
            Employees = new HashSet<Employees>();
        }

        public uint Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
        public uint DeanId { get; set; }
        [Column(TypeName = "decimal(10,0)")]
        public decimal Budget { get; set; }

        [ForeignKey("DeanId")]
        [InverseProperty("Offices")]
        public virtual Deans Dean { get; set; }
        [InverseProperty("Office")]
        public virtual ICollection<Deans> Deans { get; set; }
        [InverseProperty("Office")]
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
