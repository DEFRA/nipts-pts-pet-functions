using Defra.PTS.Pet.Domain.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.CodeAnalysis;

namespace Defra.PTS.Pet.Domain.Models;

[ExcludeFromCodeCoverage]
public class PetIdentificationViewModel : MultiPageViewModel
{
    public const int MaxLengthMicrochip = 15;
    public const int MinLengthMicrochip = 15;

    public const int MaxLengthTattoo = 10;
    public const int MinLengthTattoo = 3;

    public PetIdentificationType IdentificationType { get; set; } 

    public string? MicrochipNumber { get; set; }

    public override bool Validate(ModelStateDictionary modelState)
    {
        throw new NotImplementedException();
    }
}
