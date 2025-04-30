using CRMRealEstate.Application.Models.AnnouncementModels;
using CRMRealEstate.Application.Services.Interfaces;
using CRMRealEstate.DataAccess.Repositories.Interfaces;

namespace CRMRealEstate.Application.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _announcementRepository;

        public AnnouncementService(IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository ?? throw new ArgumentException(nameof(announcementRepository));
        }

        public async Task<AnnouncementResponseModel> CreateAsync(CreateAnnouncementRequestModel requestModel, int agentId)
        {
            var announcement = requestModel.ToAnnouncement(agentId);
            var addedAnnouncement = await _announcementRepository.InsertAsync(announcement);

            return AnnouncementResponseModel.FromAnnouncement(addedAnnouncement);
        }

        public async Task<List<AnnouncementResponseModel>> ReadAllAsync(ReadAnnouncementRequestModel requestModel)
        {
            var announcements = await _announcementRepository.ReadAllAsync(requestModel.OrderBy, requestModel.SearchText,
                requestModel.price, requestModel.maxValue,
                requestModel.roomsNumber,
                requestModel.city,
                requestModel.page, requestModel.PageCount);


            return announcements.Select(AnnouncementResponseModel.FromAnnouncement).ToList();
        }

        public async Task<AnnouncementResponseModel> UpdateAsync(int id, UpdateAnnouncementRequestModel requestModel)
        {
            var announcementFromDb = await _announcementRepository.ReadByIdAsync(id);

            announcementFromDb.Title = requestModel.Title;
            announcementFromDb.Title = requestModel.Title;
            announcementFromDb.StartDate = requestModel.StartDate;
            announcementFromDb.EndDate = requestModel.EndDate;
            announcementFromDb.Property = requestModel.Property.ToProperty();

            await _announcementRepository.UpdateAsync(announcementFromDb);
            return AnnouncementResponseModel.FromAnnouncement(announcementFromDb);
        }

        public async Task<AnnouncementResponseModel> ReadByIdAsync(int id)
        {
            var announcement = await _announcementRepository.ReadByIdAsync(id);

            return AnnouncementResponseModel.FromAnnouncement(announcement);
        }

        public async Task<List<AnnouncementResponseModel?>> GetMyAnnouncementsAsync(int agentId)
        {
            var announcements = await _announcementRepository.GetAnnouncementsByAgentIdAsync(agentId);

            return announcements.Select(AnnouncementResponseModel.FromAnnouncement).ToList(); 
        }
        public async Task DeleteAsync(int id)
        {
            await _announcementRepository.DeleteAsync(id);
        }
    }
}
