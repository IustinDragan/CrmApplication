using CRMRealEstate.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //Task<List<Announcement>> SearchAnnouncements(string searchText);

        public Task DeleteAsync(int id);
    }
}
