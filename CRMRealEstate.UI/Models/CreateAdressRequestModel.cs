namespace CRMRealEstate.UI.Models
{
    public class CreateAdressRequestModel
    {
        public string Street { get; set; }
        public int? StreetNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int? AppartamentNumber { get; set; }

        public override string ToString()
        {
            return
                $"Strada: {Street}, Numarul {StreetNumber}, Tara: {Country}, Oras: {City}";
        }
    }
}
