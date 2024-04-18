using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Defra.PTS.Pet.Domain.Models

{

    public abstract class MultiPageViewModel
    {
        public bool IsCompleted { get; set; }

        public string Title { get; set; } = "Test Title";

        public abstract bool Validate(ModelStateDictionary modelState);

    }
}
