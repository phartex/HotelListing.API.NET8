using HotelListing.Contracts;
using HotelListing.Data;
using HotelListing.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly UserManager<ApiUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(IAuthManager authManager, UserManager<ApiUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._authManager = authManager;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }
        //POST: api/Account/register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult> Register([FromBody] ApiUserDto apiUserDto)
        {
            //var errors = await _authManager.Register(apiUserDto);

            //if(errors.Any())
            //{
            //    foreach(var error in errors)
            //    {
            //        ModelState.AddModelError(error.Code, error.Description);
            //    }
            //    return BadRequest(ModelState);
            //}
            //return Ok();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApiUser
            {
                UserName = apiUserDto.Email,  // Use Email as UserName
                Email = apiUserDto.Email,
                FirstName = apiUserDto.FirstName,
                LastName = apiUserDto.LastName
            };

            var result = await _userManager.CreateAsync(user, apiUserDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            // Assign a default role to the user
            //var roleExists = await _roleManager.RoleExistsAsync("Administrator");
            //if (!roleExists)
            //{
            //    await _roleManager.CreateAsync(new IdentityRole("Administrator"));
            //}

            //await _userManager.AddToRoleAsync(user, "Administrator");
            return Ok();

        }



        //POST: api/Account/login
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            var authResponse = await _authManager.Login(loginDto);

            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);
        }


         //POST: api/Account/refreshtoken
        [HttpPost]
        [Route("refreshtoken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult> RefreshToken([FromBody] AuthResponseDto request)
        {
            var authResponse = await _authManager.VerifyRefreshToken(request);

            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);
        }

        //POST:api/Account/create-admin
        [HttpPost]
        [Route("create-admin")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult> CreateAdmin([FromBody] ApiUserDto apiUserDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApiUser
            {
                UserName = apiUserDto.Email,  // Use Email as UserName
                Email = apiUserDto.Email,
                FirstName = apiUserDto.FirstName,
                LastName = apiUserDto.LastName
            };

            var result = await _userManager.CreateAsync(user, apiUserDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            if (!await _roleManager.RoleExistsAsync("Administrator"))
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole("Administrator"));
                if (!roleResult.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error creating role");
                }
            }

            // Assign the "Administrator" role to the new user
            var roleAssignResult = await _userManager.AddToRoleAsync(user, "Administrator");
            if (!roleAssignResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error assigning role");
            }

            return Ok();



        }



    }
}
