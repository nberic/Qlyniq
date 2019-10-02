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

        [Display(Name = "Time of acceptance")]
        [Column(TypeName = "datetime")]
        [Required]
        public DateTime AcceptedTime { get; set; }

        [Display(Name = "Time of sampling completion")]
        [Column(TypeName = "datetime")]
        public DateTime SampledTime { get; set; } = DateTime.Now;
        
        public float Glucose { get; set; }
        public float Urea { get; set; }
        public float Creatine { get; set; }
        public float Cholesterol { get; set; }

        [Column(TypeName = "tinyint(1)")]
        public bool? Helicobacter { get; set; } = false;

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
