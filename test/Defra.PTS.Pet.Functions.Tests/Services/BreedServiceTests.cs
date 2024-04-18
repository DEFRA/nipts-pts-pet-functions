using Defra.PTS.Pet.ApiServices.Implementation;
using Defra.PTS.Pet.Domain.Entities;
using Defra.PTS.Pet.Domain.Models;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defra.PTS.Pet.Functions.Tests.Services
{
    [TestFixture]
    public class BreedServiceTests
    {
        private BreedService? _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new BreedService();
        }

        [Test]
        public void GetBreeds_ReturnBreedsList()
        {
            var breedEntities = new List<BreedEntity>()
            {
                new BreedEntity() { Id = 1, Name = "Pug" },
                new BreedEntity() { Id = 2, Name = "Labrador" }
            };

            var expectedResult = new List<PetBreedViewModel>()
            {
                new PetBreedViewModel { BreedId = 1, BreedName = "Pug" },
                new PetBreedViewModel { BreedId = 2, BreedName = "Labrador" }
            };

            var actualResult = _sut!.GetBreeds(breedEntities);
            var orderedActual = actualResult.OrderBy(x => x.BreedId).ToList();
            Assert.AreEqual(expectedResult[0].BreedId, orderedActual[0].BreedId);
            Assert.AreEqual(expectedResult[0].BreedName, orderedActual[0].BreedName);
            Assert.AreEqual(expectedResult[1].BreedId, orderedActual[1].BreedId);
            Assert.AreEqual(expectedResult[1].BreedName, orderedActual[1].BreedName);
        }
    }
}
