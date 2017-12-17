namespace Autoshop.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Autoshop.Data;
    using Autoshop.Models;
    using Autoshop.Services.Models.Specials;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SpecialsService : ISpecialsService
    {
        private readonly AutoshopDbContext db;

        public SpecialsService(AutoshopDbContext db)
        {
            this.db = db;
        }

        public async Task<int> Add(string title, string description, int daysValid)
        {
            var special = new Special
            {
                Title = title,
                Description = description,
                DaysValid = daysValid,
                CreatedOn = DateTime.UtcNow
            };

            await this.db.Specials.AddAsync(special);
            await this.db.SaveChangesAsync();

            return special.Id;
        }

        public async Task<IEnumerable<SpecialListingServiceModel>> All(bool notExpired = false)
        {
            var query = this.db.Specials.AsQueryable();
            if (notExpired)
            {
                query = query.Where(s => s.CreatedOn.AddDays(s.DaysValid) >= DateTime.UtcNow);
            }

            return await query
                     .OrderByDescending(s => s.CreatedOn)
                     .ProjectTo<SpecialListingServiceModel>()
                     .ToListAsync();
        }

        public async Task<SpecialListingServiceModel> Find(int id)
        {
            return await this.db.Specials
                .Where(s => s.Id == id)
                .ProjectTo<SpecialListingServiceModel>()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Update(int id, string title, string description, int daysValid)
        {
            var special = await this.db.Specials.FindAsync(id);
            if (special == null)
            {
                return false;
            }

            special.Title = title;
            special.Description = description;
            special.DaysValid = daysValid;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Remove(int id)
        {
            var special = await this.db.Specials.FindAsync(id);
            if (special == null)
            {
                return false;
            }

            this.db.Specials.Remove(special);
            await this.db.SaveChangesAsync();

            return true;
        }
    }
}
