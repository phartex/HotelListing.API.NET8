using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Data.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
           builder.HasData(
                 new Hotel
                 {
                     Id = 1,
                     Name = "Sandals Resort and spa",
                     Address = "Negrill",
                     CountryId = 1,
                     Rating = 4.5
                 },
                   new Hotel
                   {
                       Id = 2,
                       Name = "Comfort Suites",
                       Address = "George Town",
                       CountryId = 3,
                       Rating = 4.3
                   }


                );
        }
    }
}
