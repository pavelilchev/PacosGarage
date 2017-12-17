namespace Autoshop.Services
{
    using Autoshop.Services.Models.Specials;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISpecialsService
    {
        Task<int> Add(string title, string description, int daysValid);

        Task<IEnumerable<SpecialListingServiceModel>> All(bool notExpired = false);

        Task<SpecialListingServiceModel> Find(int id);

        Task<bool> Update(int id, string title, string description, int daysValid);

        Task<bool> Remove(int id);
    }
}
