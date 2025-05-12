using CRMRealEstate.Application.Models.PropertyModels;
using CRMRealEstate.DataAccess.Entities;

namespace CRMRealEstate.Application.Models.AnnouncementModels
{
    public class AnnouncementResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public PropertyResponseModel? Property { get; set; }
        public int? AgentId { get; set; }
        public bool IsSold { get; set; }
        public bool IsRent { get; set; }

        public static AnnouncementResponseModel FromAnnouncement(Announcement announcement)
        {
            return new AnnouncementResponseModel
            {
                Id = announcement.Id,
                Title = announcement.Title,
                StartDate = announcement.StartDate,
                EndDate = announcement.EndDate,
                Property = announcement.Property != null
                    ? PropertyResponseModel.FromProperty(announcement.Property)
                    : null,
                AgentId = announcement.UserId,
                IsSold = announcement.IsSold,
                IsRent = announcement.IsRent
            };
        }
    }
}
