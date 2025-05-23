﻿namespace CRMRealEstate.UI.Models
{
    public class AnnouncementResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? AgentId { get; set; }
        public PropertyResponseModel Property { get; set; }
        public bool IsSold { get; set; }
        public bool IsRent { get; set; }
    }
}