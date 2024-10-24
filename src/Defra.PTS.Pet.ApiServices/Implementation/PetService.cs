using Defra.PTS.Pet.ApiServices.Interface;
using Defra.PTS.Pet.Repositories.Interface;
using Defra.PTS.Pet.Domain.Entities;
using Defra.PTS.Pet.Domain.Models;
using System.Diagnostics.CodeAnalysis;
using Defra.PTS.Pet.Domain.Enums;
using System.Linq;

namespace Defra.PTS.Pet.ApiServices.Implementation
{
    public class PetService(
        IPetRepository petRepository,
        IPetDocumentEvidenceRepository petDocumentEvidenceRepository) : IPetService
    {        
        private readonly IPetRepository _petRepository = petRepository;
        private readonly IPetDocumentEvidenceRepository _petDocumentEvidenceRepository = petDocumentEvidenceRepository;

        public async Task<Guid> CreatePet(PetViewModel petViewModel)
        {
            var newPetDbEntry = new PetEntity()
            {                
                IdentificationType = (int)PetIdentificationType.Microchipped,
                MicrochipNumber = petViewModel.PetMicrochip!.MicrochipNumber,
                MicrochippedDate = petViewModel.PetMicrochipDate?.MicrochippedDate,
                SpeciesId = (int)petViewModel.PetSpecies!.PetSpecies,
                BreedTypeId = (int)petViewModel.PetBreed!.BreedType,
                BreedId = petViewModel.PetBreed.BreedId,
                AdditionalInfoMixedBreedOrUnknown = petViewModel.PetBreed?.BreedAdditionalInfo,
                Name = petViewModel.PetName!.PetName,
                SexId = (int)petViewModel.PetGender!.Gender,
                IsDateOfBirthKnown = (int)petViewModel.PetAge!.KnowDoB,
                DOB = petViewModel.PetAge?.BirthDate,
                ApproximateAge = petViewModel.PetAge?.ApproximateAge,
                ColourId = petViewModel.PetColour!.PetColour,
                OtherColour = petViewModel.PetColour?.PetColourOther,
                HasUniqueFeature = (int)petViewModel.PetFeature!.HasUniqueFeature,
                UniqueFeatureDescription = petViewModel.PetFeature?.FeatureDescription,
                CreatedBy = petViewModel.CreatedBy,
                CreatedOn = DateTime.Now
            };

            var speciesWithBreed = new List<PetSpecies>() { 
                PetSpecies.Dog,
                PetSpecies.Cat
            };

            if (!speciesWithBreed.Contains(petViewModel!.PetSpecies.PetSpecies))
            {
                newPetDbEntry.BreedId = null;
                newPetDbEntry.BreedTypeId = null;
            }

            await _petRepository.CreatePet(newPetDbEntry);

            var petDocumentEvidence = new List<PetDocumentEvidenceEntity>();

            foreach (var item in petViewModel!.PetIdentificationEvidenceViewModel ?? Enumerable.Empty<PetIdentificationEvidenceViewModel>())
            {
                petDocumentEvidence.Add(new PetDocumentEvidenceEntity()
                {
                    PetId = newPetDbEntry.Id,
                    EvidenceReference = item.FileName,
                    CreatedBy = petViewModel.CreatedBy,
                    CreatedOn = DateTime.Now
                });
            }

            await _petDocumentEvidenceRepository.SavePetDocumentEvidence(petDocumentEvidence);
            return newPetDbEntry.Id;
        }

        [ExcludeFromCodeCoverage]
        public async Task<bool> PerformHealthCheckLogic()
        {
            return await _petRepository.PerformHealthCheckLogic();
        }
    }  
}

