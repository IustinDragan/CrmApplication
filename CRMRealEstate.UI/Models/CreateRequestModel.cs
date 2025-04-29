using CRMRealEstate.DataAccess.Enums;

namespace CRMRealEstate.UI.Models
{
    public class CreateRequestModel
    {
        public int CustomerId { get; set; }
        public int? AgentId { get; set; }
        public RequestStatus Status { get; set; }
        public int? AnnouncementID { get; set; }
        public string? Title { get; set; }
        public string? CustomerMessage { get; set; }
        public string? AgentMessage { get; set; }
    } 
}

