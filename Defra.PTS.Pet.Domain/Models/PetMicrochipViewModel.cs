using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defra.PTS.Pet.Domain.Models
{
    public class PetMicrochipViewModel : MultiPageViewModel
    {
        public string? MicrochipNumber { get; set; }

        public override bool Validate(ModelStateDictionary modelState)
        {
            throw new NotImplementedException();
        }
    }
}
