using System;
using API.PropertiesUS.BL;
using API.PropertiesUS.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace API.PropertiesUS.Controllers
{
    /// <summary>
    /// Controller actions for Property data management
    /// </summary>
    [Authorize]
    [SwaggerTag("Properties API - Controller actions for Property data management")]
    [ApiController]
    [Route("[controller]")]
    public class PropertyController : ControllerBase
    {
        /// <summary>
        /// Object of type IConfiguration
        /// </summary>
        public IConfiguration _configuration { get; }
        /// <summary>
        /// Object of type IPropertyBL
        /// </summary>
        private readonly IPropertyBL _propertyBL;
        /// <summary>
        /// Object of type ILogger(PropertyController)
        /// </summary>
        private readonly ILogger<PropertyController> _logger;

        /// <summary>
        /// Class constructor of controller
        /// </summary>
        /// <param name="configuration">Object of type IConfiguration</param>
        /// <param name="logger">Object to record the log</param>
        public PropertyController(IConfiguration configuration, ILogger<PropertyController> logger)
        {
            _logger = logger;
            _configuration = configuration;
            string connection = configuration.GetSection("ConnectionStrings")["APIConnection"];
            _propertyBL = new PropertyBL(connection);
        }

        /// <summary>
        /// Create a new Property record
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Create
        ///     {
        ///       "nameProperty": "Edificio Caobos",
        ///       "addressProperty": "Calle 10 # 11 - 1",
        ///       "priceProperty": "1000000",
        ///       "codeInternalProperty": "",
        ///       "year": "2005",
        ///       "idOwner": null,
        ///       "nameOwner": "Usuario1"
        ///     }
        ///      
        /// </remarks>
        /// <param name="property">PropertyDTO with basic data to register the property</param>
        /// <returns>Record Identifier</returns>
        /// <response code="200">Returns the ID of the new recor</response>
        /// <response code="400">Notify error in business validations</response>  
        /// <response code="401">Report authentication error</response>  
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create(PropertyDTO property)
        {
            try
            {
                var resp = _propertyBL.CreateProperty(property);
                return Ok(new { idProperty = resp });
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.InnerException != null ? exc.InnerException.Message : exc.Message);
                return BadRequest(exc.InnerException != null ? exc.InnerException.Message : exc.Message);                
            }
        }

        /// <summary>
        /// Update the price field in a property record, the property is filtered by name, internal code or ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /ChangePrice
        ///     {
        ///       "price": "550000",
        ///       "name": "",
        ///       "codeInternal": "",
        ///       "idProperty": 1
        ///     }
        ///      
        /// </remarks>
        /// <param name="propertyPrice">PropertyPriceDTO with data to identify the property and its new price</param>
        /// <returns>Successful update indicator</returns>
        /// <response code="200">Return successful change indicator</response>
        /// <response code="400">Notify error in business validations</response>  
        /// <response code="401">Report authentication error</response>  
        [HttpPost]
        [Route("ChangePrice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult ChangePrice(PropertyPriceDTO propertyPrice)
        {
            try
            {
                var resp = _propertyBL.UpdatePropertyPrice(propertyPrice);
                return Ok(new { priceChanged = resp });
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.InnerException != null ? exc.InnerException.Message : exc.Message);
                return BadRequest(exc.InnerException != null ? exc.InnerException.Message : exc.Message);
            }
        }

        /// <summary>
        /// Obtains a list of Property records according to the fields filled in as a filter
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /GetListByFilters
        ///     {
        ///       "nameProperty": "",
        ///       "addressProperty": "",
        ///       "priceProperty": "550000",
        ///       "codeInternalProperty": "",
        ///       "year": "",
        ///       "idOwner": 3,
        ///       "nameOwner": ""
        ///     }
        ///      
        /// </remarks>
        /// <param name="property">PropertyDTO with data to fill in to use as query filters</param>
        /// <returns>List of property records</returns>
        /// <response code="200">Return list of records consulted</response>
        /// <response code="400">Notify error in business validations</response>  
        /// <response code="401">Report authentication error</response>  
        [HttpPost]
        [Route("GetListByFilters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult GetListByFilters(PropertyDTO property)
        {
            try
            {
                var resp = _propertyBL.GetPropertiesByFilter(property);
                return Ok(new { propertiesList = resp });
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.InnerException != null ? exc.InnerException.Message : exc.Message);
                return BadRequest(exc.InnerException != null ? exc.InnerException.Message : exc.Message);
            }
        }

        /// <summary>
        /// Update a Property record
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Update
        ///     {
        ///       "idProperty": 5,
        ///       "nameProperty": "Edificio Caobos",
        ///       "addressProperty": "Calle 10 # 11 - 1",
        ///       "priceProperty": "1000000",
        ///       "codeInternalProperty": "",
        ///       "year": "2005",
        ///       "idOwner": 1
        ///     }
        ///      
        /// </remarks>
        /// <param name="property">PropertyDTO with basic data to register the property</param>
        /// <returns>Successful update indicator</returns>
        /// <response code="200">Return indicator of record updated</response>
        /// <response code="400">Notify error in business validations</response>  
        /// <response code="401">Report authentication error</response>  
        [HttpPost]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(PropertyDTO property)
        {
            try
            {
                var resp = _propertyBL.UpdateProperty(property);
                return Ok(new { isUpdated = resp });
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.InnerException != null ? exc.InnerException.Message : exc.Message);
                return BadRequest(exc.InnerException != null ? exc.InnerException.Message : exc.Message);
            }
        }

    }
}
