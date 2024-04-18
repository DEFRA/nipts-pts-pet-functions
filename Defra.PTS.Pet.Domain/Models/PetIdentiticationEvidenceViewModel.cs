using Defra.PTS.Pet.Domain.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.CodeAnalysis;

namespace Defra.PTS.Pet.Domain.Models;

[ExcludeFromCodeCoverage]
public class PetIdentificationEvidenceViewModel : MultiPageViewModel
{
    public string fileName { get; set; }

    public byte[]? FileUpload { get; set; }

    public PetIdentificationType IdentificationType { get; set; }

    public override bool Validate(ModelStateDictionary modelState)
    {
        return true;
    }
}

