﻿using HotelListing.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);

        Task<AuthResponseDto> Login(LoginDto loginDto);

        Task<string> CreateRefreshToken();

        Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
    }
}
