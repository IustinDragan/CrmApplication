﻿using CRMRealEstate.DataAccess.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMRealEstate.DataAccess.Entities
{
    public class Property
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int RoomsNumber { get; set; }
        public int BathroomsNumber { get; set; }

        public double? Area { get; set; }

        public int ConstructionYear { get; set; }
        public string? Details { get; set; }

        public double Price { get; set; }

        public PropertyType PropertyType { get; set; }

        public string? Utilities { get; set; }
        public int? FloorsTotalNumber { get; set; }
        public int? ApartamentFloor { get; set; }

        public bool isAvailable { get; set; }

        [ForeignKey("AnnouncementId")]
        public int? AnnouncementId { get; set; }

        public Announcement? Announcement { get; set; }

        public Adress Adress { get; set; }
    }
}
