namespace Autoshop.Services.Models.Appointments
{
    public class AppointmentListingServiceModel
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Phone { get; set; }

        public bool IsConfirmed { get; set; }
    }
}
