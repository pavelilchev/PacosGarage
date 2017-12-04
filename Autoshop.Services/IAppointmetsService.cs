namespace Autoshop.Services
{
    using System;
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
    }
}
