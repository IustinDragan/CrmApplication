using CRMRealEstate.Application.Models.AdressModels;
using CRMRealEstate.DataAccess.Entities;
using CRMRealEstate.DataAccess.Enums;

namespace CRMRealEstate.Application.Models.PropertyModels
{
    //public class CreatePropertyRequestModel
    //{
    //    public int RoomsNumber { get; set; }
    //    public int BathroomsNumber { get; set; }
    //    public double LandArea { get; set; }
    //    public double HouseUsableArea { get; set; }
    //    public double HouseTotalArea { get; set; }
    //    public int ConstructionYear { get; set; }
    //    public int FloorsTotalNumber { get; set; }
    //    public int ApartamentFloor { get; set; }
    //    public bool Elevator { get; set; }
    //    public string Utilities { get; set; }
    //    public string Details { get; set; }
    //    public double Price { get; set; }
    //    public PropertyType PropertyType { get; set; }
    //    public CreateAdressRequestModel Adress { get; set; }


    //    public Property ToProperty()
    //    {
    //        return new Property
    //        {
    //            RoomsNumber = RoomsNumber,
    //            BathroomsNumber = BathroomsNumber,
    //            Details = Details,
    //            Price = Price,
    //            PropertyType = PropertyType,
    //            Adress = Adress.toAdress()
    //        };
    //    }
    //}

    public class CreatePropertyRequestModel
    {
        public int RoomsNumber { get; set; }
        public int BathroomsNumber { get; set; }
        public double? LandArea { get; set; }
        public double? HouseUsableArea { get; set; }
        public double? HouseTotalArea { get; set; }
        public int ConstructionYear { get; set; }
        public int? FloorsTotalNumber { get; set; }
        public int? ApartamentFloor { get; set; }
        public bool? Elevator { get; set; }
        public string? Utilities { get; set; }
        public string? Details { get; set; }
        public double Price { get; set; }
        public PropertyType PropertyType { get; set; }
        public CreateAdressRequestModel Adress { get; set; }

        public Property ToProperty()
        {
            return new Property
            {
                RoomsNumber = RoomsNumber,
                BathroomsNumber = BathroomsNumber,
                ConstructionYear = ConstructionYear,
                Area = LandArea > 0 ? LandArea : HouseTotalArea, // logica adaptabilă
                Details = Details,
                Price = Price,
                PropertyType = PropertyType,
                Utilities = Utilities,
                isAvailable = true,
                Adress = Adress.toAdress(),
                FloorsTotalNumber = FloorsTotalNumber,
                ApartamentFloor = ApartamentFloor,
            };
        }
    }
}
