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

        [Required]
        [StringLength(4, MinimumLength = 4)]
        [RegularExpression(@"^[A-Z]{1}[0-9]{3}$", ErrorMessage = "Please input a capital letter A-Z followed by three digits 0-9")]
        [Column(TypeName = "varchar(4)")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Patient")]
        public uint PatientId { get; set; }

        [Required]
        [Display(Name = "Creaded by")]
        public uint CreatorId { get; set; }

        [Required]
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
