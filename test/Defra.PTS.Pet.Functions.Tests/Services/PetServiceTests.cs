using Defra.PTS.Pet.ApiServices.Implementation;
using Defra.PTS.Pet.Domain.Entities;
using Defra.PTS.Pet.Domain.Enums;
using Defra.PTS.Pet.Domain.Models;
using Defra.PTS.Pet.Repositories.Interface;
using Moq;
using System.Text.Json;
using NUnit.Framework;

namespace Defra.PTS.Pet.Functions.Tests.Services
{
    [TestFixture]
    public class PetServiceTests
    {
        private Mock<IPetRepository>? _mockPetRepostory;
        private Mock<IPetDocumentEvidenceRepository>? _mockPetDocumentEvidenceRepository;

        private PetService? _sut;

        [SetUp]
        public void Setup()
        {
            _mockPetRepostory = new Mock<IPetRepository>();
            _mockPetDocumentEvidenceRepository = new Mock<IPetDocumentEvidenceRepository>();

            _sut = new PetService(_mockPetRepostory.Object, _mockPetDocumentEvidenceRepository.Object);
        }

        [Test]
        public async Task CreatePet()
        {
            var petVm = new PetViewModel
            {
                PetIdentification = new PetIdentificationViewModel
                {
                    IdentificationType = PetIdentificationType.Microchipped,
                    MicrochipNumber = "123456789012345",
                },
                PetMicrochipDate = new PetMicrochipDateViewModel
                {
                    Day = "1",
                    Month = "1",
                    Year = "2008",
                    Title = "Test",
                    IsCompleted = true,
                },
                PetMicrochip = new PetMicrochipViewModel
                {
                    MicrochipNumber = "123456789012345"
                },
                PetSpecies = new PetSpeciesViewModel
                {
                    PetSpecies = PetSpecies.Dog
                },
                PetBreed = new PetBreedViewModel
                {
                    BreedType = BreedType.PurebredOrPedigree,
                    BreedId = 1
                },
                PetName = new PetNameViewModel
                {
                    PetName = "Fido"
                },
                PetGender = new PetGenderViewModel
                {
                    Gender = PetGender.Male,
                },
                PetAge = new PetAgeViewModel
                {
                    KnowDoB = YesNo.No
                },
                PetColour = new PetColourViewModel
                {
                    PetColour = 1,
                    PetColourOther = "grey"
                },
                PetFeature = new PetFeatureViewModel
                {
                    HasUniqueFeature = YesNo.No,
                    FeatureDescription = "Test",                    
                },
                PetIdentificationEvidenceViewModel = new List<PetIdentificationEvidenceViewModel> { new PetIdentificationEvidenceViewModel() { fileName = "item1.png" } }
            };

            _mockPetRepostory!.Setup(x => x.CreatePet(It.IsAny<PetEntity>()));
            _mockPetDocumentEvidenceRepository!.Setup(x => x.SavePetDocumentEvidence(It.IsAny<List<PetDocumentEvidenceEntity>>()))
                .ReturnsAsync(true);

            var result = await _sut!.CreatePet(petVm);

            Assert.AreEqual(Guid.Empty, result);            
        }
    }
}
