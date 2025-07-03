using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurboProject.BusinessLayer.Model.ApiResponse;
using TurboProject.BusinessLayer.Model.DTO.Request.Status;
using TurboProject.BusinessLayer.Model.DTO.Response.Status;
using TurboProject.BusinessLayer.Service.Interface;

namespace TurboProject.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService statusService;

        public StatusController(IStatusService statusService)
        {
            this.statusService = statusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStatuses()
        {
            var response = new ApiResponse<List<GetStatusDto>>();
            var statuses = await statusService.GetAllStatuses();
            response.Success(statuses);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatusById(int id)
        {
            var response = new ApiResponse<GetStatusDto>();
            var status= await statusService.GetById(id);
            if (status != null)
            {
                response.Error("Status not found");
                return NotFound(response);
            }
            response.Success(status);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateStatus([FromBody] CreateStatusDto dto)
        {
            var response = new ApiResponse<string>();

            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }

            await statusService.CreateStatus(dto);
            response.Success("Status created successfully.");
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto dto)
        {
            var response = new ApiResponse<string>();

            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }

            await statusService.UpdateStatus(id, dto);
            response.Success("Status updated successfully.");
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteStatus(int id)
        {
            var response = new ApiResponse<string>();

            await statusService.DeleteStatus(id);
            response.Success("Status deleted successfully.");
            return Ok(response);
        }
    }
}
