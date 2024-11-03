namespace AuthService.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            
            if (token != null)
                await AttachUserToContext(context, userManager, token);

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, UserManager<ApplicationUser> userManager, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                }, out var validatedToken);

                var userId = ((JwtSecurityToken)validatedToken).Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var user = await userManager.FindByIdAsync(userId);
                context.Items["User"] = user; // Store user in context
            }
            catch
            {
                // Do nothing if JWT validation fails
            }
        }
    }
}
