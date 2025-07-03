using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using TurboProject.BusinessLayer.Model.DTO.Response.Account;
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.DataLayer.Context;
using TurboProject.DataLayer.Entity;
using TurboProject.DataLayer.Repository.Interface;

namespace TurboProject.BusinessLayer.Service.Impl
{
    public class TokenService : ITokenService
    {
        private readonly IJWTService jWTService;
        private readonly IRefreshTokenRepository refreshTokenRepository;
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;

        public TokenService(IJWTService jWTService, IRefreshTokenRepository refreshTokenRepository,
                AppDbContext context, IConfiguration configuration, UserManager<User> userManager)
        {
            this.jWTService = jWTService;
            this.refreshTokenRepository = refreshTokenRepository;
            this.context = context;
            this.configuration = configuration;
            this.userManager = userManager;
        }
        public async Task<LoginResponseDto> GenerateTokensAsync(
         User user,
         IList<string> roles,
         string ipAddress)
        {
            // 1) Создаём Access Token
            var accessToken = jWTService.GenerateToken(user, roles.ToList());

            // 2) Генерируем Refresh Token
            var days = int.Parse(configuration["JwtSettings:RefreshExpireDays"]);
            var refresh = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(days),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress,
                UserId = user.Id
            };

            await refreshTokenRepository.AddAsync(refresh);
            await context.SaveChangesAsync();

            // 3) Возвращаем оба токена
            return new LoginResponseDto
            {
                Token = accessToken,
                RefreshToken = refresh.Token,
                Email = user.Email,
                Roles = roles.ToList()
            };
        }

        public async Task<LoginResponseDto> RefreshAsync(
            string refreshToken,
            string ipAddress)
        {
            // 1) Находим в БД
            var existing = await refreshTokenRepository.GetByTokenAsync(refreshToken)
                           ?? throw new SecurityTokenException("Invalid refresh token");

            if (!existing.IsActive)
                throw new SecurityTokenException("Refresh token expired or revoked");

            // 2) Аннулируем старый
            existing.Revoked = DateTime.UtcNow;
            existing.RevokedByIp = ipAddress;

            // 3) Получаем пользователя и его роли
            var user = existing.User;
            var roles = await userManager.GetRolesAsync(user);

            // 4) Генерируем новую пару токенов
            var newTokens = await GenerateTokensAsync(user, roles, ipAddress);

            // 5) Сохраняем ссылку на новый RT и апдейтим старый
            existing.ReplacedByToken = newTokens.RefreshToken;
            refreshTokenRepository.Update(existing);
            await context.SaveChangesAsync();

            return newTokens;
        }
    }
}
