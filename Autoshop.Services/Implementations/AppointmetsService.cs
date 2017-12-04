namespace Autoshop.Services.Implementations
{
    using Autoshop.Data;
    using Autoshop.Models;
    using System;
    using System.Threading.Tasks;

    public class AppointmetsService : IAppointmetsService
    {
        private readonly AutoshopDbContext db;

        public AppointmetsService(AutoshopDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> Add(string firstName, string lastName, string email, string phone, string vehicleInfo, string reason, DateTime date, int? specialId, string userId)
        {
            var appointment = new Appointment
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                VehicleInformation = vehicleInfo,
                Reason = reason,
                Date = date,
                SpecialId = specialId,
                UserId = userId,
            };

            await this.db.Appointments.AddAsync(appointment);

            var result =  await this.db.SaveChangesAsync();

            return result > 0;
        }
    }
}
