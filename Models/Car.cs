using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CarShops.Data.Enum;

namespace CarShops.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public string? ShortDesc { get; set; }
        public string? LongDesc { get; set; }
        public string? Image { get; set; }
        public uint Price { get; set; }
        public bool Available { get; set; }
        //[ForeignKey("Category")]
        public CarCategory categoryID { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
