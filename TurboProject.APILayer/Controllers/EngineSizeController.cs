using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurboProject.BusinessLayer.Model.ApiResponse;
using TurboProject.BusinessLayer.Model.DTO.Request.Brand;
using TurboProject.BusinessLayer.Model.DTO.Request.EngineSize;
using TurboProject.BusinessLayer.Model.DTO.Response.Brand;
using TurboProject.BusinessLayer.Model.DTO.Response.EngineSize;
using TurboProject.BusinessLayer.Service.Impl;
using TurboProject.BusinessLayer.Service.Interface;

namespace TurboProject.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EngineSizeController : ControllerBase
    {
        private readonly IEngineSizeService engineSizeService;

        public EngineSizeController(IEngineSizeService engineSizeService)
        {
            this.engineSizeService = engineSizeService;
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetAllSizes()
        {
            var response = new ApiResponse<List<GetEngineSizeDto>>();
            var size = await engineSizeService.GetAllEngineSizes();
            return Ok(size);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEngineSize([FromBody] CreateEngineSizeDto createEngineSizeDto)
        {
            var response = new ApiResponse<string>();
            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }
            await engineSizeService.CreateEngineSize(createEngineSizeDto);
            response.Success("Engine size successfully created!");
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = new ApiResponse<GetEngineSizeDto>();
            var size = await engineSizeService.GetEngineSizeById(id);
            if (size == null)
            {
                response.Error("Size not found");
                return NotFound(response);
            }
            response.Success(size);
            return Ok(response);
        }
        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEngineSize([FromBody] UpdateEngineSizeDto updateEngineSizeDto)
        {
            var response = new ApiResponse<string>();

            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }

            try
            {
                await engineSizeService.UpdateEngineSize(updateEngineSizeDto);
                response.Success("Size successfully updated");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Error(ex.Message);
                return NotFound(response);
            }
        }
        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEngineSize(int id)
        {
            var response = new ApiResponse<string>();

            await engineSizeService.DeleteEngineSize(id);
            response.Success("Engine size successfully deleted");

            return Ok(response);
        }
    }
}
