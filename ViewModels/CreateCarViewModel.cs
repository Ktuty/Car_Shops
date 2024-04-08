using CarShops.Data.Enum;

namespace CarShops.ViewModels
{
    public class CreateCarViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ShortDesc { get; set; }
        public string? LongDesc { get; set; }
        public IFormFile Image { get; set; }
        public uint Price { get; set; }
        public bool Available { get; set; }
        //[ForeignKey("Category")]
        public CarCategory categoryID { get; set; }
       // public string AppUserId { get; set; }
    }
}
