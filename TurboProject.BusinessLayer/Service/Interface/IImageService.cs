using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.Image;
using TurboProject.BusinessLayer.Model.DTO.Response.Image;

namespace TurboProject.BusinessLayer.Service.Interface
{
    public interface IImageService
    {
        Task<List<GetImageDto>> GetImagesByCarId(int carId);
        Task<GetImageDto?> GetPrimaryImageByCarId(int carId);
        Task UploadImage(int carId, IFormFile file, bool isPrimary);
        Task DeleteImage(int imageId);
    }
}
