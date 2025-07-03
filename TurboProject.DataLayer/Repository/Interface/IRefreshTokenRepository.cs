using TurboProject.DataLayer.Entity;

namespace TurboProject.DataLayer.Repository.Interface
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
        Task<RefreshToken> GetByTokenAsync(string token);
        void Update(RefreshToken token);
    }
}
