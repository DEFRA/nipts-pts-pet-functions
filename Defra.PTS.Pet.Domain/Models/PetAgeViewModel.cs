using Defra.PTS.Pet.Domain.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.CodeAnalysis;

namespace Defra.PTS.Pet.Domain.Models;

[ExcludeFromCodeCoverage]
public class PetAgeViewModel : MultiPageViewModel
{
    public YesNo KnowDoB {  get; set; }
    public DateTime? BirthDate
    {
        get
        {
            try
            {
                return new DateTime(Year.GetValueOrDefault(), Month.GetValueOrDefault(), Day.GetValueOrDefault(),0,0,0, DateTimeKind.Utc);
            }
            catch
            {
                return null;
            }
        }
    }

    public int? Day { get; set; }

    public int? Month { get; set; }

    public int? Year { get; set; }

    public int? ApproximateAge { get; set; }


    public override bool Validate(ModelStateDictionary modelState)
    {

        return true;
    }
}
