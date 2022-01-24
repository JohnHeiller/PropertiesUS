using API.PropertiesUS.BL;
using API.PropertiesUS.DTO;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace API.PropertiesUS.Test
{
    /// <summary>
    /// PropertyImage entity BL test class
    /// </summary>
    [TestFixture]
    public class TestPropertyImageBL
    {
        private IPropertyImagesBL _propertyImageBL;
        public static PropertyImageDTO _propertyImage1Dto;
        public static PropertyImageDTO _propertyImage2Dto;
        public static PropertyImageDTO _propertyImage3Dto;
        public static PropertyImageSimpleDTO _propertyImageSimple1Dto;
        public static PropertyImageSimpleDTO _propertyImageSimple2Dto;
        public static PropertyImageSimpleDTO _propertyImageSimple3Dto;
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
            _propertyImageBL = new PropertyImagesBL(_connectionString);
        }

        /// <summary>
        /// Property containing data for test cases for PropertyImageDto
        /// </summary>
        public static IEnumerable<TestCaseData> PropertyImageDtoTestCases
        {
            get
            {
                yield return new TestCaseData(_propertyImage1Dto = new PropertyImageDTO()
                {
                    FileImage = null,
                    ImageBase64 = "KSHD553/KHSUAS/MWNM0Q02300T0E...",
                    IdProperty = null,
                    CodeInternalProperty = "",
                    NameProperty = "Edificio Saenz"
                });
                yield return new TestCaseData(_propertyImage2Dto = new PropertyImageDTO()
                {
                    FileImage = null,
                    ImageBase64 = "KSHD5099H/U7863GTMWNM0Q02300/0E...",
                    IdProperty = null,
                    CodeInternalProperty = "",
                    NameProperty = "Edificio Miranda"
                });
                yield return new TestCaseData(_propertyImage3Dto = new PropertyImageDTO()
                {
                    FileImage = null,
                    ImageBase64 = "KSHD5099H/U7863GTMWNM0Q02300/0E...",
                    IdProperty = null,
                    CodeInternalProperty = "ABC401",
                    NameProperty = ""
                });
            }
        }

        /// <summary>
        /// Property containing data for test cases for PropertyImageSimpleDto
        /// </summary>
        public static IEnumerable<TestCaseData> PropertyImageSimpleDtoTestCases
        {
            get
            {
                yield return new TestCaseData(_propertyImageSimple1Dto = new PropertyImageSimpleDTO()
                {
                    FileImage = null,
                    ImageBase64 = "KSHD5099H/U7863GTMWNM0Q02300/99...",
                    IdPropertyImage = 1
                });
                yield return new TestCaseData(_propertyImageSimple2Dto = new PropertyImageSimpleDTO()
                {
                    FileImage = null,
                    ImageBase64 = "KSHD5099H/U7863GTMWNM0Q0230015E...",
                    IdPropertyImage = 1
                });
                yield return new TestCaseData(_propertyImageSimple3Dto = new PropertyImageSimpleDTO()
                {
                    FileImage = null,
                    ImageBase64 = "KSHD5099H/U7863GTMWNM0Q02300/0E...",
                    IdPropertyImage = 1
                });
            }
        }

        /// <summary>
        /// Test to add new Property image record
        /// </summary>
        /// <param name="propertyImage">PropertyImageDTO data</param>
        [Test]
        [TestCaseSource(nameof(PropertyImageDtoTestCases))]
        public void AddImageByPropertyTest(PropertyImageDTO propertyImage)
        {
            var resp = _propertyImageBL.AddImageByProperty(propertyImage);
            Assert.IsTrue(new long().GetType() == resp.GetType());
            Assert.IsInstanceOf(new long().GetType(), resp);
            Assert.IsTrue(resp > 0);
        }

        /// <summary>
        /// Test to update a Property image record
        /// </summary>
        /// <param name="propertyImage">PropertyImageSimpleDTO data</param>
        [Test]
        [TestCaseSource(nameof(PropertyImageSimpleDtoTestCases))]
        public void UpdateImageByIdTest(PropertyImageSimpleDTO propertyImage)
        {
            var resp = _propertyImageBL.UpdateImageById(propertyImage);
            Assert.IsTrue(new bool().GetType() == resp.GetType());
            Assert.IsInstanceOf(new bool().GetType(), resp);
            Assert.IsTrue(resp);
        }
    }
}