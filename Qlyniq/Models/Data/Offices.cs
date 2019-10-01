using System;
using System.ComponentModel.DataAnnotations;

namespace Qlyniq.Models.Data
{
    public class Office
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int DeanId { get; set; }

        [Required]
        public decimal Budget { get; set; } = 0.0M;

    }
}