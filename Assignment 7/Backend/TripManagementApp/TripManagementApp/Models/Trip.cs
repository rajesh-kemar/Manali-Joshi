//using System.ComponentModel.DataAnnotations;

//namespace TripManagementApp.Models
//{
//    public class Trip
//    {
//        [Key]
//        public int Id { get; set; }

//        [Required]
//        public int DriverId { get; set; }

//        public Driver Driver { get; set; }      // Include Driver info, no cycle

//        [Required]
//        public int VehicleId { get; set; }

//        public Vehicle Vehicle { get; set; }    // Include Vehicle info, no cycle

//        [Required]
//        public string Source { get; set; }

//        [Required]
//        public string Destination { get; set; }

//        [Required]
//        public DateTime StartTime { get; set; }

//        public DateTime? EndTime { get; set; }

//        public string Status { get; set; } = "Active"; // Active or Completed
//    }
//}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TripManagementApp.Models
{
    [Table("Trips")]
    public class Trip
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }

        [ForeignKey("Driver")]
        public int DriverId { get; set; }

        public string Source { get; set; }

        public string Destination { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string Status { get; set; } = "InProgress";

        [JsonIgnore]
        public Vehicle? Vehicle { get; set; }
        public Driver? Driver { get; set; }
    }
}
