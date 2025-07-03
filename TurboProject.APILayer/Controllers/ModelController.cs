using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using TurboProject.BusinessLayer.Model.ApiResponse;
using TurboProject.BusinessLayer.Model.DTO.Request.Car;
using TurboProject.BusinessLayer.Model.DTO.Request.CarsModel;
using TurboProject.BusinessLayer.Model.DTO.Response.CarsModel;
using TurboProject.BusinessLayer.Service.Impl;
using TurboProject.BusinessLayer.Service.Interface;

namespace TurboProject.APILayer.Controllers
{
    [Route("api/model")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly ICarsModelService modelService;

        public ModelController(ICarsModelService modelService)
        {
            this.modelService = modelService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllModels()
        {
            var response = new ApiResponse<List<GetModelResponseDto>>();
            var models = await modelService.GetAllModels();

            if (models == null)
            {
                response.Error("Error while receiving data");
                return BadRequest(response);
            }

            response.Success(models); 
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = new ApiResponse<GetModelResponseDto>();
            var model = await modelService.GetModelById(id);
            if (model == null)
            {
                response.Error("Model not found");
                return NotFound(response);
            }
            response.Success(model);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateModelRequestDto dto)
        {
            var response = new ApiResponse<string>();
            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }
            await modelService.CreateModel(dto);
            response.Success("Model successfully created!");
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateModel([FromBody] UpdateModelRequestDto dto)
        {
            var response = new ApiResponse<string>();

            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }

            try
            {
                await modelService.UpdateModel(dto);
                response.Success("Model successfully updated");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Error(ex.Message);
                return NotFound(response);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            var response = new ApiResponse<string>();

            await modelService.DeleteModel(id);
            response.Success("Car successfully deleted");

            return Ok(response);
        }
    }
}
