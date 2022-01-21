using API.PropertiesUS.BL;
using API.PropertiesUS.DTO;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace API.PropertiesUS.Test
{
    /// <summary>
    /// PropertyTrace entity BL test class
    /// </summary>
    [TestFixture]
    public class TestPropertyTraceBL
    {
        private IPropertyTraceBL _propertyTraceBL;
        public static PropertyTraceDTO _propertyTrace1Dto;
        public static PropertyTraceDTO _propertyTrace2Dto;
        public static PropertyTraceDTO _propertyTrace3Dto;
        public static PropertyTraceSimpleDTO _propertyTraceSimple1Dto;
        public static PropertyTraceSimpleDTO _propertyTraceSimple2Dto;
        public static PropertyTraceSimpleDTO _propertyTraceSimple3Dto;
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
            _propertyTraceBL = new PropertyTraceBL(_connectionString);
        }

        /// <summary>
        /// Property containing data for test cases for PropertyTraceDTO
        /// </summary>
        public static IEnumerable<TestCaseData> PropertyTraceDtoTestCases
        {
            get
            {
                yield return new TestCaseData(_propertyTrace1Dto = new PropertyTraceDTO()
                {
                    DateSale = "2011-02-05",
                    NameTrace = "Primera venta",
                    TaxTrace = "5000",
                    ValueTrace = "25000000",
                    IdProperty = null,
                    CodeInternalProperty = "ABC401",
                    NameProperty = ""
                });
                yield return new TestCaseData(_propertyTrace2Dto = new PropertyTraceDTO()
                {
                    DateSale = "2005-12-05",
                    NameTrace = "Tercera venta",
                    TaxTrace = "8500",
                    ValueTrace = "36000000",
                    IdProperty = null,
                    CodeInternalProperty = "",
                    NameProperty = "Edificio Miranda"
                });
                yield return new TestCaseData(_propertyTrace3Dto = new PropertyTraceDTO()
                {
                    DateSale = "2015-02-25",
                    NameTrace = "Quinta venta",
                    TaxTrace = "1600",
                    ValueTrace = "5000000",
                    IdProperty = null,
                    CodeInternalProperty = "",
                    NameProperty = "Mansion Saenz"
                });
            }
        }

        /// <summary>
        /// Property containing data for test cases for PropertyTraceSimpleDTO
        /// </summary>
        public static IEnumerable<TestCaseData> PropertyTraceSimpleDtoTestCases
        {
            get
            {
                yield return new TestCaseData(_propertyTraceSimple1Dto = new PropertyTraceSimpleDTO()
                {
                    DateSale = "",
                    Value = "15600000",
                    Tax = "18600",
                    Name = "",
                    IdPropertyTrace = 1
                });
                yield return new TestCaseData(_propertyTraceSimple2Dto = new PropertyTraceSimpleDTO()
                {
                    DateSale = "",
                    Value = "20599000",
                    Tax = "2000",
                    Name = "Sexta Venta",
                    IdPropertyTrace = 1
                });
                yield return new TestCaseData(_propertyTraceSimple3Dto = new PropertyTraceSimpleDTO()
                {
                    DateSale = "2001-12-12",
                    Value = "10000000",
                    Tax = "19900",
                    Name = "",
                    IdPropertyTrace = 1
                });
            }
        }

        /// <summary>
        /// Test to create new trace record
        /// </summary>
        /// <param name="propertyTrace">PropertyTraceDTO data</param>
        [Test]
        [TestCaseSource(nameof(PropertyTraceDtoTestCases))]
        public void CreateTraceTest(PropertyTraceDTO propertyTrace)
        {
            var resp = _propertyTraceBL.CreateTrace(propertyTrace);
            Assert.IsTrue(new long().GetType() == resp.GetType());
            Assert.IsInstanceOf(new long().GetType(), resp);
            Assert.IsTrue(resp > 0);
        }

        /// <summary>
        /// Test to update a property trace record
        /// </summary>
        /// <param name="propertySimple">PropertyTraceSimpleDTO data</param>
        [Test]
        [TestCaseSource(nameof(PropertyTraceSimpleDtoTestCases))]
        public void UpdatePropertyTraceTest(PropertyTraceSimpleDTO propertySimple)
        {
            var resp = _propertyTraceBL.UpdatePropertyTrace(propertySimple);
            Assert.IsTrue(new bool().GetType() == resp.GetType());
            Assert.IsInstanceOf(new bool().GetType(), resp);
            Assert.IsTrue(resp);
        }
    }
}