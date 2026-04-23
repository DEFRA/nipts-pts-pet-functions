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
        private Mock<IBreedRepository>? _mockBreedRepository;

        private PetService? _sut;

        [SetUp]
        public void Setup()
        {
            _mockPetRepostory = new Mock<IPetRepository>();
            _mockPetDocumentEvidenceRepository = new Mock<IPetDocumentEvidenceRepository>();
            _mockBreedRepository = new Mock<IBreedRepository>();

            _sut = new PetService(_mockPetRepostory.Object, _mockPetDocumentEvidenceRepository.Object, _mockBreedRepository.Object);
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
                PetIdentificationEvidenceViewModel = new List<PetIdentificationEvidenceViewModel> { new PetIdentificationEvidenceViewModel() { FileName = "item1.png" } }
            };

            _mockPetRepostory!.Setup(x => x.CreatePet(It.IsAny<PetEntity>()));
            _mockPetDocumentEvidenceRepository!.Setup(x => x.SavePetDocumentEvidence(It.IsAny<List<PetDocumentEvidenceEntity>>()))
                .ReturnsAsync(true);

            var result = await _sut!.CreatePet(petVm);

            Assert.AreEqual(Guid.Empty, result);            
        }

        [Test]
        public async Task CreatePet_WithNonBreedSpecies_SetsBreedFieldsToNull()
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
                    PetSpecies = PetSpecies.Ferret
                },
                PetBreed = new PetBreedViewModel
                {
                    BreedType = BreedType.PurebredOrPedigree,
                    BreedId = 1
                },
                PetName = new PetNameViewModel
                {
                    PetName = "Buddy"
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
                    PetColourOther = "white"
                },
                PetFeature = new PetFeatureViewModel
                {
                    HasUniqueFeature = YesNo.No,
                    FeatureDescription = "None",
                },
                PetIdentificationEvidenceViewModel = null
            };

            _mockPetRepostory!.Setup(x => x.CreatePet(It.IsAny<PetEntity>()));
            _mockPetDocumentEvidenceRepository!.Setup(x => x.SavePetDocumentEvidence(It.IsAny<List<PetDocumentEvidenceEntity>>()))
                .ReturnsAsync(true);

            var result = await _sut!.CreatePet(petVm);

            Assert.AreEqual(Guid.Empty, result);
            _mockPetRepostory.Verify(x => x.CreatePet(It.Is<PetEntity>(p => p.BreedId == null && p.BreedTypeId == null)), Times.Once);
        }

        [Test]
        public async Task CreatePet_WithNoEvidence_SavesEmptyList()
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
                    PetName = "Rex"
                },
                PetGender = new PetGenderViewModel
                {
                    Gender = PetGender.Female,
                },
                PetAge = new PetAgeViewModel
                {
                    KnowDoB = YesNo.Yes,
                    Day = "15",
                    Month = "5",
                    Year = "2020"
                },
                PetColour = new PetColourViewModel
                {
                    PetColour = 2
                },
                PetFeature = new PetFeatureViewModel
                {
                    HasUniqueFeature = YesNo.Yes,
                    FeatureDescription = "Scar on left ear",
                },
                PetIdentificationEvidenceViewModel = new List<PetIdentificationEvidenceViewModel>()
            };

            _mockPetRepostory!.Setup(x => x.CreatePet(It.IsAny<PetEntity>()));
            _mockPetDocumentEvidenceRepository!.Setup(x => x.SavePetDocumentEvidence(It.IsAny<List<PetDocumentEvidenceEntity>>()))
                .ReturnsAsync(true);

            var result = await _sut!.CreatePet(petVm);

            Assert.AreEqual(Guid.Empty, result);
            _mockPetDocumentEvidenceRepository.Verify(
                x => x.SavePetDocumentEvidence(It.Is<List<PetDocumentEvidenceEntity>>(l => l.Count == 0)), Times.Once);
        }

        [Test]
        public async Task CheckMicrochipAsync_ReturnsMicrochipNumber()
        {
            var microchipNumber = "123456789012345";
            _mockBreedRepository!.Setup(x => x.GetMicrochipNumberAsync(microchipNumber)).ReturnsAsync(microchipNumber);

            var result = await _sut!.CheckMicrochipAsync(microchipNumber);

            Assert.AreEqual(microchipNumber, result);
            _mockBreedRepository.Verify(x => x.GetMicrochipNumberAsync(microchipNumber), Times.Once);
        }

        [Test]
        public async Task CheckMicrochipAsync_WhenNotFound_ReturnsNull()
        {
            var microchipNumber = "999999999999999";
            _mockBreedRepository!.Setup(x => x.GetMicrochipNumberAsync(microchipNumber)).ReturnsAsync((string?)null);

            var result = await _sut!.CheckMicrochipAsync(microchipNumber);

            Assert.IsNull(result);
            _mockBreedRepository.Verify(x => x.GetMicrochipNumberAsync(microchipNumber), Times.Once);
        }
    }
}
