using Defra.PTS.Pet.Domain.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.CodeAnalysis;

namespace Defra.PTS.Pet.Domain.Models;

[ExcludeFromCodeCoverage]
public class PetAgeViewModel : MultiPageViewModel
{
    public YesNo KnowDoB {  get; set; }
   public string? Day { get; set; }

    public string? Month { get; set; }

    public string? Year { get; set; }

    public DateTime? BirthDate
    {
        get
        {
            try
            {
                _ = int.TryParse(Day, out int day);
                _ = int.TryParse(Month, out int month);
                _ = int.TryParse(Year, out int year);
                return new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
            }
            catch
            {
                return null;
            }
        }
    }

    public int? ApproximateAge { get; set; }


    public override bool Validate(ModelStateDictionary modelState)
    {

        return true;
    }
}
