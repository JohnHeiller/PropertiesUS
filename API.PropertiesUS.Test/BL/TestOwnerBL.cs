using API.PropertiesUS.BL;
using API.PropertiesUS.DAL.Dominio;
using API.PropertiesUS.DTO;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace API.PropertiesUS.Test
{
    /// <summary>
    /// Owner entity BL test class
    /// </summary>
    [TestFixture]
    public class TestOwnerBL
    {
        private IOwnerBL _ownersBL;
        public static OwnerDTO _owners1Dto;
        public static OwnerDTO _owners2Dto;
        public static OwnerDTO _owners3Dto;
        private string _connectionString;

        /// <summary>
        /// Class configuration method
        /// </summary>
        [SetUp]
        public void Setup()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();
            _connectionString = root.GetConnectionString("APIConnection");
            _ownersBL = new OwnerBL(_connectionString);
        }

        /// <summary>
        /// Property containing data for test cases
        /// </summary>
        public static IEnumerable<TestCaseData> OwnersDtoTestCases
        {
            get
            {
                yield return new TestCaseData(_owners1Dto = new OwnerDTO()
                {
                    Address = "Carrea 123 nro 32-01",
                    Birthday = "1990-12-01",
                    Name = "Anthony Saenz"
                });
                yield return new TestCaseData(_owners2Dto = new OwnerDTO()
                {
                    Birthday = "1988-10-15",
                    Address = "Calle 3 nro 05-11",
                    Name = "Carlos Miranda"
                });
                yield return new TestCaseData(_owners3Dto = new OwnerDTO()
                {
                    Name = "Juan Perez",
                    Address = "Av. sol nro 12-77",
                    Birthday = "1991-01-01"
                });
            }
        }

        /// <summary>
        /// Test to create new owner record
        /// </summary>
        /// <param name="owner">OwnerDTO data</param>
        [Test]
        [TestCaseSource(nameof(OwnersDtoTestCases))]
        public void CreateOwnerTest(OwnerDTO owner)
        {
            var resp = _ownersBL.CreateOwner(owner);
            Assert.IsTrue(new long().GetType() == resp.GetType());
            Assert.IsInstanceOf(new long().GetType(), resp);
            Assert.IsTrue(resp > 0);
        }

        /// <summary>
        /// Test to get all records of owner
        /// </summary>
        [Test]
        public void GetAllTest()
        {
            var resp = _ownersBL.GetAll();
            Assert.IsTrue(new List<Owners>().GetType() == resp.GetType());
            Assert.IsInstanceOf(new List<Owners>().GetType(), resp);
            if (resp != null && resp.Count > 0)
            {
                Assert.IsTrue(resp.Count > 0);
                Assert.IsTrue(resp.Any(x => x.Name.Length > 0));
                Assert.IsTrue(resp.Any(x => x.Name.GetType() == string.Empty.GetType()));
                Assert.IsTrue(resp.Any(x => x.IdOwner > 0));
                Assert.IsTrue(resp.Any(x => x.IdOwner.GetType() == new long().GetType()));
            }
        }
    }
}