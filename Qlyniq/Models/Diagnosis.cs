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

        [Key]
        public uint Id { get; set; }

        [NotMapped]
        public string Name => $"{ Code }: { MedicalTerm }";

        [StringLength(4, MinimumLength = 4)]
        [RegularExpression(@"^[A-Z]{1}[0-9]{3}$", ErrorMessage = "Please input a capital letter A-Z followed by three digits 0-9")]
        [Required]
        [Column(TypeName = "varchar(4)")]
        public string Code { get; set; }

        [Display(Name = "Medical Term")]
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string MedicalTerm { get; set; }


        [InverseProperty("Diagnosis")]
        public virtual ICollection<Examinations> Examinations { get; set; }
    }
}
