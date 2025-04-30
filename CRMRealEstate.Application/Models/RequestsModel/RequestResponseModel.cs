using CRMRealEstate.DataAccess.Entities;
using CRMRealEstate.DataAccess.Enums;

namespace CRMRealEstate.Application.Models.RequestsModel
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

        public static RequestResponseModel FromUser(Request requests)
        {
            return new RequestResponseModel
            {
                Id = requests.Id,
                CustomerId = requests.CustomerId,
                AgentId = requests.AgentId,
                Status = requests.Status,
                CreatedAt = requests.CreatedAt,
                UpdatedAt = requests.UpdatedAt,
                AnnouncementId= requests.AnnouncementId,
                Title = requests.Title,
                CustomerMessage = requests.CustomerMessage,
                AgentMessage = requests.AgentMessage

                //recomandare Alex: title, customerMessage = required + idAnnouncemet
            };
        }
    }
}

