﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CRMRealEstate.DataAccess.Entities
{
    public class Announcement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Property Property { get; set; }
        public int? UserId { get; set; }
        public Users? User { get; set; }
        public ICollection<UserAnnouncement> UserAnnouncements { get; set; }
    }
}
