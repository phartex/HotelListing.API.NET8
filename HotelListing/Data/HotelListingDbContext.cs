using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace HotelListing.Data
{
    public class HotelListingDbContext : IdentityDbContext<ApiUser>
    {
        public HotelListingDbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name= "jamaica",
                    ShortName = "JM"

                },
                new Country
                {
                    Id = 2,
                    Name = "Bahamas",
                    ShortName = "BS"
                },
                new Country
                   {
                       Id = 3,
                       Name = "Cayman Island",
                       ShortName = "CI"
                   }
            );

            modelBuilder.Entity<Hotel>().HasData(
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
