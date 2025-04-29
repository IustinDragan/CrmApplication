using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMRealEstate.DataAccess.Entities
{
    [Table("UsersAnnouncements")]
    public class UserAnnouncement
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public Users User { get; set; }

        public int AnnouncementId { get; set; }

        public Announcement Announcement { get; set; }
        
    }
}
