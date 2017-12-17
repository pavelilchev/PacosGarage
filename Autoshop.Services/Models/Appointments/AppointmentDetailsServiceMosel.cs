namespace Autoshop.Services.Models.Appointments
{
    using System;

    public class AppointmentDetailsServiceMosel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
       
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string VehicleInformation { get; set; }

        public string Reason { get; set; }

        public DateTime Date { get; set; }

        public bool IsConfirmed { get; set; }
    }
}
