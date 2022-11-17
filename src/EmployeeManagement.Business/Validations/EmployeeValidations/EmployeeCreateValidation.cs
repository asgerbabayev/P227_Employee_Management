using EmployeeManagement.Business.DTOs.EmployeeDtos;
using FluentValidation;

namespace EmployeeManagement.Business.Validations.EmployeeValidations;

public class EmployeeCreateValidation : AbstractValidator<EmployeeCreateDto>
{
    public EmployeeCreateValidation()
    {
        RuleFor(a => a.Name).NotEmpty().NotNull().MaximumLength(64);
        RuleFor(a => a.Surname).NotNull().WithMessage("Surname bos ola bilmez").NotEmpty().WithMessage("Surname bos ola bilmez").MaximumLength(128);

        //null
        // " "
    }
}
