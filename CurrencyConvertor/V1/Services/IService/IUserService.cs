using CurrencyConvertor.V1.DataTransferObjects;
using System.Threading.Tasks;

namespace CurrencyConvertor.V1.Services.IService
{
    /// <summary>
    /// Service for managing Users
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Add new Identity User Account
        /// </summary>
        /// <param name="userAddDto"></param>
        Task AddUserAsync(UserDto userAddDto);
        Task<LoginResponseDto> LoginAsync(UserDto user);
    }
}
