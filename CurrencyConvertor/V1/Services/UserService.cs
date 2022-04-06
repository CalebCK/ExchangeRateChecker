using CurrencyConvertor.Configurations;
using CurrencyConvertor.Extensions;
using CurrencyConvertor.Extensions.Exceptions;
using CurrencyConvertor.V1.DataTransferObjects;
using CurrencyConvertor.V1.Services.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConvertor.V1.Services
{
    public partial class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtTokenSettings _jwtTokenSettings;

        public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JwtTokenSettings> jwtTokenSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenSettings = jwtTokenSettings.Value;
        }

        public async Task AddUserAsync(UserDto userAddDto)
        {
            if (userExists(userAddDto.Email) || userEmailExists(userAddDto.Email))
                throw new CustomException("User exists");

            IdentityUser identityUser = new IdentityUser
            {
                UserName = userAddDto.Email,
                Email = userAddDto.Email,
            };

            var res = await _userManager.CreateAsync(identityUser, userAddDto.Password);

            if (!res.Succeeded)
                throw new CustomException(GlobalFunctions.GetIdentityErrors(res.Errors.ToList()));
        }

        public async Task<LoginResponseDto> LoginAsync(UserDto user)
        {
            var identityUser = await _userManager.FindByNameAsync(user.Email);
            LoginResponseDto tokenObject = new LoginResponseDto();

            if (identityUser is null)
                throw new InvalidLoginException();

            var passwordCheck = await _userManager.CheckPasswordAsync(identityUser, user.Password);

            if (!passwordCheck)
                throw new InvalidLoginException();

            tokenObject = await GenerateTokenObject(identityUser);

            return tokenObject;
        }
    }
}
