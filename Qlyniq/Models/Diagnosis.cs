using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qlyniq.Models
{
    [Table("diagnosis")]
    public partial class Diagnosis
    {
        public Diagnosis()
        {
            Examinations = new HashSet<Examinations>();
        }

        public uint Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(4)")]
        public string Code { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string MedicalTerm { get; set; }

        [InverseProperty("Diagnosis")]
        public virtual ICollection<Examinations> Examinations { get; set; }
    }
}
