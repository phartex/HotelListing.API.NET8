using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.Data;
using AutoMapper;
using HotelListing.Model.Country;
using HotelListing.Models.Country;
using System.Diagnostics.Metrics;
using HotelListing.Contracts;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Identity;
using FluentValidation;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRepository;
        private readonly HttpClient _httpClient;
        private readonly ILogger<CountriesController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public CountriesController(IMapper mapper, ICountriesRepository countriesRepository,ILogger<CountriesController> logger, IHttpClientFactory clientFactory)
        {
            this._mapper = mapper;
            this._countriesRepository = countriesRepository;
            this._logger = logger;
            this._httpClientFactory = clientFactory;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var countries = await _countriesRepository.GetAllAsync();
            var records = _mapper.Map<List<GetCountryDto>>(countries);
            return Ok(records);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            var country = await _countriesRepository.GetDetails(id);

            if (country == null)
            {
                return NotFound();
            }

            var countryDto = _mapper.Map<CountryDto>(country);

            return countryDto;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                return BadRequest();
            }

            var country = await _countriesRepository.GetAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            _mapper.Map(updateCountryDto, country);

            try
            {
                await _countriesRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(country);
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Country>> PostCountry(Country createCountryDto)
        {
            
            var country = _mapper.Map<Country>(createCountryDto);
        
            await _countriesRepository.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            await _countriesRepository.DeleteAsync(id);

            return NoContent();
        }


        [HttpGet("/countrySchool/{country_name}")]
        public async Task<IActionResult> GetCountrySchool([FromRoute] string country_name)
        {
            var countryDto = new CountrySchoolsDto { CountryName = country_name };
            var validator = new CountrySchoolsValidator();
            var validatorResult = await validator.ValidateAsync(countryDto);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }


            var httpClient = _httpClientFactory.CreateClient();

            var httpRequestMesage = new HttpRequestMessage(HttpMethod.Get, $"http://universities.hipolabs.com/search?country={country_name}");

            var response = await httpClient.SendAsync(httpRequestMesage);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                return Content(result, "application/json");
            }

            return BadRequest();
        }



        private async Task<bool> CountryExists(int id)
        {
            return await _countriesRepository.Exists(id);
        }
    }
}
