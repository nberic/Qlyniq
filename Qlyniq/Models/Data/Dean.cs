using System;
using System.ComponentModel.DataAnnotations;

namespace Qlyniq.Models.Data
{
    public class Dean
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OfficeId { get; set; }

        public int EmployeeId { get; set; }
    }
}