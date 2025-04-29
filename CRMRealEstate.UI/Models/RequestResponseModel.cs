using CRMRealEstate.DataAccess.Enums;

namespace CRMRealEstate.UI.Models
{
    public class RequestResponseModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int? AgentId { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? AnnouncementId { get; set; }
        public string? Title { get; set; }
        public string? CustomerMessage { get; set; }
        public string? AgentMessage { get; set; }
    }
}
