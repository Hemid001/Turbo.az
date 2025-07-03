using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Request.Favorite;

namespace TurboProject.BusinessLayer.Validators.Favorite
{
    public class CreateFavoriteDtoValidator: AbstractValidator<CreateFavoriteDto>
    {
        public CreateFavoriteDtoValidator()
        {
            RuleFor(x => x.CarId)
                .GreaterThan(0).WithMessage("CarId must be greater than 0.");
        }
    }
}
