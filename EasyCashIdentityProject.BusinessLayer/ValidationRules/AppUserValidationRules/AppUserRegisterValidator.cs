using EasyCashIdentityProject.DtoLayer.Dtos.AppUserDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.BusinessLayer.ValidationRules.AppUserValidationRules
{
    public class AppUserRegisterValidator : AbstractValidator<AppUserRegisterDto>
    {
        public AppUserRegisterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(5).WithMessage("Minimum 5 xarakter daxil olmalidi").MaximumLength(30).WithMessage("Maksimum 30 xarakter daxil olunmalidi");
            RuleFor(x => x.Surname).NotEmpty().MinimumLength(5).WithMessage("Minimum 5 xarakter daxil olmalidi").MaximumLength(30).WithMessage("Maksimum 30 xarakter daxil olunmalidi");
            RuleFor(x => x.Username).NotEmpty().MinimumLength(5).WithMessage("Minimum 5 xarakter daxil olmalidi").MaximumLength(30).WithMessage("Maksimum 30 xarakter daxil olunmalidi");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Mail adresi daxil edin");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(5).WithMessage("Minimum 5 xarakter daxil olmalidi").MaximumLength(30).WithMessage("Maksimum 30 xarakter daxil olunmalidi");
            RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(y => y.Password).WithMessage("Parol Eyni Olmalidir");
            RuleFor(x => x.Email).NotEqual(y => y.Email).WithMessage("Artiq qeydiyyatdan kecib");
        }
    }
}
