using CRMRealEstate.DataAccess.Entities;

namespace CRMRealEstate.Application.Models.AdressModels
{
    public class CreateAdressRequestModel
    {
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int AppartamentNumber { get; set; }


        public Adress toAdress()
        {
            return new Adress
            {
                Street = Street,
                StreetNumber = StreetNumber,
                Country = Country,
                City = City,
                AppartamentNumber = AppartamentNumber
            };
        }
    }
}
