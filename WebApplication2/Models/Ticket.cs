using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("Train")]
        public int TrainId { get; set; }
        public virtual Train Train { get; set; }

        [ForeignKey("Seat")]
        public int SeatId { get; set; }
        [Required]
        public virtual Seat Seat { get; set; }

        public bool IsConfirmed { get; set; } = false;

        [Required]
        public DateTime PurchaseDate { get; set; }
    }
}