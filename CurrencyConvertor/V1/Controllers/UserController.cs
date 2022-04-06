using System;
using System.Threading.Tasks;
using CurrencyConvertor.Extensions;
using CurrencyConvertor.Extensions.Exceptions;
using CurrencyConvertor.V1.DataTransferObjects;
using CurrencyConvertor.V1.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConvertor.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Register a new user account
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(UserDto user)
        {
            if (ModelState.IsValid)
            {
                await _userService.AddUserAsync(user);

                return Ok();
            }

            throw new CustomException(GlobalFunctions.GetModelStateErrors(ModelState));
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(UserDto user)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.LoginAsync(user);

                return Ok(response);
            }

            throw new CustomException(GlobalFunctions.GetModelStateErrors(ModelState));
        }
    }
}
