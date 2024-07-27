using FluentValidation;

namespace HotelListing.Models.Country
{
    public class CountrySchoolsValidator : AbstractValidator<CountrySchoolsDto>
    {
        public CountrySchoolsValidator()
        {
            RuleFor(x => x.CountryName)
            .NotEmpty().WithMessage("Country name must not be empty.")
            .Matches(@"^[a-zA-Z]+$").WithMessage("Country name must contain only letters.");
        }
    }
}
