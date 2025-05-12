using CRMRealEstate.DataAccess.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMRealEstate.DataAccess.Entities
{
    public class Request
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int? AgentId { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Users Customer { get; set; }
        [ForeignKey(nameof(AgentId))]
        public Users Agent { get; set; }
        public int? AnnouncementId { get; set; }
        public string? Title { get; set; }
        public string? CustomerMessage { get; set; }
        public string? AgentMessage { get; set; }

        //public string fileName {  get; set; } --cred ca va trebui adaugat
        [ForeignKey(nameof(AnnouncementId))]
        public Announcement? Announcement { get; set; }

    }
}
