using Defra.PTS.Pet.Domain.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.CodeAnalysis;

namespace Defra.PTS.Pet.Domain.Models;

[ExcludeFromCodeCoverage]
public class PetBreedViewModel : MultiPageViewModel
{
    public PetSpecies PetSpecies { get; set; }
    public BreedType BreedType { get; set; }
    public string BreedName { get; set; }
    public int BreedId { get; set; }
    public string BreedAdditionalInfo { get; set; }
    public override bool Validate(ModelStateDictionary modelState)
    {
        return true;
    }
}
