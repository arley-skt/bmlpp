using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class CentroValidation : AbstractValidator<Centro>
    {
        public CentroValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2,200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

           
        }
    }
}
