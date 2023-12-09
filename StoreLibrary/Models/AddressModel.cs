using System.ComponentModel.DataAnnotations;

namespace StoreLibrary.Models
{
    public class AddressModel
    {
        [Key]
        public int Id { get; set; }
        public string CartId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string HouseNumber { get; set; }
    }
}
