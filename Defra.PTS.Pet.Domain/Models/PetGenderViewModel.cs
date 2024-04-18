using Defra.PTS.Pet.Domain.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.CodeAnalysis;

namespace Defra.PTS.Pet.Domain.Models;

public class PetGenderViewModel : MultiPageViewModel
{
    public PetGender Gender { get; set; }

    [ExcludeFromCodeCoverage]
    public override bool Validate(ModelStateDictionary modelState)
    {
        return true;
    }
}