namespace Defra.PTS.Pet.Domain.Models
{
    public class PetViewModel
    {
        public Guid? CreatedBy { get; set; }
        public PetIdentificationViewModel PetIdentification { get; set; }
        public PetMicrochipDateViewModel PetMicrochipDate { get; set; }
        public PetSpeciesViewModel PetSpecies { get; set; }
        public PetBreedViewModel PetBreed { get; set; }
        public PetNameViewModel PetName { get; set; }
        public PetGenderViewModel PetGender { get; set; }
        public PetAgeViewModel PetAge { get; set; }
        public PetColourViewModel PetColour { get; set; }
        public PetFeatureViewModel PetFeature { get; set; }
        public List<PetIdentificationEvidenceViewModel> PetIdentificationEvidenceViewModel { get; set; }
        public PetMicrochipViewModel PetMicrochip { get; set; }
    }
}