using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.CodeAnalysis;

namespace Defra.PTS.Pet.Domain.Models;

[ExcludeFromCodeCoverage]
public class PetMicrochipDateViewModel : MultiPageViewModel
{
    public string? Day { get; set; }

    public string? Month { get; set; }

    public string? Year { get; set; }

    public DateTime? MicrochippedDate
    {
        get
        {
            try
            {
                _ = int.TryParse(Day, out int day);
                _ = int.TryParse(Month, out int month);
                _ = int.TryParse(Year, out int year);
                return new DateTime(year, month, day);
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
