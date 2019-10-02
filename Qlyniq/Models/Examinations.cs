using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qlyniq.Models
{
    [Table("examinations")]
    public partial class Examinations
    {
        public uint Id { get; set; }
        public uint PatientId { get; set; }
        public uint DoctorId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartingTime { get; set; }
        public uint FileId { get; set; }
        public uint? DiagnosisId { get; set; }
        [Required]
        [Column(TypeName = "varchar(500)")]
        public string Therapy { get; set; }
        [Column(TypeName = "tinyint(1)")]
        public sbyte IsEmergency { get; set; }
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
