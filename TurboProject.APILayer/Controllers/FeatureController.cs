using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurboProject.BusinessLayer.Model.ApiResponse;
using TurboProject.BusinessLayer.Model.DTO.Request.Features;
using TurboProject.BusinessLayer.Model.DTO.Response.Features;
using TurboProject.BusinessLayer.Service.Impl;
using TurboProject.BusinessLayer.Service.Interface;

namespace TurboProject.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureService featureService;

        public FeatureController(IFeatureService featureService)
        {
            this.featureService = featureService;
        }

        [HttpGet("{carId}")]
        public async Task<IActionResult> GetFeaturesByCarId(int carId)
        {
            var response = new ApiResponse<List<GetFeatureDto>>();
            var features = await featureService.GetFeaturesByCarId(carId);
            response.Success(features);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddFeature([FromBody] CreateFeatureDto dto)
        {
            var response = new ApiResponse<string>();
            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }

            await featureService.AddFeature(dto);
            response.Success("Feature added successfully.");
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            var response = new ApiResponse<string>();
            await featureService.DeleteFeature(id);
            response.Success("Feature deleted successfully.");
            return Ok(response);
        }
    }
}
