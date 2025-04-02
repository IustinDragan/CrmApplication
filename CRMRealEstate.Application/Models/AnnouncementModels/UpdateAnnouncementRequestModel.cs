using CRMRealEstate.Application.Models.PropertyModels;
using CRMRealEstate.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMRealEstate.Application.Models.AnnouncementModels
{
    public class UpdateAnnouncementRequestModel
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public CreatePropertyRequestModel Property { get; set; }

        public Announcement ToAnnouncement()
        {
            return new Announcement
            {
                Title = Title,
                StartDate = StartDate,
                EndDate = EndDate,
                Property = Property.ToProperty()
            };
        }
    }
}
