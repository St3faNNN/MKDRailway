using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Carriage
    {
        [Key]
        public int CarriageId { get; set; }

        [Required]
        public int CarriageNumber { get; set; }

        [ForeignKey("Train")]
        public int TrainId { get; set; }
        public virtual Train Train { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
    }
}