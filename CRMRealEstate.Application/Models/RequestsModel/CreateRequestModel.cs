using CRMRealEstate.DataAccess.Entities;
using CRMRealEstate.DataAccess.Enums;

namespace CRMRealEstate.Application.Models.RequestsModel
{
    public class CreateRequestModel
    {
        public int CustomerId { get; set; }
        public int? AgentId { get; set; }
        public RequestStatus Status { get; set; }
        public int? AnnouncementID {  get; set; }
        public string? Title {  get; set; }
        public string? CustomerMessage { get; set; }
        public string? AgentMessage { get; set; }

        public Request ToRequest()
        {
            return new Request
            {
                CustomerId = CustomerId,
                AgentId = AgentId,
                Status = RequestStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                AnnouncementId = AnnouncementID,
                Title = Title,
                CustomerMessage = CustomerMessage,
                AgentMessage = AgentMessage
            };
        }
    }
}