using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qlyniq.Models
{
    [Table("appointments")]
    public partial class Appointments
    {
        [Key]
        public uint Id { get; set; }

        [Display(Name = "First Name")]
        [Column(TypeName = "varchar(255)")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Unknown")]
        public string PatientFirstName { get; set; }

        [Display(Name = "Last Name")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Unknown")]
        [Column(TypeName = "varchar(255)")]
        public string PatientLastName { get; set; }

        public uint? PatientId { get; set; }

        public uint DoctorId { get; set; }

        [Required]
        [Display(Name = "Starting Time")]
        [Column(TypeName = "datetime")]
        public DateTime StartingTime { get; set; }

        [ForeignKey("DoctorId")]
        [InverseProperty("Appointments")]
        public virtual Employees Doctor { get; set; }
        [ForeignKey("PatientId")]
        [InverseProperty("Appointments")]
        public virtual Patients Patient { get; set; }
    }
}
