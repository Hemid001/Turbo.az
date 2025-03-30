using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurboProject.BusinessLayer.Model.ApiResponse;
using TurboProject.BusinessLayer.Model.DTO.Request.FuelType;
using TurboProject.BusinessLayer.Model.DTO.Request.Transmission;
using TurboProject.BusinessLayer.Model.DTO.Response.FuelType;
using TurboProject.BusinessLayer.Model.DTO.Response.Transmission;
using TurboProject.BusinessLayer.Service.Impl;
using TurboProject.BusinessLayer.Service.Interface;

namespace TurboProject.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransmissionController : ControllerBase
    {
        private readonly ITransmissionService transmissionService;

        public TransmissionController(ITransmissionService transmissionService)
        {
            this.transmissionService = transmissionService;
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetAllTypes()
        {
            var response = new ApiResponse<List<GetTransmissionTypeDto>>();
            var types = await transmissionService.GetAllTransmissionTypes();
            return Ok(types);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTransmissionType([FromBody] CreateTransmissionTypeDto createTransmissionTypeDto)
        {
            var response = new ApiResponse<string>();
            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }
            await transmissionService.CreateTransmissionType(createTransmissionTypeDto);
            response.Success("Transmission type successfully created!");
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = new ApiResponse<GetTransmissionTypeDto>();
            var type = await transmissionService.GetTransmissionTypeById(id);
            if (type == null)
            {
                response.Error("Transmission type not found");
                return NotFound(response);
            }
            response.Success(type);
            return Ok(response);
        }
        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTransmissionType(int id)
        {
            var response = new ApiResponse<string>();

            await transmissionService.DeleteTransmissionType(id);
            response.Success("Transmission type successfully deleted");

            return Ok(response);
        }
    }
}
