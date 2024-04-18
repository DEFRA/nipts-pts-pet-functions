using Defra.PTS.Pet.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defra.PTS.Pet.ApiServices.Interface
{
    public interface IPetService
    {        
        Task<Guid> CreatePet(PetViewModel petViewModel);

        Task<bool> PerformHealthCheckLogic();
    }
}
