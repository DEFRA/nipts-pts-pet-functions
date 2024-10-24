using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.CodeAnalysis;

namespace Defra.PTS.Pet.Domain.Models;

[ExcludeFromCodeCoverage]
    public class AcknowledgementViewModel : MultiPageViewModel
    {
        public string? Reference { get; set; }

        public bool IsSuccess { get; set; }

        public override bool Validate(ModelStateDictionary modelState)
        {
            return true;
        }
    }

