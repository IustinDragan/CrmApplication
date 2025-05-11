using CRMRealEstate.DataAccess.Entities;
using CRMRealEstate.DataAccess.Repositories.Interfaces;
using CRMRealEstate.DataAccess.Scripts;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CRMRealEstate.DataAccess.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly DatabaseContext _databaseContext;

        public AnnouncementRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Announcement> InsertAsync(Announcement announcement)
        {
            var addedEntity = await _databaseContext.Announcements.AddAsync(announcement);

            await _databaseContext.SaveChangesAsync();

            return addedEntity.Entity;
        }

        public async Task<Announcement> UpdateAsync(Announcement announcement)
        {
            var updatedEntity = _databaseContext
                .Announcements
                .Update(announcement);

            await _databaseContext.SaveChangesAsync();

            return updatedEntity.Entity;
        }

        public async Task<List<Announcement>> ReadAllAsync(string? orderBy, string? searchText, double? price,
            double? maxValue, int? roomsNumber, string? city,
            int page, int pageCount)
        {
            var orderByConfiguration = new Dictionary<string, Expression<Func<Announcement, object>>>
            {
                { "title", x => x.Title },
                { "startdate", x => x.StartDate },
                { "enddate", x => x.EndDate },
                { "roomsnumber", x => x.Property.RoomsNumber },
                { "bathroomsnumber", x => x.Property.BathroomsNumber },
                { "price", x => x.Property.Price },
                { "description", x => x.Property.Details },
                { "propertytype", x => x.Property.PropertyType },
                { "utilities", x => x.Property.Utilities },
                { "street", x => x.Property.Adress.Street },
                { "district", x => x.Property.Adress.Country },
                { "city", x => x.Property.Adress.City },
                {"streetNumber", x => x.Property.Adress.StreetNumber }
            };

            var query = _databaseContext.Announcements.Include(a => a.Property).ThenInclude(p => p.Adress).AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
                query = query.Where(p => p.Title.Contains(searchText) || p.Property.Details.Contains(searchText));

            if (roomsNumber.HasValue)
                query = query.Where(p => p.Property.RoomsNumber == roomsNumber);

            if (!string.IsNullOrEmpty(city))
                query = query.Where(p => p.Property.Adress.City.Contains(city));

            if (price.HasValue && maxValue.HasValue)
            {
                query = query.Where(p => p.Property.Price >= price && p.Property.Price <= maxValue);
            }
            else
            {
                if (price.HasValue)
                    query = query.Where(p => p.Property.Price >= price);
                else if (maxValue.HasValue) query = query.Where(p => p.Property.Price <= maxValue);
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                if (!orderByConfiguration.ContainsKey(orderBy.ToLower()))
                    throw new Exception($"Nu se poate ordona dupa {orderBy}");
                query = query.OrderByDescending(orderByConfiguration[orderBy]);
            }
            query = query.Where(a => !a.IsSold && !a.IsRent);

            query = query.Skip((page - 1) * pageCount).Take(pageCount);

            return await query.ToListAsync();
        }

        public async Task<Announcement?> ReadByIdAsync(int id)
        {
            return await _databaseContext
                .Announcements
                .Where(x => x.Id == id)
                .Include(a => a.Property)
                .ThenInclude(p => p.Adress)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Announcement>> GetAnnouncementsByAgentIdAsync(int agentId)
        {
            return await _databaseContext
                .Announcements
                .Where(x => x.UserId == agentId)
                .Include(a => a.Property)
                .ThenInclude(p => p.Adress)
                .ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var announcement = await ReadByIdAsync(id);

            _databaseContext.Announcements.Remove(announcement);

            await _databaseContext.SaveChangesAsync();
        }

        public async Task<Announcement> GetByPropertyIdAsync(int propertyId)
        {
            return await _databaseContext.Announcements.FirstOrDefaultAsync(a => a.Property.Id == propertyId);
        }
    }
}
