using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Web;

namespace WebApplication2.Models
{
    public class Train
    {
        [Key]
        public int TrainId { get; set; }

        [Required]
        [StringLength(50)]
        public string LineNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string StationName { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan DepartureTime { get; set; }

        [Required]
        public TimeSpan ArrivalTime { get; set; }

        [Required]
        [Range(1, 500, ErrorMessage = "Seats must be between 1 and 500")]
        [Display(Name = "Number of Seats")]
        public int NumberOfSeats { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "Carriages must be between 1 and 20")]
        [Display(Name = "Number of Carriages")]
        public int NumberOfCarriages { get; set; }

        public virtual ICollection<Carriage> Carriages { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}