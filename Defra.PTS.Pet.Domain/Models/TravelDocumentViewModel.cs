using System.Diagnostics.CodeAnalysis;

namespace Defra.PTS.Pet.Domain.Models;

[ExcludeFromCodeCoverage]
public class TravelDocumentViewModel
{
    public PetIdentificationViewModel PetIdentification { get; set; } = new PetIdentificationViewModel();

    public PetIdentificationEvidenceViewModel PetIdentificationEvidence { get; set; } = new PetIdentificationEvidenceViewModel();

    public PetMicrochipDateViewModel PetMicrochipDate { get; set; } = new PetMicrochipDateViewModel();

    public PetSpeciesViewModel PetSpecies { get; set; } = new PetSpeciesViewModel();

    public PetBreedViewModel PetBreed { get; set; } = new PetBreedViewModel();

    public PetNameViewModel PetName { get; set; } = new PetNameViewModel();

    public PetGenderViewModel PetGender { get; set; } = new PetGenderViewModel();

    public PetAgeViewModel PetAge { get; set; } = new PetAgeViewModel();

    public PetColourViewModel PetColour { get; set; } = new PetColourViewModel();

    public PetFeatureViewModel PetFeature { get; set; } = new PetFeatureViewModel();

    public DeclarationViewModel Declaration { get; set; } = new DeclarationViewModel();

    public AcknowledgementViewModel Acknowledgement { get; set; } = new AcknowledgementViewModel();
}
