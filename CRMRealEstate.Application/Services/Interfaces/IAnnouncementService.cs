using CRMRealEstate.Application.Models.AnnouncementModels;

namespace CRMRealEstate.Application.Services.Interfaces
{
    public interface IAnnouncementService
    {
        Task<AnnouncementResponseModel> CreateAsync(
            CreateAnnouncementRequestModel requestModel);

        Task<AnnouncementResponseModel> UpdateAsync(int id,
            UpdateAnnouncementRequestModel requestModel);

        Task<List<AnnouncementResponseModel>> ReadAllAsync(
            ReadAnnouncementRequestModel requestModel);

        Task<AnnouncementResponseModel> ReadByIdAsync(int id);

        //Task<List<Announcement>> SearchAsync(string searchText);

        Task DeleteAsync(int id);
    }
}
