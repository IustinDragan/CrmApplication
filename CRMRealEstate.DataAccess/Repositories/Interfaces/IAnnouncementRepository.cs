﻿using CRMRealEstate.DataAccess.Entities;

namespace CRMRealEstate.DataAccess.Repositories.Interfaces
{
    public interface IAnnouncementRepository
    {
        public Task<Announcement> InsertAsync(Announcement announcement);
        public Task<Announcement> UpdateAsync(Announcement announcement);
        Task<List<Announcement>> ReadAllAsync(string? orderBy, string? searchText, double? price, double? maxValue,
            int? roomsNumber, string? city,
            int page, int pageCount);
        public Task<Announcement> ReadByIdAsync(int id);
        Task<List<Announcement?>> GetAnnouncementsByAgentIdAsync(int agentId);
        Task<Announcement> GetByPropertyIdAsync(int propertyId);

        public Task DeleteAsync(int id);
    }
}
