using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Seat
    {
        [Key]
        public int SeatId { get; set; }

        [Required]
        public int SeatNumber { get; set; }

        public bool IsAvailable { get; set; } = true;

        [ForeignKey("Carriage")]
        public int CarriageId { get; set; }
        public virtual Carriage Carriage { get; set; }
    }
}