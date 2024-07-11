using FluentValidation;
using RepoLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureIsolatedFunc.FluentValidation
{
    public class FluentValidatorr: AbstractValidator<TestModel>
    {
        public FluentValidatorr()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id required");

            RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Email is required.");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Description required");
        }
    }
}
