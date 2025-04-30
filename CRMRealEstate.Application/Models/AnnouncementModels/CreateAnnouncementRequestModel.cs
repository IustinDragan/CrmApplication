using CRMRealEstate.Application.Models.PropertyModels;
using CRMRealEstate.DataAccess.Entities;

namespace CRMRealEstate.Application.Models.AnnouncementModels
{
    public class CreateAnnouncementRequestModel
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public CreatePropertyRequestModel Property { get; set; }

        public Announcement ToAnnouncement(int agentId)
        {
            return new Announcement
            {
                Title = Title,
                StartDate = StartDate,
                EndDate = EndDate,
                UserId = agentId,
                Property = Property.ToProperty()
            };
        }
    }
}
