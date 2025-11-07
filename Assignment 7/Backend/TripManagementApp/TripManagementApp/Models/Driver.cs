namespace TripManagementApp.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Trip>? Trips { get; set; }
    }
}
