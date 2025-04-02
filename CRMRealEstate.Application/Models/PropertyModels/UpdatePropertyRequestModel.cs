using CRMRealEstate.Application.Models.AdressModels;
using CRMRealEstate.DataAccess.Entities;
using CRMRealEstate.DataAccess.Enums;

namespace CRMRealEstate.Application.Models.PropertyModels
{
    public class UpdatePropertyRequestModel
    {
        public int RoomsNumber { get; set; }
        public int BathroomsNumber { get; set; }
        public double Area { get; set; }
        public int ConstructionYear { get; set; }
        public string Details { get; set; }
        public double Price { get; set; }
        public PropertyType PropertyType { get; set; }
        public CreateAdressRequestModel Adress { get; set; }

        public Property ToPropertyUpdate()
        {
            return new Property
            {
                RoomsNumber = RoomsNumber,
                BathroomsNumber = BathroomsNumber,
                Area = Area,
                ConstructionYear = ConstructionYear,
                Details = Details,
                Price = Price,
                PropertyType = PropertyType,
                Adress = Adress.toAdress()
            };
        }
    }
}
