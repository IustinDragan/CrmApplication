﻿namespace CRMRealEstate.Application.Models.AnnouncementModels
{
    public class ReadPropertyRequestModel
    {
        public string? OrderBy { get; set; }
        public int page { get; set; } = 1;
        public int PageCount { get; set; } = 9;
    }
}
