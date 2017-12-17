namespace Autoshop.Web.Areas.Administration.Models.Appointments
{
    using Autoshop.Services.Models.Appointments;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class AppointmentsListingViewModel
    {
        public IEnumerable<AppointmentListingServiceModel> Appointments { get; set; }

        [DisplayName("Appointment Status")]
        public AppointmentStatus AppointmentStatus { get; set; }
    }
}
