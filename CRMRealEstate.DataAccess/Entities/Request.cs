using CRMRealEstate.DataAccess.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMRealEstate.DataAccess.Entities
{
    public class Request
    {
        public int Id { get; set; }
        public int CustomerId {  get; set; }
        public int AgentId {  get; set; }
        public RequestStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Users Customer { get; set; }
        [ForeignKey(nameof(AgentId))]
        public Users Agent { get; set; }
        public int AnnouncementId {  get; set; }
        public string? Title { get; set; }
        public string? CustomerMessage {  get; set; }
        public string? AgentMessage { get; set; }

        //public string fileName {  get; set; }
        [ForeignKey(nameof(AnnouncementId))]
        public Announcement Announcement { get; set; }

        //Titlu
        //Customer message
        //Agent? message
        //string fileName



        //public int AnnouncementId { get;set; }
        //public Announcement Announcement { get; set; }
    }
}
