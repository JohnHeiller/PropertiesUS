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
    /// Property entity BL test class
    /// </summary>
    [TestFixture]
    public class TestPropertyBL
    {
        private IPropertyBL _propertyBL;
        public static PropertyDTO _property1Dto;
        public static PropertyDTO _property2Dto;
        public static PropertyDTO _property3Dto;
        public static PropertyPriceDTO _propertyPrice1Dto;
        public static PropertyPriceDTO _propertyPrice2Dto;
        public static PropertyPriceDTO _propertyPrice3Dto;
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
            _propertyBL = new PropertyBL(_connectionString);
        }

        /// <summary>
        /// Property containing data for test cases for PropertyDto
        /// </summary>
        public static IEnumerable<TestCaseData> PropertyDtoTestCases
        {
            get
            {
                yield return new TestCaseData(_property1Dto = new PropertyDTO()
                {
                    AddressProperty = "Carrea 123 nro 32-01",
                    CodeInternalProperty = "ABC401",
                    NameProperty = "Edificio Saenz",
                    PriceProperty = "5000000",
                    Year = "2000",
                    NameOwner = "Anthony Saenz"
                });
                yield return new TestCaseData(_property2Dto = new PropertyDTO()
                {
                    AddressProperty = "Calle 3 nro 05-11",
                    CodeInternalProperty = "",
                    NameProperty = "Edificio Miranda",
                    PriceProperty = "1000000",
                    Year = "2005",
                    NameOwner = "Carlos Miranda"
                });
                yield return new TestCaseData(_property3Dto = new PropertyDTO()
                {
                    AddressProperty = "Av. sol nro 12-77",
                    CodeInternalProperty = "",
                    NameProperty = "Mansion Saenz",
                    PriceProperty = "1800000",
                    Year = "2010",
                    NameOwner = "Anthony Saenz"
                });
            }
        }

        /// <summary>
        /// Property containing data for test cases for PropertyPriceDto
        /// </summary>
        public static IEnumerable<TestCaseData> PropertyPriceDtoTestCases
        {
            get
            {
                yield return new TestCaseData(_propertyPrice1Dto = new PropertyPriceDTO()
                {
                    CodeInternal = "ABC401",
                    IdProperty = null,
                    Name = "",
                    Price = "2800000"
                });
                yield return new TestCaseData(_propertyPrice2Dto = new PropertyPriceDTO()
                {
                    CodeInternal = "",
                    Name = "Edificio Miranda",
                    IdProperty = null,
                    Price = "1350000"
                });
                yield return new TestCaseData(_propertyPrice3Dto = new PropertyPriceDTO()
                {
                    CodeInternal = "",
                    IdProperty = null,
                    Name = "Mansion Saenz",
                    Price = "2100000"
                });
            }
        }

        /// <summary>
        /// Test to create new Property record
        /// </summary>
        /// <param name="property">PropertyDTO data</param>
        [Test]
        [TestCaseSource(nameof(PropertyDtoTestCases))]
        public void CreatePropertyTest(PropertyDTO property)
        {
            var resp = _propertyBL.CreateProperty(property);
            Assert.IsTrue(new long().GetType() == resp.GetType());
            Assert.IsInstanceOf(new long().GetType(), resp);
            Assert.IsTrue(resp > 0);
        }

        /// <summary>
        /// Test to get records of Properties by filters
        /// </summary>
        /// <param name="property">PropertyDTO data</param>
        [Test]
        [TestCaseSource(nameof(PropertyDtoTestCases))]
        public void GetPropertiesByFilterTest(PropertyDTO property)
        {
            var resp = _propertyBL.GetPropertiesByFilter(property);
            Assert.IsTrue(new List<Properties>().GetType() == resp.GetType());
            Assert.IsInstanceOf(new List<Properties>().GetType(), resp);
            if (resp != null && resp.Count > 0)
            {
                Assert.IsTrue(resp.Count > 0);
                Assert.IsTrue(resp.Any(x => x.Name.Length > 0));
                Assert.IsTrue(resp.Any(x => x.Name.GetType() == string.Empty.GetType()));
                Assert.IsTrue(resp.Any(x => x.IdProperty > 0));
                Assert.IsTrue(resp.Any(x => x.IdProperty.GetType() == new long().GetType()));
            }
        }

        /// <summary>
        /// Test to update Property price 
        /// </summary>
        /// <param name="propertyPrice">PropertyPriceDTO data</param>
        [Test]
        [TestCaseSource(nameof(PropertyPriceDtoTestCases))]
        public void UpdatePropertyPriceTest(PropertyPriceDTO propertyPrice)
        {
            var resp = _propertyBL.UpdatePropertyPrice(propertyPrice);
            Assert.IsTrue(new bool().GetType() == resp.GetType());
            Assert.IsInstanceOf(new bool().GetType(), resp);
            Assert.IsTrue(resp);
        }
    }
}