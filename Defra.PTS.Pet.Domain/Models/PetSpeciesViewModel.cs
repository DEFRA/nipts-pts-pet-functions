using Defra.PTS.Pet.Domain.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.CodeAnalysis;

namespace Defra.PTS.Pet.Domain.Models;

[ExcludeFromCodeCoverage]
public class PetSpeciesViewModel : MultiPageViewModel
{
    public PetSpecies PetSpecies { get; set; }
    public override bool Validate(ModelStateDictionary modelState)
    {
        return true;
    }
}
