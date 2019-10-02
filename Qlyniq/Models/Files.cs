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

        [Key]
        public uint Id { get; set; }

        [Display(Name = "Patient")]
        [Required]
        public uint PatientId { get; set; }

        [Display(Name = "Created by")]
        [Required]
        public uint CreatorId { get; set; }


        [Display(Name = "Creation Date")]
        [Column(TypeName = "datetime")]
        public DateTime CreationDate { get; set; } = DateTime.Now;

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
