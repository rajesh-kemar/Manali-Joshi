namespace TripManagementApp.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public string Model { get; set; }
        public bool IsAvailable { get; set; } = true;

        public ICollection<Trip>? Trips { get; set; }
    }
}
