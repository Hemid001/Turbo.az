using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TurboProject.BusinessLayer.Model.ApiResponse;
using TurboProject.BusinessLayer.Model.DTO.Request.Account;
using TurboProject.BusinessLayer.Model.DTO.Response.Account;
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.DataLayer.Entity;

namespace TurboProject.APILayer.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IJWTService jWTService;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
        private readonly IRedisService redisService;
        private readonly ITokenService tokenService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            IJWTService jWTService, IMapper mapper, IEmailService emailService, IRedisService redisService,
            ITokenService tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jWTService = jWTService;
            this.mapper = mapper;
            this.emailService = emailService;
            this.redisService = redisService;
            this.tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
        {
            var response = new ApiResponse<string>();

            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }

            if (model.Password != model.ConfirmPassword)
            {
                response.Error("Passwords do not match");
                return BadRequest(response);
            }

            var existingUser = await userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                response.Error("User with this email already exists");
                return BadRequest(response);
            }

            // Сохраняем пользователя в Redis
            var user = mapper.Map<User>(model);
            user.Id = Guid.NewGuid().ToString();
            user.PasswordHash = model.Password;

            try
            {
                await redisService.SetUserAsync($"User:{user.Id}", user, 10);
            }
            catch (Exception ex)
            {
                // Логируйте ошибку или верните ответ об ошибке
                response.Error($"Error with Redis: {ex.Message}");
                return StatusCode(500, response);
            }

            // Отправляем подтверждение email
            var confirmationToken = Uri.EscapeDataString(user.Id);
            var confirmationLink = $"https://localhost:44307/api/account/confirm-email?token={confirmationToken}";

            try
            {
                await emailService.SendEmailAsync(user.Email, "Confirm your email",
                    $"Click <a href='{confirmationLink}'>here</a> to confirm your email.");
            }
            catch (Exception ex)
            {
                response.Error($"Error sending email: {ex.Message}");
                return StatusCode(500, response);
            }

            response.Success("User registered successfully. Check your email for confirmation.");
            return Ok(response);
        }



        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
        {
            var response = new ApiResponse<string>();

            if (string.IsNullOrEmpty(token))
            {
                response.Error("Invalid email confirmation request");
                return BadRequest(response);
            }

            // Получаем пользователя из Redis
            var user = await redisService.GetUserAsync<User>($"User:{token}");
            if (user == null)
            {
                response.Error("User not found or token expired");
                return BadRequest(response);
            }

            // Добавляем пользователя в базу
            var result = await userManager.CreateAsync(user, user.PasswordHash);
            if (!result.Succeeded)
            {
                response.Error(result.Errors.Select(e => e.Description).ToList());
                return BadRequest(response);
            }

            await userManager.AddToRoleAsync(user, "User");

            // Удаляем из Redis
            await redisService.DeleteUserAsync($"User:{token}");

            response.Success("Email confirmed and user added to database!");
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var response = new ApiResponse<LoginResponseDto>();
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                response.Error("Incorrect email or password");
                return Unauthorized(response);
            }

            var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.IsLockedOut)
            {
                return Forbid("Account is locked");
            }
            if (!result.Succeeded)
            {
                response.Error("Incorrect email or password");
                return Unauthorized(response);
            }

            var roles = await userManager.GetRolesAsync(user);
            var tokens = await tokenService.GenerateTokensAsync(
                user,
                roles,
                HttpContext.Connection.RemoteIpAddress?.ToString()
            );

            response.Success(tokens);
            return Ok(response);
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto model)
        {
            var response = new ApiResponse<LoginResponseDto>();
            try
            {
                var tokens = await tokenService
           .RefreshAsync(
               model.RefreshToken,
               HttpContext.Connection.RemoteIpAddress?.ToString()
           );

                response.Success(tokens);
                return Ok(response);
            }
            catch (SecurityTokenException ex)
            {
                response.Error(ex.Message);
                return Unauthorized(response);
            }
        }


    }
}
