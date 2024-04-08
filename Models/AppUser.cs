using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CarShops.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Car> Cars { get; set; }
    }
}
