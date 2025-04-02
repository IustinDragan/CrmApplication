using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMRealEstate.Application.Models.AnnouncementModels
{
    public class ReadPropertyRequestModel
    {
        public string? OrderBy { get; set; }

        public int page { get; set; } = 1;

        public int PageCount { get; set; } = 9;
    }
}
