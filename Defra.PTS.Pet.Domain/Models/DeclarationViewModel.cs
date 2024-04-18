using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.CodeAnalysis;

namespace Defra.PTS.Pet.Domain.Models;

[ExcludeFromCodeCoverage]
public class DeclarationViewModel : MultiPageViewModel
{
    public bool AgreedToAccuracy { get; set; }

    public bool AgreetToPrivacyPolicy { get; set; }

    public bool AgreedToDeclaration { get; set; }

    public override bool Validate(ModelStateDictionary modelState)
    {
       
        
        return true;
    }
}
