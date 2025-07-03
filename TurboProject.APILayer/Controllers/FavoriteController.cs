using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TurboProject.BusinessLayer.Model.ApiResponse;
using TurboProject.BusinessLayer.Model.DTO.Request.Favorite;
using TurboProject.BusinessLayer.Model.DTO.Response.Favorite;
using TurboProject.BusinessLayer.Service.Impl;
using TurboProject.BusinessLayer.Service.Interface;

namespace TurboProject.APILayer.Controllers
{
    [Route("api/favorite")]
    [ApiController]
    [Authorize]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            this.favoriteService = favoriteService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<GetFavoriteDto>>>> GetUserFavorites()
        {
            var response = new ApiResponse<List<GetFavoriteDto>>();

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var favorites = await favoriteService.GetUserFavoritesAsync(userId);

            if (favorites == null || favorites.Count == 0)
            {
                response.Error("No favorites found.");
                return NotFound(response);
            }

            response.Success(favorites);
            return Ok(response);
        }

       
        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> AddToFavorites([FromBody] CreateFavoriteDto dto)
        {
            var response = new ApiResponse<string>();
         
            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                response.Error("User not authenticated");
                return Unauthorized(response);
            }
            await favoriteService.AddToFavoritesAsync(userId, dto);

            response.Success("Car added to favorites.");
            return Ok(response);
        }

  
        [HttpDelete("{carId}")]
        public async Task<ActionResult<ApiResponse<string>>> RemoveFromFavorites(int carId)
        {
            var response = new ApiResponse<string>();

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                response.Error("Invalid user identifier.");
                return Unauthorized(response);
            }

            await favoriteService.RemoveFromFavoritesAsync(userId, carId);     

            response.Success("Car removed from favorites.");
            return Ok(response);
        }
    }
}
