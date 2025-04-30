using CRMRealEstate.Application.Models.AnnouncementModels;

namespace CRMRealEstate.Application.Services.Interfaces
{
    public interface IAnnouncementService
    {
        Task<AnnouncementResponseModel> CreateAsync(
            CreateAnnouncementRequestModel requestModel, int agentId);

        Task<AnnouncementResponseModel> UpdateAsync(int id,
            UpdateAnnouncementRequestModel requestModel);

        Task<List<AnnouncementResponseModel>> ReadAllAsync(
            ReadAnnouncementRequestModel requestModel);

        Task<AnnouncementResponseModel> ReadByIdAsync(int id);

        Task<List<AnnouncementResponseModel?>> GetMyAnnouncementsAsync(int agentId);

        Task DeleteAsync(int id);
    }
}
