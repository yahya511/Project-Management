/* using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using AuthService.DbContexts;
using Proto;

namespace AuthService.Services
{
    public class AuthGrpcService : Proto.Auth.AuthBase 
    {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;

    public AuthGrpcService(UserManager<ApplicationUser> userManager, ITokenService tokenService) {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context) {
        var user = await _userManager.FindByNameAsync(request.username);
        if (user != null && await _userManager.CheckPasswordAsync(user, request.password)) {
            var token = _tokenService.GenerateJwtToken(user);
            return new LoginResponse { token = token, success = true };
        }
        return new LoginResponse { success = false };
    }

    public override Task<TokenValidationResponse> ValidateToken(TokenValidationRequest request, ServerCallContext context) 
    {
        var principal = _tokenService.ValidateJwtToken(request.token);
        if (principal != null) {
            var role = principal.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
            return Task.FromResult(new TokenValidationResponse { isValid = true, role = role });
        }
        return Task.FromResult(new TokenValidationResponse { isValid = false });
    }
    }


}
 */