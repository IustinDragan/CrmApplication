using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CRMRealEstate.DataAccess.Entities
{
    public class Adress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public string? Country { get; set; }
        public string City { get; set; }
        public int? AppartamentNumber { get; set; }

        [ForeignKey("PropertyId")]
        public Property? Property { get; set; }
        public int? PropertyId { get; set; }

        public override string ToString()
        {
            return
                $"Strada: {Street}, Numarul {StreetNumber}, Oras: {City}, Tara: {Country}";
        }
    }
}
