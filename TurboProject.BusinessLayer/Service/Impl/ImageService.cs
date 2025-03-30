using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using TurboProject.BusinessLayer.Model.DTO.Response.Image;
using TurboProject.BusinessLayer.Service.Interface;
using TurboProject.DataLayer.Entity;
using TurboProject.DomainLayer.UoW.Interface;

namespace TurboProject.BusinessLayer.Service.Impl
{
    public class ImageService : IImageService
    {
        private readonly IUnitofWork unitofWork;
        private readonly IWebHostEnvironment env;
        private readonly IMapper mapper;
        private readonly string _imageRootPath = "images/cars";

        public ImageService(IUnitofWork unitofWork, IWebHostEnvironment env, IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.env = env;
            this.mapper = mapper;
        }
        public async Task DeleteImage(int imageId)
        {
            var image = await unitofWork.imageRepository.GetById(imageId);
            if (image == null)
                throw new KeyNotFoundException("Image not found");
            var filePath = Path.Combine(env.WebRootPath, image.ImageURL.TrimStart('/'));
            if (File.Exists(filePath))
                File.Delete(filePath);
            unitofWork.imageRepository.Delete(image);
            await unitofWork.Commit();
        }

        public async Task<List<GetImageDto>> GetImagesByCarId(int carId)
        {
            var images = await unitofWork.imageRepository.GetImagesByCarIdAsync(carId);
            return mapper.Map<List<GetImageDto>>(images);
        }

        public async Task<GetImageDto?> GetPrimaryImageByCarId(int carId)
        {
            var image = await unitofWork.imageRepository.GetPrimaryImageByCarIdAsync(carId);
            return mapper.Map<GetImageDto>(image);
        }

        public async Task UploadImage(int carId, IFormFile file, bool isPrimary)
        {

            if (file == null || file.Length == 0)
                throw new ArgumentException("Image file is required.");

            var car = await unitofWork.carRepository.GetById(carId);
            if (car == null)
                throw new KeyNotFoundException("Car not found.");

            var carFolderPath = Path.Combine(env.WebRootPath, _imageRootPath, carId.ToString());
            if (!Directory.Exists(carFolderPath))
                Directory.CreateDirectory(carFolderPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(carFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageUrl = $"/images/cars/{carId}/{fileName}";

            if (isPrimary)
            {
                var existingPrimaryImage = await unitofWork.imageRepository.GetPrimaryImageByCarIdAsync(carId);
                if (existingPrimaryImage != null)
                    existingPrimaryImage.IsPrimary = false;
            }

            var image = new Image
            {
                CarId = carId,
                ImageURL = imageUrl,
                IsPrimary = isPrimary
            };

            await unitofWork.imageRepository.Create(image);
            await unitofWork.Commit();


        }
    }
}
