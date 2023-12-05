using Microsoft.AspNetCore.Identity;
using StoreLibrary.Models;

namespace OnlineStore.Areas.Identity.Data
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
    }
}
