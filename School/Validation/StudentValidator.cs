using FastEndpoints;
using FluentValidation;
using School.Model;

namespace School.Validation
{
    public class StudentValidator : Validator<Class>
    {
        public StudentValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(3, 50);
        }
    }
}
