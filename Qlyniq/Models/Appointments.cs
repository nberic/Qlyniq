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
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Unknown")]
        [Column(TypeName = "varchar(255)")]
        public string PatientFirstName { get; set; } = null;

        [Display(Name = "Last Name")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Unknown")]
        [Column(TypeName = "varchar(255)")]
        public string PatientLastName { get; set; }
        
        [Display(Name = "Patient")]
        public uint? PatientId { get; set; }

        [Display(Name = "Appointed Doctor")]
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
