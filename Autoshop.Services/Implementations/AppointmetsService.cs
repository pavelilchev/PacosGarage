namespace Autoshop.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Autoshop.Data;
    using Autoshop.Models;
    using Autoshop.Services.Models.Appointments;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AppointmetsService : IAppointmetsService
    {
        private readonly AutoshopDbContext db;
        private readonly IEmailSender emailSender;
        private readonly IConfiguration configuration;

        public AppointmetsService(AutoshopDbContext db, IEmailSender emailSender, IConfiguration configuration)
        {
            this.db = db;
            this.emailSender = emailSender;
            this.configuration = configuration;
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

            await this.db.SaveChangesAsync();

            await this.SendLeadNotification(appointment);

            return true;
        }

        public async Task<IEnumerable<AppointmentListingServiceModel>> All(AppointmentStatus status)
        {
            var appointments = this.db.Appointments.AsQueryable();
            if (status == AppointmentStatus.Confirmed)
            {
                appointments = appointments.Where(a => a.IsConfirmed);
            }
            else if (status == AppointmentStatus.Unconfirmed)
            {
                appointments = appointments.Where(a => !a.IsConfirmed);
            }

            return await appointments
                .OrderByDescending(a => a.Id)
                .ProjectTo<AppointmentListingServiceModel>()
                .ToListAsync();
        }

        public async Task<IEnumerable<AppointmentListingServiceModel>> ByUser(string id)
        {
            return await this.db.Appointments
                .Where(a => a.UserId == id)
                .OrderByDescending(a => a.Id)
                .ProjectTo<AppointmentListingServiceModel>()
                .ToListAsync();
        }

        public async Task<bool> Confirm(int id)
        {
            var appointment = await this.db.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return false;
            }

            appointment.IsConfirmed = true;
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<AppointmentDetailsServiceMosel> Find(int id)
        {
            return await this.db
                .Appointments
                .Where(a => a.Id == id)
                .ProjectTo<AppointmentDetailsServiceMosel>()
                .FirstOrDefaultAsync();
        }

        private async Task SendLeadNotification(Appointment appointment)
        {
            const string emailSubject = "Appointment Form Submited";
            var leadEmail = this.configuration["WebSiteSettings:LeadEmails"];
            var domain = this.configuration["WebSiteSettings:Domain"];
            var message = $@"<p>{appointment.FirstName} {appointment.LastName} submit appointment</p>
                             <p>Requested date: {appointment.Date.ToShortDateString()}</p>
                             <a href='{domain}/administration/appointments/details/{appointment.Id}'>See more details</a>";

            await emailSender.SendEmailAsync(leadEmail, emailSubject, message);
        }
    }
}
