using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TurboProject.BusinessLayer.Model.ApiResponse;
using TurboProject.BusinessLayer.Model.DTO.Request.BodyType;
using TurboProject.BusinessLayer.Model.DTO.Response.BodyType;
using TurboProject.BusinessLayer.Service.Interface;

namespace TurboProject.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyTypeController : ControllerBase
    {
        private readonly IBodyTypeService bodyTypeService;

        public BodyTypeController(IBodyTypeService bodyTypeService)
        {
            this.bodyTypeService = bodyTypeService;
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetAllTypes()
        {
            var response = new ApiResponse<List<GetBodyTypeDto>>();
            var types = await bodyTypeService.GetAllTypes();
            return Ok(types);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBodyType([FromBody] CreateBodyTypeDto createBodyTypeDto)
        {
            var response = new ApiResponse<string>();
            if (!ModelState.IsValid)
            {
                response.Error(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }
            await bodyTypeService.CreateBodyType(createBodyTypeDto);
            response.Success("Body type successfully created!");
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = new ApiResponse<GetBodyTypeDto>();
            var type = await bodyTypeService.GetById(id);
            if (type == null)
            {
                response.Error("Body type not found");
                return NotFound(response);
            }
            response.Success(type);
            return Ok(response);
        }
        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBodyType(int id)
        {
            var response = new ApiResponse<string>();

            await bodyTypeService.DeleteBodyType(id);
            response.Success("Body type successfully deleted");

            return Ok(response);
        }
    }
}
