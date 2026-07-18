using ECommerce.Application.Common;
using ECommerce.Application.DTOs.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Contracts
{
    public interface IIdentityService
    {
        Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email , CancellationToken ct = default);
        Task<Result<bool>> CheckPasswordAsync(string email , string password , CancellationToken ct = default);
        Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDto registerDto, CancellationToken ct = default);
        Task<Result<IReadOnlyList<string>>> GetUserRoles( string email, CancellationToken ct = default);

    }
}
