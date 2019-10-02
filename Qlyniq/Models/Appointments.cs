using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qlyniq.Models
{
    [Table("appointments")]
    public partial class Appointments
    {
        public uint Id { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string PatientFirstName { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string PatientLastName { get; set; }
        public uint? PatientId { get; set; }
        public uint DoctorId { get; set; }
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
