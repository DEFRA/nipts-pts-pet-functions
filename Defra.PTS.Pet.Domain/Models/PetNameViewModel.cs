using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.CodeAnalysis;

namespace Defra.PTS.Pet.Domain.Models;

[ExcludeFromCodeCoverage]
public class PetNameViewModel : MultiPageViewModel
{
    public string PetName { get; set; }
    public override bool Validate(ModelStateDictionary modelState)
    {
        return true;
    }
}
