using CRMRealEstate.DataAccess.Entities;
using CRMRealEstate.DataAccess.Enums;

namespace CRMRealEstate.Application.Models.PropertyModels
{
    public class PropertyResponseModel
    {
        public int Id { get; set; }
        public int RoomsNumber { get; set; }
        public int BathroomsNumber { get; set; }
        public double LandArea { get; set; }
        public double HouseUsableArea { get; set; }
        public double HouseTotalArea { get; set; }
        public int ConstructionYear { get; set; }
        public int FloorsTotalNumber { get; set; }
        public int ApartamentFloor { get; set; }
        public bool Elevator { get; set; }
        public string Details { get; set; }
        public double Price { get; set; }
        public PropertyType PropertyType { get; set; }
        public int? AnnouncementId { get; set; }
        public string? Adress { get; set; }

        public static PropertyResponseModel FromProperty(Property property)
        {
            return new PropertyResponseModel
            {
                Id = property.Id,
                RoomsNumber = property.RoomsNumber,
                BathroomsNumber = property.BathroomsNumber,
                LandArea = property.Area,
                ConstructionYear = property.ConstructionYear,
                Details = property.Details,
                Price = property.Price,
                PropertyType = property.PropertyType,
                AnnouncementId = property.AnnouncementId,
                Adress = property.Adress?.ToString()
            };
        }
    }
}
