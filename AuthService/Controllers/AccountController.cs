using AuthService.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AccountController(IUnitOfWork unitOfWork, IConfiguration configuration, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _userManager = userManager;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistDTO model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, "Employee");
            return Ok(new { message = "User registered successfully with Employee role" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return Unauthorized("User not found."); // المستخدم غير موجود
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized("Invalid password."); // كلمة المرور غير صحيحة
            }

            // الحصول على الدور الأصلي للمستخدم من قاعدة البيانات
            var originalRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            
            // التحقق من الدور المطلوب
            if (originalRole != model.Role)
            {
                // إذا كان الدور المطلوب مختلفًا، نعينه مؤقتًا
                var tempRoleAssignmentResult = await _userManager.AddToRoleAsync(user, model.Role);
                if (!tempRoleAssignmentResult.Succeeded)
                {
                    return Unauthorized("User does not have permission for this role.");
                }

                // تحديد بداية الجلسة وتوقيت انتهاء التوكن
                var sessionStartTemp = DateTime.UtcNow;
                var jwtSettingsTemp = _configuration.GetSection("Jwt");
                var expiresTemp = sessionStartTemp.AddMinutes(Convert.ToDouble(jwtSettingsTemp["ExpireMinutes"]));

                // توليد التوكن مع الدور المؤقت
                var token = GenerateJwtToken(user, model.Role);

                // تسجيل جلسة الدور المؤقت في قاعدة البيانات
                var session = new UserRoleSession
                {
                    UserId = user.Id,
                    Role = model.Role,
                    SessionStart = sessionStartTemp,
                    SessionEnd = expiresTemp
                };

                _context.UserRoleSessions.Add(session);
                await _context.SaveChangesAsync();

                // إزالة الدور المؤقت بعد انتهاء الجلسة
                await _userManager.RemoveFromRoleAsync(user, model.Role);
                
                // إرجاع المعلومات الإضافية مع التوكن
                return Ok(new
                {
                    token,
                    role = model.Role,
                    sessionStartTemp,
                    expiresTemp
                });
            }

            // إذا كان الدور المطلوب هو نفس الدور المسجل، فقط توليد التوكن
            var sessionStart = DateTime.UtcNow;
            var jwtSettings = _configuration.GetSection("Jwt");
            var expires = sessionStart.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"]));
            var tokenSameRole = GenerateJwtToken(user, originalRole);


            return Ok(new
            {
                token = tokenSameRole,
                role = originalRole,
                sessionStart,
                expires
            });
        }




        private string GenerateJwtToken(ApplicationUser user, string role)
        {
            // توليد التوكن (يمكنك تخصيصه حسب الحاجة)
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
