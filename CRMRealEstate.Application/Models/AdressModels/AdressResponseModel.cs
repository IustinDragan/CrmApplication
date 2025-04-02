using CRMRealEstate.DataAccess.Entities;

namespace CRMRealEstate.Application.Models.AdressModels
{
    public class AdressResponseModel
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int AppartamentNumber { get; set; }

        public static AdressResponseModel FromAdress(Adress adress)
        {
            return new AdressResponseModel
            {
                Id = adress.Id,
                Street = adress.Street,
                StreetNumber = adress.StreetNumber,
                Country = adress.Country,
                City = adress.City,
                AppartamentNumber = adress.AppartamentNumber
            };
        }
    }
}
