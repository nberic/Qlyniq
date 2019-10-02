using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qlyniq.Models
{
    [Table("examinations")]
    public partial class Examinations
    {
        [Key]
        public uint Id { get; set; }

        [Display(Name = "Patient")]
        [Required]
        public uint PatientId { get; set; }

        [Display(Name = "Doctor")]
        [Required]
        public uint DoctorId { get; set; }

        [Display(Name = "Starting Time")]
        [Column(TypeName = "datetime")]
        public DateTime StartingTime { get; set; } = DateTime.Now;

        [Display(Name = "File Number")]
        [Required]
        public uint FileId { get; set; }

        [Display(Name = "Diagnosis")]
        [Required]
        public uint? DiagnosisId { get; set; }

        [Required]
        [Column(TypeName = "varchar(500)")]
        public string Therapy { get; set; }

        [Display(Name = "Is Emergency")]
        [Column(TypeName = "tinyint(1)")]
        public bool? IsEmergency { get; set; } = false;

        [Display(Name = "Lab Report")]
        public uint? LabReportId { get; set; }

        [ForeignKey("DiagnosisId")]
        [InverseProperty("Examinations")]
        public virtual Diagnosis Diagnosis { get; set; }
        [ForeignKey("DoctorId")]
        [InverseProperty("Examinations")]
        public virtual Employees Doctor { get; set; }
        [ForeignKey("FileId")]
        [InverseProperty("Examinations")]
        public virtual Files File { get; set; }
        [ForeignKey("LabReportId")]
        [InverseProperty("Examinations")]
        public virtual Labreports LabReport { get; set; }
        [ForeignKey("PatientId")]
        [InverseProperty("Examinations")]
        public virtual Patients Patient { get; set; }
    }
}
