using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMRealEstate.DataAccess.Entities
{
    [Table("UsersAnnouncements")]
    public class UserAnnouncement
    {
        public Users User { get; set; }

        public int UserId { get; set; }

        public Announcement Announcement { get; set; }

        public int AnnouncementId { get; set; }
    }
}
