using HotelListing.Models.Country;
using HotelListing.Models.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.Model.Country
{
    public class GetCountryDto : BaseCountryDto
    {
        public int Id { get; set; } 
       
    }

  
}
