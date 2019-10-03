using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qlyniq.Models
{
    [Table("labreports")]
    public partial class Labreports
    {
        public Labreports()
        {
            Examinations = new HashSet<Examinations>();
        }

        [Key]
        public uint Id { get; set; }

        [Required]
        [Display(Name = "Received by")]
        public uint RecipientId { get; set; }

        [Required]
        [Display(Name = "Patient")]
        public uint PatientId { get; set; }

        [Required]
        [Display(Name = "Time of Acceptance")]
        [Column(TypeName = "datetime")]
        public DateTime AcceptedTime { get; set; }

        [Required]
        [Display(Name = "Time of Sampling Completion")]
        [Column(TypeName = "datetime")]
        public DateTime SampledTime { get; set; } = DateTime.Now;

        [Required]
        public float Glucose { get; set; }

        [Required]
        public float Urea { get; set; }

        [Required]
        public float Creatine { get; set; }

        [Required]
        public float Cholesterol { get; set; }

        [Required]
        [Column(TypeName = "tinyint(1)")]
        public bool Helicobacter { get; set; } = false;


        [ForeignKey("PatientId")]
        [InverseProperty("Labreports")]
        public virtual Patients Patient { get; set; }
        [ForeignKey("RecipientId")]
        [InverseProperty("Labreports")]
        public virtual Employees Recipient { get; set; }
        [InverseProperty("LabReport")]
        public virtual ICollection<Examinations> Examinations { get; set; }
    }
}
