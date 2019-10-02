using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qlyniq.Models
{
    [Table("files")]
    public partial class Files
    {
        public Files()
        {
            Examinations = new HashSet<Examinations>();
        }

        public uint Id { get; set; }
        public uint PatientId { get; set; }
        public uint CreatorId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreationDate { get; set; }
        [Required]
        [Column(TypeName = "varchar(1000)")]
        public string Note { get; set; }

        [ForeignKey("CreatorId")]
        [InverseProperty("Files")]
        public virtual Employees Creator { get; set; }
        [ForeignKey("PatientId")]
        [InverseProperty("Files")]
        public virtual Patients Patient { get; set; }
        [InverseProperty("File")]
        public virtual ICollection<Examinations> Examinations { get; set; }
    }
}
