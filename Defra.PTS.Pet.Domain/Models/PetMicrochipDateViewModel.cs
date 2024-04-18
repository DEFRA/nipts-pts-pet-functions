using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.CodeAnalysis;

namespace Defra.PTS.Pet.Domain.Models;

[ExcludeFromCodeCoverage]
public class PetMicrochipDateViewModel : MultiPageViewModel
{
    public int? Day { get; set; }

    public int? Month { get; set; }

    public int? Year { get; set; }

    public DateTime? MicrochippedDate
    {
        get
        {
            try
            {
                return new DateTime(Year.GetValueOrDefault(), Month.GetValueOrDefault(), Day.GetValueOrDefault());
            }
            catch
            {
                return null;
            }
        }
    }
    public override bool Validate(ModelStateDictionary modelState)
    {
        return true;
    }
}
