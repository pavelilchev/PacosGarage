namespace Autoshop.Services
{
    using Autoshop.Services.Models.Appointments;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAppointmetsService
    {
        Task<bool> Add(
            string firstName, 
            string lastName, 
            string email, 
            string phone, 
            string vehicleInfo, 
            string reason,
            DateTime date,
            int? specialId,
            string userId);

        Task<IEnumerable<AppointmentListingServiceModel>> All(AppointmentStatus status);

        Task<AppointmentDetailsServiceMosel> Find(int id);

        Task<bool> Confirm(int id);
    }
}
