using HotelListing.Data;
using HotelListing.Models.Model;

namespace HotelListing.Models.Country
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public List<HotelDto> Hotels { get; set; }

    }
}
